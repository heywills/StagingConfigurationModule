using KenticoCommunity.StagingConfigurationModule.Models.Settings;
using KenticoCommunity.StagingConfigurationModule.Interfaces;
using KenticoCommunity.StagingConfigurationModule.Models;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;

namespace KenticoCommunity.StagingConfigurationModule.Repositories
{
    /// <summary>
    /// Provide the settings required by the StagingConfigurationModule from the .NET Core appsettings file 
    /// The settings include:
    /// - Excluded types
    /// - Media libraries for which to excluded media files
    /// - Excluded child types
    /// Use the following snippet to add configurations to an appsettings.json file:
    ///
    ///  "stagingConfiguration": {
    ///    "sourceServer": {
    ///      "excludedTypes": [
    ///        "cms.user",
    ///        "cms.userculture"
    ///      ],
    ///      "excludedMediaLibraries": [
    ///        "templateImages"
    ///      ]
    ///    },
    ///    "targetServer": {
    ///        "excludedChildTypes": [
    ///        {
    ///          "parentType": "cms.role",
    ///          "childType": "cms.userrole"
    ///        }
    ///      ]
    ///    }
    ///  }
    ///
    /// </summary>
    internal class AppSettingsRepository : ISettingsRepository
    {
        private readonly SourceServerSettings _sourceServerSettings;
        private readonly TargetServerSettings _targetServerSettings;

        public AppSettingsRepository(IOptions<StagingConfigurationSettings> stagingConfigurationSettingsOptions)
        {
            var stagingConfigurationSettings = stagingConfigurationSettingsOptions?.Value;
            _sourceServerSettings = stagingConfigurationSettings?.SourceServer;
            _targetServerSettings = stagingConfigurationSettings?.TargetServer;
        }

        /// <summary>
        /// Get the list of Kentico Xperience object types to exclude from staging.
        /// </summary>
        /// <returns></returns>
        public List<string> GetExcludedTypes()
        {
            if ((_sourceServerSettings != null) && (_sourceServerSettings.ExcludedTypes != null))
                return _sourceServerSettings.ExcludedTypes
                    .Where(x => !string.IsNullOrWhiteSpace(x)).Select(x => x.Trim()).ToList();
            return new List<string>();
        }

        /// <summary>
        /// Get the list of Kentico Xperience parent/child object type relationships to exclude from staging. For example, if you
        /// want to prevent cms.userrole objects from being processed as a part of a staged cms.role, use the parent type
        /// "cms.role" and child type "cms.userrole".
        /// </summary>
        /// <returns></returns>
        public List<ParentChildTypePair> GetExcludedChildTypes()
        {
            if ((_targetServerSettings != null) && (_targetServerSettings.ExcludedChildTypes != null))
                return _targetServerSettings.ExcludedChildTypes
                    .Where(x => !(string.IsNullOrWhiteSpace(x.ParentType) || string.IsNullOrWhiteSpace(x.ChildType)))
                    .Select(x => new ParentChildTypePair
                        {ParentType = x.ParentType.Trim(), ChildType = x.ChildType.Trim()})
                    .ToList();
            return new List<ParentChildTypePair>();
        }

        /// <summary>
        /// Get the list of Kentico Xperience media library code names, for which to exclude media files.
        /// </summary>
        /// <returns></returns>
        public List<string> GetExcludedMediaLibraries()
        {
            if ((_sourceServerSettings != null) && (_sourceServerSettings.ExcludedMediaLibraries != null))
                return _sourceServerSettings.ExcludedMediaLibraries
                    .Where(x => !string.IsNullOrWhiteSpace(x)).Select(x => x.Trim()).ToList();
            return new List<string>();
        }
    }
}