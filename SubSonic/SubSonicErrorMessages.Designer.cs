﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SubSonic {
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
    internal class SubSonicErrorMessages {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal SubSonicErrorMessages() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("SubSonic.SubSonicErrorMessages", typeof(SubSonicErrorMessages).Assembly);
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
        ///   Looks up a localized string similar to Type {0}is not registed with the db model..
        /// </summary>
        internal static string EntityTypeIsNotRegisteredException {
            get {
                return ResourceManager.GetString("EntityTypeIsNotRegisteredException", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {0} does not implement the {1} interface..
        /// </summary>
        internal static string MissingInterfaceImplementation {
            get {
                return ResourceManager.GetString("MissingInterfaceImplementation", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Supply the name of the containing method..
        /// </summary>
        internal static string MissingNameArgumentException {
            get {
                return ResourceManager.GetString("MissingNameArgumentException", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to IServiceCollection is not found in the IOC container..
        /// </summary>
        internal static string MissingServiceCollectionException {
            get {
                return ResourceManager.GetString("MissingServiceCollectionException", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Provider &quot;{0}&quot;, is not registered in {1}..
        /// </summary>
        internal static string ProviderInvariantNameNotRegisteredException {
            get {
                return ResourceManager.GetString("ProviderInvariantNameNotRegisteredException", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {0} SqlQueryProvider is null..
        /// </summary>
        internal static string SqlQueryProviderIsNull {
            get {
                return ResourceManager.GetString("SqlQueryProviderIsNull", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The LINQ member {0} is not supported..
        /// </summary>
        internal static string UnSupportedMemberException {
            get {
                return ResourceManager.GetString("UnSupportedMemberException", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The LINQ expression node of type {0} is not supported..
        /// </summary>
        internal static string UnSupportedNodeException {
            get {
                return ResourceManager.GetString("UnSupportedNodeException", resourceCulture);
            }
        }
    }
}
