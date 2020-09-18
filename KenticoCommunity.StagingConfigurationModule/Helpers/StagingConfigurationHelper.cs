using CMS;
using CMS.Core;
using CMS.DataEngine;
using CMS.EventLog;
using CMS.MediaLibrary;
using CMS.Synchronization;
using KenticoCommunity.StagingConfigurationModule.Helpers;
using KenticoCommunity.StagingConfigurationModule.Interfaces;
using KenticoCommunity.StagingConfigurationModule.Models;
using System;
using System.Collections.Generic;
using System.Linq;

[assembly: RegisterImplementation(typeof(IStagingConfigurationHelper), typeof(StagingConfigurationHelper))]

namespace KenticoCommunity.StagingConfigurationModule.Helpers
{
    /// <summary>
    /// Provide helper functions for the StagingConfigurationModule that use the data from the SettingsRepository to check whether or not an IInfo object is of an excluded type, excluded child type, or a media file that belongs to an excluded library.
    /// </summary>
    internal class StagingConfigurationHelper : IStagingConfigurationHelper
    {
        private const string ModuleName = "StagingConfigurationModule";
        private readonly IEventLogService _eventLogService;
        private readonly List<string> _excludedTypes;
        private readonly List<ParentChildTypePair> _excludedChildTypes;
        private readonly List<string> _excludedMediaLibraries;

        public StagingConfigurationHelper(ISettingsRepository settingRepository, IEventLogService eventLogService)
        {
            var settingsRepository = settingRepository;
            _eventLogService = eventLogService;
            _excludedTypes = settingsRepository.GetExcludedTypes();
            _excludedChildTypes = settingsRepository.GetExcludedChildTypes();
            _excludedMediaLibraries = settingsRepository.GetExcludedMediaLibraries();
        }

        /// <summary>
        /// Test if the provided IInfo is a MediaFileInfo object. If it is, see if it belongs to a media library that is in the excluded media libraries list.
        /// </summary>
        /// <param name="infoObject"></param>
        /// <returns>Returns true if the IInfo is a MediaFileInfo object and belongs to a Media Library that's in the exclusion list.</returns>
        public bool IsExcludedMediaLibraryFile(IInfo infoObject)
        {
            if (!(infoObject is MediaFileInfo mediaFileInfo))
            {
                return false;
            }
            var mediaLibraryInfo = (MediaLibraryInfo)mediaFileInfo.Parent;
            return _excludedMediaLibraries.Contains(mediaLibraryInfo.LibraryName, StringComparer.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Test if the provided IInfo is of a type that is in the the excluded object type list.
        /// </summary>
        /// <param name="infoObject"></param>
        /// <returns>Returns true if the IInfo is of a type in the exclusion list.</returns>
        public bool IsExcludedObjectType(IInfo infoObject)
        {
            var typeInfo = infoObject.TypeInfo;
            var objectType = typeInfo.ObjectType;
            return _excludedTypes.Contains(objectType, StringComparer.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Test if the provided StagingChildProcessingTypeEventArgs represents a parent type/child type relationship that is in the exclusion list.
        /// </summary>
        /// <param name="eventArgs"></param>
        /// <returns>Returns true if the parent/child type relationship is in the exclusion list.</returns>
        public bool IsExcludedChildType(StagingChildProcessingTypeEventArgs eventArgs)
        {
            var currentParentType = eventArgs.ParentObjectType;
            var currentChildType = eventArgs.ObjectType;
            return _excludedChildTypes.Exists(pair =>
                (pair.ParentType.Equals(currentParentType, StringComparison.OrdinalIgnoreCase) &&
                 pair.ChildType.Equals(currentChildType, StringComparison.OrdinalIgnoreCase)));
        }

        /// <summary>
        /// Log information to the Kentico Event log for the Staging Configuration Module.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="message"></param>
        public void LogInformation(string source, string message)
        {
            _eventLogService.LogEvent(EventType.INFORMATION, ModuleName, source, message);
        }
    }
}
