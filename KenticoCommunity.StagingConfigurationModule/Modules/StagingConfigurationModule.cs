using CMS;
using CMS.Core;
using CMS.DataEngine;
using CMS.MediaLibrary;
using CMS.Synchronization;
using KenticoCommunity.StagingConfigurationModule.Interfaces;
using KenticoCommunity.StagingConfigurationModule.Modules;

[assembly: RegisterModule(typeof(StagingConfigurationModule))]

namespace KenticoCommunity.StagingConfigurationModule.Modules
{
    /// <summary>
    /// This module reads configurable settings to limit the Kentico objects that are staged from a source environment to a
    /// target environment. If configured correctly, it eliminates the need to maintain lists of objects that need to be
    /// deployed with each build. By excluding the objects that you would never want to deploy, you are able to synchronize
    /// all staging tasks whenever a deployment is required. This eliminates human error, the oops-I-forgot-to-deploy-X
    /// scenario. This module should be installed in a source environment so that it can use the
    /// `StagingEvents.LogTask.Before` event to limit the types of Kentico objects that create staging tasks. Additionally,
    /// it should be installed on the target environment so that it can use the
    /// `StagingEvents.GetChildProcessingType.Execute` event , to control how child relationships are processed on the
    /// target server.
    /// </summary>
    public class StagingConfigurationModule : Module
    {
        private IStagingConfigurationHelper _stagingCustomizationModuleHelper;

        public StagingConfigurationModule() : base(nameof(StagingConfigurationModule))
        {
        }

        /// <summary>
        /// Initialize the module by creating the StagingCustomizationHelper and setting up the global system events.
        /// </summary>
        /// <remarks>
        /// The first dependency is created using Service.Resolve, which uses the DI container. However, all other dependencies in
        /// the chain will be created automatically using constructor-based injection.
        /// </remarks>
        protected override void OnInit()
        {
            _stagingCustomizationModuleHelper = Service.Resolve<IStagingConfigurationHelper>();
            base.OnInit();

            StagingEvents.LogTask.Before += LogTaskBefore;
            StagingEvents.GetChildProcessingType.Execute += GetChildProcessingType;
        }

        /// <summary>
        /// Stop staging tasks from being created for any object type that is in the excluded-types list. This prevents tasks from
        /// being created in the staging source environment.
        /// </summary>
        /// <param name="sender">The calling object</param>
        /// <param name="e">The StagingLogTaskEventArgs arguments</param>
        public void LogTaskBefore(object sender, StagingLogTaskEventArgs e)
        {
            if (_stagingCustomizationModuleHelper.IsExcludedMediaLibraryFile(e.Object))
            {
                var mediaFileInfo = e.Object as MediaFileInfo;
                var mediaLibraryInfo = mediaFileInfo?.Parent as MediaLibraryInfo;

                var message =
                    $"Preventing creation of staging task for media file in an excluded library, {mediaLibraryInfo?.LibraryName}.";
                _stagingCustomizationModuleHelper.LogInformation("ExcludeMediaFile", message);
                e.Cancel();
                return;
            }

            if (_stagingCustomizationModuleHelper.IsExcludedObjectType(e.Object))
            {
                var message =
                    $"Preventing creation of staging task for excluded type, {e.Object?.TypeInfo?.ObjectType}.";
                _stagingCustomizationModuleHelper.LogInformation("ExcludeObjectType", message);
                e.Cancel();
            }
        }

        /// <summary>
        /// Modify how object relationships are processed in the target environment, by changing the `ProcessingType` from
        /// `IncludeToParentEnum.Complete` to `IncludeToParentEnum.None`.  This will prevent a staging task from wiping out the
        /// child collection of an object, like deleting all the user relationships in a role.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        public void GetChildProcessingType(object sender, StagingChildProcessingTypeEventArgs eventArgs)
        {
            var currentParentType = eventArgs.ParentObjectType;
            var currentChildType = eventArgs.ObjectType;
            if (_stagingCustomizationModuleHelper.IsExcludedChildType(eventArgs))
            {
                var message = $"Preventing {currentChildType} from being synchronized with {currentParentType}.";
                _stagingCustomizationModuleHelper.LogInformation("ExcludeChildType", message);
                eventArgs.ProcessingType = IncludeToParentEnum.None;
            }
        }
    }
}