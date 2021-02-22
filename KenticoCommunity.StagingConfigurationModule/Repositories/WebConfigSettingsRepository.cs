using System.Collections.Generic;
using System.Linq;
using KenticoCommunity.StagingConfigurationModule.Configurations;
using KenticoCommunity.StagingConfigurationModule.Interfaces;
using KenticoCommunity.StagingConfigurationModule.Models;

namespace KenticoCommunity.StagingConfigurationModule.Repositories
{
    /// <summary>
    /// Provide the settings required by the StagingConfigurationModule from the Kentico web app's web.config file (even if
    /// being run in ContinuousIntegration.exe). The settings include:
    /// - Excluded types
    /// - Media libraries for which to excluded media files
    /// - Excluded child types
    /// Use the following snippet to add configurations to the web.config:
    /// <configSections>
    ///     <section name="stagingConfiguration"
    ///         type="KenticoCommunity.StagingConfigurationModule.Configurations.StagingConfigurationSection,KenticoCommunity.StagingConfigurationModule" />
    /// </configSections>
    /// <stagingConfiguration>
    ///     <sourceServer>
    ///         <excludedTypes>
    ///             <type name="cms.user" />
    ///             <type name="cms.userculture" />
    ///         </excludedTypes>
    ///         <excludedMediaLibraries>
    ///             <mediaLibrary code="templateImages" />
    ///         </excludedMediaLibraries>
    ///     </sourceServer>
    ///     <targetServer>
    ///         <excludedChildTypes>
    ///             <childType parentType="cms.role" childType="cms.userrole" />
    ///         </excludedChildTypes>
    ///     </targetServer>
    /// </stagingConfiguration>
    /// </summary>
    internal class WebConfigSettingsRepository : ISettingsRepository
    {
        private readonly SourceServerElement _sourceServerElement;
        private readonly TargetServerElement _targetServerSection;

        public WebConfigSettingsRepository(IConfigurationHelper configurationHelper)
        {
            var configuration = configurationHelper.GetWebConfiguration();
            var stagingConfigurationSection =
                configuration?.GetSection(StagingConfigurationSection.StagingConfigurationSectionName) as
                    StagingConfigurationSection;
            _sourceServerElement = stagingConfigurationSection?.SourceServerElement;
            _targetServerSection = stagingConfigurationSection?.TargetServerSection;
        }

        /// <summary>
        /// Get the list of Kentico Xperience object types to exclude from staging.
        /// </summary>
        /// <returns></returns>
        public List<string> GetExcludedTypes()
        {
            if (_sourceServerElement != null)
                return _sourceServerElement.ExcludedTypesElementCollection
                    .Where(x => !string.IsNullOrWhiteSpace(x.Name)).Select(x => x.Name.Trim()).ToList();
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
            if (_targetServerSection != null)
                return _targetServerSection.ExcludedChildTypeElementCollection
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
            if (_sourceServerElement != null)
                return _sourceServerElement.ExcludedMediaLibraryElementCollection
                    .Where(x => !string.IsNullOrWhiteSpace(x.Code)).Select(x => x.Code.Trim()).ToList();
            return new List<string>();
        }
    }
}