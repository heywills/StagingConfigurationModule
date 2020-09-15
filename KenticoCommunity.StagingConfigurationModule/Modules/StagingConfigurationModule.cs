using CMS;
using CMS.Core;
using CMS.DataEngine;
using CMS.MediaLibrary;
using CMS.Synchronization;
using KenticoCommunity.StagingConfigurationModule.Modules;
using KenticoCommunity.StagingConfigurationModule.Interfaces;

[assembly: RegisterModule(typeof(StagingConfigurationModule))]

namespace KenticoCommunity.StagingConfigurationModule.Modules
{
    /// <summary>
    /// Custom Module to override Kentico staging task events
    /// </summary>
    public class StagingConfigurationModule : Module
    {
        private IStagingCustomizationHelper _stagingCustomizationModuleHelper;

        public StagingConfigurationModule() : base(nameof(StagingConfigurationModule))
        {
        }


        protected override void OnInit()
        {
            _stagingCustomizationModuleHelper = Service.Resolve<IStagingCustomizationHelper>();
            base.OnInit();

            StagingEvents.LogTask.Before += LogTaskBefore;
            StagingEvents.GetChildProcessingType.Execute += GetChildProcessingType;
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
        public void LogTaskBefore(object sender, StagingLogTaskEventArgs e)
        {
            if (_stagingCustomizationModuleHelper.IsExcludedMediaLibraryFile(e.Object))
            {
                var mediaFileInfo = e.Object as MediaFileInfo;
                var mediaLibraryInfo = mediaFileInfo?.Parent as MediaLibraryInfo;

                var message = $"Preventing creation of staging task for media file in an excluded library, {mediaLibraryInfo?.LibraryName}.";
                _stagingCustomizationModuleHelper.LogInformation("ExcludeMediaFile", message);
                e.Cancel();
                return;
            }

            if (_stagingCustomizationModuleHelper.IsExcludedObjectType(e.Object))
            {
                var message = $"Preventing creation of staging task for excluded type, {e.Object?.TypeInfo?.ObjectType}.";
                _stagingCustomizationModuleHelper.LogInformation("ExcludeObjectType", message);
                e.Cancel();
            }
        }

        /// <summary>
        /// Prevents users in a role being synchronized
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