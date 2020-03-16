﻿/*
 * SonarLint for Visual Studio
 * Copyright (C) 2016-2020 SonarSource SA
 * mailto:info AT sonarsource DOT com
 *
 * This program is free software; you can redistribute it and/or
 * modify it under the terms of the GNU Lesser General Public
 * License as published by the Free Software Foundation; either
 * version 3 of the License, or (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
 * Lesser General Public License for more details.
 *
 * You should have received a copy of the GNU Lesser General Public License
 * along with this program; if not, write to the Free Software Foundation,
 * Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA.
 */

using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.CodeAnalysis;
using Microsoft.VisualStudio.Shell.Interop;
using SonarAnalyzer.Helpers;
using SonarLint.VisualStudio.Core.SystemAbstractions;
using SonarLint.VisualStudio.Integration.NewConnectedMode;
using SonarLint.VisualStudio.Integration.Suppression;
using SonarLint.VisualStudio.Integration.Vsix.Suppression;
using SonarQube.Client;

namespace SonarLint.VisualStudio.Integration.Vsix
{
    // This class is simply responsible to react to the solution binding state change and to trigger the right workflow
    internal sealed class SonarAnalyzerManager : IDisposable
    {
        private readonly IActiveSolutionBoundTracker activeSolutionBoundTracker;
        private readonly ISonarQubeService sonarQubeService;
        private readonly Workspace workspace;
        private readonly IVsSolution vsSolution;
        private readonly ILogger logger;

        private readonly Func<IEnumerable<DiagnosticDescriptor>, SyntaxTree, bool> previousShouldExecuteRegisteredAction;
        private readonly Action<IReportingContext> previousReportDiagnostic;
        private readonly Func<SyntaxTree, Diagnostic, bool> previousShouldDiagnosticBeReported;

        private readonly List<IDisposable> disposableObjects = new List<IDisposable>();

        internal /* for testing purposes */ SonarAnalyzerWorkflowBase currentWorklow;

        public SonarAnalyzerManager(IActiveSolutionBoundTracker activeSolutionBoundTracker, ISonarQubeService sonarQubeService,
            Workspace workspace, IVsSolution vsSolution, ILogger logger)
        {
            if (activeSolutionBoundTracker == null)
            {
                throw new ArgumentNullException(nameof(activeSolutionBoundTracker));
            }

            if (sonarQubeService == null)
            {
                throw new ArgumentNullException(nameof(sonarQubeService));
            }

            if (workspace == null)
            {
                throw new ArgumentNullException(nameof(workspace));
            }

            if (vsSolution == null)
            {
                throw new ArgumentNullException(nameof(vsSolution));
            }

            if (logger == null)
            {
                throw new ArgumentNullException(nameof(logger));
            }

            this.activeSolutionBoundTracker = activeSolutionBoundTracker;
            this.sonarQubeService = sonarQubeService;
            this.workspace = workspace;
            this.vsSolution = vsSolution;
            this.logger = logger;

            // Saving previous state so that SonarLint doesn't have to know what's the default state in SonarAnalyzer
            this.previousShouldExecuteRegisteredAction = SonarAnalysisContext.ShouldExecuteRegisteredAction;
            this.previousShouldDiagnosticBeReported = SonarAnalysisContext.ShouldDiagnosticBeReported;
            this.previousReportDiagnostic = SonarAnalysisContext.ReportDiagnostic;

            activeSolutionBoundTracker.SolutionBindingChanged += OnSolutionBindingChanged;
            activeSolutionBoundTracker.SolutionBindingUpdated += OnSolutionBindingUpdated;

            RefreshWorkflow(this.activeSolutionBoundTracker.CurrentConfiguration);
        }

        private void OnSolutionBindingChanged(object sender, ActiveSolutionBindingEventArgs e)
        {
            RefreshWorkflow(e.Configuration);
        }

        private void OnSolutionBindingUpdated(object sender, EventArgs e)
        {
            Debug.Assert(this.activeSolutionBoundTracker.CurrentConfiguration.Mode != SonarLintMode.Standalone,
                "Not expecting to received the solution binding update event in standalone mode.");

            RefreshWorkflow(this.activeSolutionBoundTracker.CurrentConfiguration);
        }

        private void RefreshWorkflow(BindingConfiguration configuration)
        {
            // There might be some race condition here if an analysis is triggered while the delegates are reset and set back
            // to the new workflow behavior. This race condition would lead to some issues being reported using the old mode
            // instead of the new one but everything will be fixed on the next analysis.
            ResetState();

            switch (configuration?.Mode)
            {
                case SonarLintMode.Standalone:
                    this.logger.WriteLine(Resources.Strings.AnalyzerManager_InStandaloneMode);
                    this.currentWorklow = new SonarAnalyzerStandaloneWorkflow(this.workspace);
                    break;

                case SonarLintMode.LegacyConnected:
                case SonarLintMode.Connected:
                    this.logger.WriteLine(Resources.Strings.AnalyzerManager_InConnectedMode);
                    var sonarQubeIssueProvider = new SonarQubeIssuesProvider(sonarQubeService, configuration.Project.ProjectKey,
                        new TimerFactory(), this.logger);
                    this.disposableObjects.Add(sonarQubeIssueProvider);
                    var liveIssueFactory = new RoslynLiveIssueFactory(workspace, vsSolution);
                    var suppressionHandler = new RoslynSuppressionHandler(liveIssueFactory, sonarQubeIssueProvider);

                    if (configuration.Mode == SonarLintMode.Connected)
                    {
                        this.currentWorklow = new SonarAnalyzerConnectedWorkflow(this.workspace, suppressionHandler);
                    }
                    else // Legacy
                    {
                        this.currentWorklow = new SonarAnalyzerLegacyConnectedWorkflow(this.workspace, suppressionHandler,
                            this.logger);
                    }
                    break;
            }
        }

        private void ResetState()
        {
            this.disposableObjects.ForEach(x => x.Dispose());
            this.disposableObjects.Clear();
            this.currentWorklow?.Dispose();
            this.currentWorklow = null;

            SonarAnalysisContext.ShouldDiagnosticBeReported = this.previousShouldDiagnosticBeReported;
            SonarAnalysisContext.ShouldExecuteRegisteredAction = this.previousShouldExecuteRegisteredAction;
            SonarAnalysisContext.ReportDiagnostic = this.previousReportDiagnostic;
        }

        #region IDisposable Support
        private bool disposedValue;
        public void Dispose()
        {
            if (disposedValue)
            {
                return;
            }

            ResetState();
            this.activeSolutionBoundTracker.SolutionBindingChanged -= OnSolutionBindingChanged;
            this.disposedValue = true;
        }
        #endregion
    }
}
