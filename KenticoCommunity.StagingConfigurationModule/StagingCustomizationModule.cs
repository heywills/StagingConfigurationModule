using System.Collections.Generic;
using System.Linq;
using CMS;
using CMS.Base;
using CMS.DataEngine;
using CMS.EventLog;
using CMS.MediaLibrary;
using CMS.Membership;
using CMS.Modules;
using CMS.Synchronization;
using KenticoCommunity.StagingConfigurationModule;
using KenticoCommunity.StagingConfigurationModule.Configuration;

[assembly: RegisterModule(typeof(StagingCustomizationModule))]

namespace KenticoCommunity.StagingConfigurationModule
{
    /// <summary>
    /// Custom Module to override Kentico staging task events
    /// </summary>
    public class StagingCustomizationModule : Module
    {
        private List<string> excludedTypes = new List<string>();
        private List<string> excludedMediaLibraries = new List<string>();

        public StagingCustomizationModule() : base("StagingCustomizationModule")
        {
        }

        protected override void OnPreInit()
        {
            excludedTypes = new List<string>();
            excludedMediaLibraries = new List<string>();

            var configuration = ConfigurationHelper.GetWebConfiguration();
            var stagingExcludedTypes =
                (StagingConfigurationSection)configuration?.GetSection("pattersonConfiguration/stagingConfiguration");

            if (stagingExcludedTypes == null)
            {
                return;
            }

            foreach (ExcludedTypesElement excludedType in stagingExcludedTypes.ExcludedTypesElementCollection)
            {
                excludedTypes.Add(excludedType.Name);
            }

            foreach (ExcludedMediaLibraryElement mediaLibrary in stagingExcludedTypes.ExcludedMediaLibraryElementCollection)
            {
                excludedMediaLibraries.Add(mediaLibrary.Code);
            }
        }

        protected override void OnInit()
        {
            base.OnInit();
            StagingEvents.LogTask.Before += LogTask_Before;
            StagingEvents.GetChildProcessingType.Execute += Staging_GetChildProcessingType;
            this.EnableTrackingOfCustomModuleClasses();
        }

        /// <summary>
        /// Stops user objects being created into staging tasks.
        /// </summary>
        /// <remarks>
        /// This prevents data of specific types from being able to be staged from one environment to another.
        /// </remarks>
        /// <param name="sender">The calling object</param>
        /// <remarks>It is possible for the sender to be null</remarks>
        /// <param name="e">The StagingLogTaskEventArgs arguments</param>
        public void LogTask_Before(object sender, StagingLogTaskEventArgs e)
        {
            Guard.ArgumentNotNull(e, nameof(e));
            var objectType = e.Object?.TypeInfo?.ObjectType;

            if (e.Object?.TypeInfo == MediaFileInfo.TYPEINFO)
            {
                var parentLibrary = (MediaLibraryInfo)e.Object?.Parent;
                if (excludedMediaLibraries.Contains(parentLibrary?.LibraryName))
                {
                    LogAndCancelStagingEvent(e, objectType);
                    return;
                }
            }

            if ((!this.excludedTypes.Contains(objectType)) || (this.excludedTypes.FirstOrDefault(x => x == objectType) == null))
            {
                return;
            }

            LogAndCancelStagingEvent(e, objectType);
        }

        /// <summary>
        /// Prevents users in a role being synchronized
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void Staging_GetChildProcessingType(object sender, StagingChildProcessingTypeEventArgs e)
        {
            if (string.Equals(e.ParentObjectType, RoleInfo.OBJECT_TYPE) && string.Equals(e.ObjectType, UserRoleInfo.OBJECT_TYPE))
            {
                var message = $"Preventing users in a role being syncronized.";
                EventLogProvider.LogEvent(EventType.WARNING, "PattersonModule", "Protecting roles by not executing child tasks.", eventDescription: message);
                e.ProcessingType = IncludeToParentEnum.None;
            }
        }

        private static void LogAndCancelStagingEvent(CMSEventArgs e, string objectType)
        {
            var message = $"Preventing any {objectType} related changes being made into staging tasks.";
            EventLogProvider.LogEvent(EventType.WARNING, "PattersonModule", $"Protecting {objectType} by cancelling staging tasks.", message);
            e.Cancel();
        }
        /// <summary>
        /// Enable tracking of changes to custom module classes using Kentico's staging
        /// feature. By default, Kentico does not do this, which prevents deployment of customizations
        /// of these classes, like updates to custom fields.
        /// </summary>
        /// <remarks>
        /// Credit: http://devtrev.com/Trev-Tips-(Blog)/February-2019/Enabling-Module-Class-Changes-in-Staging-Linking
        /// </remarks>
        private void EnableTrackingOfCustomModuleClasses()
        {
            var syncSettings = DataClassInfo.TYPEINFO.SynchronizationSettings;
            syncSettings.ExcludedStagingColumns.Clear();
            syncSettings.LogCondition = CanSynchronizeClass;
        }

        /// <summary>
        /// Kentico will call this function to evaluate whether or not a Module Class
        /// should be synchronized, because it is used as the SynchronizationSettings.LogCondition delegate.
        /// Only objects for which this condition returns true are synchnronized using the
        /// Staging feature.
        /// </summary>
        /// <param name="classObjectInfo"></param>
        /// <returns>True if the class should be synchronized.</returns>
        /// <remarks>
        /// Classes without a module (i.e. Resource) are Page Types and BizForms.
        /// Credit: http://devtrev.com/Trev-Tips-(Blog)/February-2019/Enabling-Module-Class-Changes-in-Staging-Linking
        /// </remarks>
        /// <seealso cref="https://devnet.kentico.com/docs/11_0/api/html/P_CMS_DataEngine_SynchronizationSettings_LogCondition.htm"/>
        private static bool CanSynchronizeClass(BaseInfo classObjectInfo)
        {
            int classResourceId = ((DataClassInfo)classObjectInfo).ClassResourceID;
            if (classResourceId <= 0)
            {
                return true;
            }
            var resourceInfo = ResourceInfoProvider.GetResourceInfo(classResourceId);
            if (resourceInfo == null)
            {
                return false;
            }
            return true;
        }
    }
}