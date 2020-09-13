
using CMS.DataEngine;
using CMS.Synchronization;

namespace KenticoCommunity.StagingConfigurationModule.Interfaces
{
    public interface IStagingCustomizationHelper
    {
        bool IsExcludedMediaLibraryFile(IInfo infoObject);
        bool IsExcludedObjectType(IInfo infoObject);

        bool IsExcludedChildType(StagingChildProcessingTypeEventArgs eventArgs);

        void LogInformation(string source, string message);
    }
}
