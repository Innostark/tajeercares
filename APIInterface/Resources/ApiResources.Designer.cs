﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18408
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace APIInterface.Resources {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class ApiResources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal ApiResources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("APIInterface.Resources.ApiResources", typeof(ApiResources).Assembly);
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
        ///   Looks up a localized string similar to https://localhost/WebApi/Api/.
        /// </summary>
        internal static string BaseAddress {
            get {
                return ResourceManager.GetString("BaseAddress", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to GetServiceItemsInsurances.
        /// </summary>
        internal static string ExtrasInsurances {
            get {
                return ResourceManager.GetString("ExtrasInsurances", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to GetSiteContents.
        /// </summary>
        internal static string GetSiteContents {
            get {
                return ResourceManager.GetString("GetSiteContents", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to GetAvailableHireGroupDetails.
        /// </summary>
        internal static string HireGroupDetail {
            get {
                return ResourceManager.GetString("HireGroupDetail", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to HireGroupStandardRate.
        /// </summary>
        internal static string HireGroupRate {
            get {
                return ResourceManager.GetString("HireGroupRate", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to GetAllParentHireGroups.
        /// </summary>
        internal static string ParentHireGroup {
            get {
                return ResourceManager.GetString("ParentHireGroup", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to RegisterUser.
        /// </summary>
        internal static string RegisterUser {
            get {
                return ResourceManager.GetString("RegisterUser", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Something bad happend. Please try again later. We have recorded your action, and you will be contacted on the email you provided. We apologize for inconvenience!.
        /// </summary>
        internal static string registerUserError {
            get {
                return ResourceManager.GetString("registerUserError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to GetServiceItemRate.
        /// </summary>
        internal static string ServiceItemRate {
            get {
                return ResourceManager.GetString("ServiceItemRate", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to CheckUserAvailability.
        /// </summary>
        internal static string UserAvailability {
            get {
                return ResourceManager.GetString("UserAvailability", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to https://localhost/WebApi/Api/.
        /// </summary>
        internal static string WebApiBaseAddress {
            get {
                return ResourceManager.GetString("WebApiBaseAddress", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to QWEasd123.
        /// </summary>
        internal static string WebApiPassword {
            get {
                return ResourceManager.GetString("WebApiPassword", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to innoist.
        /// </summary>
        internal static string WebApiUserName {
            get {
                return ResourceManager.GetString("WebApiUserName", resourceCulture);
            }
        }
    }
}
