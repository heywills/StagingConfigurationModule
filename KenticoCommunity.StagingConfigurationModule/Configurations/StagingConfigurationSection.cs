using System.Configuration;

namespace KenticoCommunity.StagingConfigurationModule.Configurations
{

    public class StagingConfigurationSection : ConfigurationSection
    {
        public const string StagingConfigurationSectionName = "stagingConfiguration";
        private const string ExcludedTypeCollectionName = "excludedTypes";
        private const string ExcludedMediaLibrariesCollectionName = "excludedMediaLibraries";
        private const string ExcludedChildTypeCollectionName = "excludedChildTypes";

        [ConfigurationProperty(ExcludedTypeCollectionName, IsDefaultCollection = true)]
        public ExcludedTypeElementCollection ExcludedTypesElementCollection
        {
            get => (ExcludedTypeElementCollection)this[ExcludedTypeCollectionName];
            set => this[ExcludedTypeCollectionName] = value;
        }

        [ConfigurationProperty(ExcludedMediaLibrariesCollectionName, IsDefaultCollection = false)]
        public ExcludedMediaLibraryElementCollection ExcludedMediaLibraryElementCollection
        {
            get => (ExcludedMediaLibraryElementCollection)this[ExcludedMediaLibrariesCollectionName];
            set => this[ExcludedMediaLibrariesCollectionName] = value;
        }

        [ConfigurationProperty(ExcludedChildTypeCollectionName, IsDefaultCollection = false)]
        public ExcludedChildTypeElementCollection ExcludedChildTypeElementCollection
        {
            get => (ExcludedChildTypeElementCollection)this[ExcludedChildTypeCollectionName];
            set => this[ExcludedChildTypeCollectionName] = value;
        }
    }
}