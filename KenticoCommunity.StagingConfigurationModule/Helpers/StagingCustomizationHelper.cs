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

[assembly: RegisterImplementation(typeof(IStagingCustomizationHelper), typeof(StagingCustomizationHelper))]

namespace KenticoCommunity.StagingConfigurationModule.Helpers
{
    internal class StagingCustomizationHelper : IStagingCustomizationHelper
    {
        private const string ModuleName = "StagingCustomizationModule";
        private readonly IEventLogService _eventLogService;
        private readonly List<string> _excludedTypes;
        private readonly List<ParentChildTypePair> _excludedChildTypes;
        private readonly List<string> _excludedMediaLibraries;

        public StagingCustomizationHelper(ISettingsRepository settingRepository, IEventLogService eventLogService)
        {
            var settingsRepository = settingRepository;
            _eventLogService = eventLogService;
            _excludedTypes = settingsRepository.GetExcludedTypes();
            _excludedChildTypes = settingsRepository.GetExcludedChildTypes();
            _excludedMediaLibraries = settingsRepository.GetExcludedMediaLibraries();
        }

        public bool IsExcludedMediaLibraryFile(IInfo infoObject)
        {
            if (!(infoObject is MediaFileInfo mediaFileInfo))
            {
                return false;
            }
            var mediaLibraryInfo = (MediaLibraryInfo)mediaFileInfo.Parent;
            return _excludedMediaLibraries.Contains(mediaLibraryInfo.LibraryName, StringComparer.OrdinalIgnoreCase);
        }

        public bool IsExcludedObjectType(IInfo infoObject)
        {
            var typeInfo = infoObject.TypeInfo;
            var objectType = typeInfo.ObjectType;
            return _excludedTypes.Contains(objectType, StringComparer.OrdinalIgnoreCase);
        }

        public bool IsExcludedChildType(StagingChildProcessingTypeEventArgs eventArgs)
        {
            var currentParentType = eventArgs.ParentObjectType;
            var currentChildType = eventArgs.ObjectType;
            return _excludedChildTypes.Exists(pair =>
                (pair.ParentType.Equals(currentParentType, StringComparison.OrdinalIgnoreCase) &&
                 pair.ChildType.Equals(currentChildType, StringComparison.OrdinalIgnoreCase)));
        }

        public void LogInformation(string source, string message)
        {
            _eventLogService.LogEvent(EventType.INFORMATION, ModuleName, source, message);
        }
    }
}
