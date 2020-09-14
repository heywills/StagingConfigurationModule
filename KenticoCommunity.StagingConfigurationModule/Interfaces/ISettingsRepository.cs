using KenticoCommunity.StagingConfigurationModule.Models;
using System.Collections.Generic;

namespace KenticoCommunity.StagingConfigurationModule.Interfaces
{
    public interface ISettingsRepository
    {
        List<string> GetExcludedTypes();

        List<string> GetExcludedMediaLibraries();

        List<ParentChildTypePair> GetExcludedChildTypes();
    }
}
