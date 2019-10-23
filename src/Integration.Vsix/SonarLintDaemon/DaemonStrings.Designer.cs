﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SonarLint.VisualStudio.Integration.Vsix {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class DaemonStrings {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal DaemonStrings() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("SonarLint.VisualStudio.Integration.Vsix.SonarLintDaemon.DaemonStrings", typeof(DaemonStrings).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Disabled rule &quot;{0}&quot;.
        /// </summary>
        internal static string DisableRule_DisabledRule {
            get {
                return ResourceManager.GetString("DisableRule_DisabledRule", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Error in DisableCommandRule.QueryStatus:
        ///  Error: {0}.
        /// </summary>
        internal static string DisableRule_ErrorCheckingCommandStatus {
            get {
                return ResourceManager.GetString("DisableRule_ErrorCheckingCommandStatus", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Error disabling rule &quot;{0}&quot;:
        ///  Error: {1}.
        /// </summary>
        internal static string DisableRule_ErrorDisablingRule {
            get {
                return ResourceManager.GetString("DisableRule_ErrorDisablingRule", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {unknown}.
        /// </summary>
        internal static string DisableRule_UnknownErrorCode {
            get {
                return ResourceManager.GetString("DisableRule_UnknownErrorCode", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Error handling file change notification: {0}.
        /// </summary>
        internal static string FileMonitor_ErrorHandlingFileChange {
            get {
                return ResourceManager.GetString("FileMonitor_ErrorHandlingFileChange", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to File changed: {0}. Change type: {1}.
        /// </summary>
        internal static string FileMonitor_FileChanged {
            get {
                return ResourceManager.GetString("FileMonitor_FileChanged", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Created directory: {0}.
        /// </summary>
        internal static string FileMonitory_CreatedDirectory {
            get {
                return ResourceManager.GetString("FileMonitory_CreatedDirectory", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Error re-analysing open documents: {0}.
        /// </summary>
        internal static string ReanalysisStatusBar_Error {
            get {
                return ResourceManager.GetString("ReanalysisStatusBar_Error", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SonarLint: re-analysing open documents. Completed {0} of {1}.
        /// </summary>
        internal static string ReanalysisStatusBar_InProgress {
            get {
                return ResourceManager.GetString("ReanalysisStatusBar_InProgress", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Error loading user settings from &quot;{0}&quot;. Default rules configuration will be used.
        ///  Error message: {1}.
        /// </summary>
        internal static string Settings_ErrorLoadingSettings {
            get {
                return ResourceManager.GetString("Settings_ErrorLoadingSettings", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Error saving user settings to &quot;{0}&quot;.
        ///  Error: {1}.
        /// </summary>
        internal static string Settings_ErrorSavingSettings {
            get {
                return ResourceManager.GetString("Settings_ErrorSavingSettings", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Loaded user settings from &quot;{0}&quot;..
        /// </summary>
        internal static string Settings_LoadedUserSettings {
            get {
                return ResourceManager.GetString("Settings_LoadedUserSettings", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to User settings file does not exist at &quot;{0}&quot;. Using default rules configuration..
        /// </summary>
        internal static string Settings_NoUserSettings {
            get {
                return ResourceManager.GetString("Settings_NoUserSettings", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Saved user settings to &quot;{0}&quot;..
        /// </summary>
        internal static string Settings_SavedUserSettings {
            get {
                return ResourceManager.GetString("Settings_SavedUserSettings", resourceCulture);
            }
        }
    }
}
