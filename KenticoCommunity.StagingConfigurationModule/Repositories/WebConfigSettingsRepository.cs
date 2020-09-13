using CMS;
using KenticoCommunity.StagingConfigurationModule.Configurations;
using KenticoCommunity.StagingConfigurationModule.Interfaces;
using KenticoCommunity.StagingConfigurationModule.Models;
using KenticoCommunity.StagingConfigurationModule.Repositories;
using System.Collections.Generic;
using System.Linq;

[assembly: RegisterImplementation(typeof(ISettingsRepository), typeof(WebConfigSettingsRepository))]

namespace KenticoCommunity.StagingConfigurationModule.Repositories
{
    internal class WebConfigSettingsRepository:ISettingsRepository
    {
        private readonly StagingConfigurationSection _stagingConfigurationSection;

        public WebConfigSettingsRepository(IConfigurationHelper configurationHelper)
        {
            var configuration = configurationHelper.GetWebConfiguration();
            _stagingConfigurationSection =
                configuration?.GetSection(StagingConfigurationSection.StagingConfigurationSectionName) as
                    StagingConfigurationSection;

        }

        public List<string> GetExcludedTypes()
        {
            if (_stagingConfigurationSection != null)
            {
                return _stagingConfigurationSection.ExcludedTypesElementCollection
                    .Where(x => !string.IsNullOrWhiteSpace(x.Name)).Select(x => x.Name.Trim()).ToList();
            }
            else
            {
                return new List<string>();
            }
        }

        public List<ParentChildTypePair> GetExcludedChildTypes()
        {
            if (_stagingConfigurationSection != null)
            {
                return _stagingConfigurationSection.ExcludedChildTypeElementCollection
                    .Where(x => (!(string.IsNullOrWhiteSpace(x.ParentType) || string.IsNullOrWhiteSpace(x.ChildType))))
                    .Select(x => new ParentChildTypePair()
                        {ParentType = x.ParentType.Trim(), ChildType = x.ChildType.Trim()})
                    .ToList();
            }
            else
            {
                return new List<ParentChildTypePair>();
            }
        }

        public List<string> GetExcludedMediaLibraries()
        {
            if (_stagingConfigurationSection != null)
            {
                return _stagingConfigurationSection.ExcludedMediaLibraryElementCollection
                    .Where(x => !string.IsNullOrWhiteSpace(x.Code)).Select(x => x.Code.Trim()).ToList();
            }
            else
            {
                return new List<string>();
            }
        }
    }
}
