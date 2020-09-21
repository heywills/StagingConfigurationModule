using System.Collections.Generic;
using KenticoCommunity.StagingConfigurationModule.Models;

namespace KenticoCommunity.StagingConfigurationModule.Interfaces
{
    /// <summary>
    /// Provide the settings required by the StagingConfigurationModule.
    /// The settings include:
    /// - Excluded types
    /// - Media libraries for which to excluded media files
    /// - Excluded child types
    /// </summary>
    public interface ISettingsRepository
    {
        /// <summary>
        /// Get the list of Kentico Xperience object types to exclude from staging.
        /// </summary>
        /// <returns></returns>
        List<string> GetExcludedTypes();

        /// <summary>
        /// Get the list of Kentico Xperience parent/child object type relationships to exclude from staging. For example, if you
        /// want to prevent cms.userrole objects from being processed as a part of a staged cms.role, use the parent type
        /// "cms.role" and child type "cms.userrole".
        /// </summary>
        /// <returns></returns>
        List<ParentChildTypePair> GetExcludedChildTypes();

        /// <summary>
        /// Get the list of Kentico Xperience media library code names, for which to exclude media files.
        /// </summary>
        /// <returns></returns>
        List<string> GetExcludedMediaLibraries();
    }
}