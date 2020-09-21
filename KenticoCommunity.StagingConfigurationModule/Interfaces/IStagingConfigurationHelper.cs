using CMS.DataEngine;
using CMS.Synchronization;

namespace KenticoCommunity.StagingConfigurationModule.Interfaces
{
    /// <summary>
    /// Provide helper functions for the StagingConfigurationModule that check whether or not an IInfo object is of an excluded
    /// type, excluded child type, or a media file that belongs to an excluded library.
    /// </summary>
    public interface IStagingConfigurationHelper
    {
        /// <summary>
        /// Test if the provided IInfo is a MediaFileInfo object. If it is, see if it belongs to a media library that is in the
        /// excluded media libraries list.
        /// </summary>
        /// <param name="infoObject"></param>
        /// <returns>
        /// Returns true if the IInfo is a MediaFileInfo object and belongs to a Media Library that's in the exclusion
        /// list.
        /// </returns>
        bool IsExcludedMediaLibraryFile(IInfo infoObject);

        /// <summary>
        /// Test if the provided IInfo is of a type that is in the the excluded object type list.
        /// </summary>
        /// <param name="infoObject"></param>
        /// <returns>Returns true if the IInfo is of a type in the exclusion list.</returns>
        bool IsExcludedObjectType(IInfo infoObject);

        /// <summary>
        /// Test if the provided StagingChildProcessingTypeEventArgs represents a parent type/child type relationship that is in
        /// the exclusion list.
        /// </summary>
        /// <param name="eventArgs"></param>
        /// <returns>Returns true if the parent/child type relationship is in the exclusion list.</returns>
        bool IsExcludedChildType(StagingChildProcessingTypeEventArgs eventArgs);

        /// <summary>
        /// Log information for the Staging Configuration Module.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="message"></param>
        void LogInformation(string source, string message);
    }
}