using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KenticoCommunity.StagingConfigurationModule.Interfaces
{
    interface ISettingsRepository
    {
        List<string> GetExcludedTypes();

        List<string> GetExcludedMediaLibraries();
    }
}
