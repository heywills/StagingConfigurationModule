using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using KenticoCommunity.StagingConfigurationModule.Configuration;
using KenticoCommunity.StagingConfigurationModule.Interfaces;

namespace KenticoCommunity.StagingConfigurationModule.Repositories
{
    internal class WebConfigSettingsRepository:ISettingsRepository
    {
        private System.Configuration.Configuration _configuration;

        public WebConfigSettingsRepository()
        {
            _configuration = ConfigurationHelper.GetWebConfiguration();
        }

        public List<string> GetExcludedTypes()
        {
            if (_configuration?.GetSection("pattersonConfiguration/stagingConfiguration") is StagingConfigurationSection
                stagingConfigurationSection)
            {
                return (List<string>)stagingConfigurationSection.ExcludedTypesElementCollection.Where(x => !string.IsNullOrEmpty(x.Name)).Select(x => x.Name);
            }
            else
            {
                return new List<string>();
            }
        }

        public List<string> GetExcludedMediaLibraries()
        {
            throw new NotImplementedException();
        }
    }
}
