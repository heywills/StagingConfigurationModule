using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KenticoCommunity.StagingConfigurationModule.Models;

namespace KenticoCommunity.StagingConfigurationModule.Interfaces
{
    public interface ISettingsRepository
    {
        List<string> GetExcludedTypes();

        List<string> GetExcludedMediaLibraries();

        List<ParentChildTypePair> GetExcludedChildTypes();
    }
}
