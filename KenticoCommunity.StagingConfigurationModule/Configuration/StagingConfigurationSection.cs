using System.Configuration;

namespace KenticoCommunity.StagingConfigurationModule.Configuration
{

    public class StagingConfigurationSection : ConfigurationSection
    {
        [ConfigurationProperty("excludedTypes", IsDefaultCollection = true)]
        public ExcludedTypesElementCollection ExcludedTypesElementCollection
        {
            get => (ExcludedTypesElementCollection)this["excludedTypes"];
            set => this["excludedTypes"] = value;
        }

        [ConfigurationProperty("excludedMediaLibraries", IsDefaultCollection = false)]
        public ExcludedMediaLibraryElementCollection ExcludedMediaLibraryElementCollection
        {
            get => (ExcludedMediaLibraryElementCollection)this["excludedMediaLibraries"];
            set => this["excludedMediaLibraries"] = value;
        }
    }
}