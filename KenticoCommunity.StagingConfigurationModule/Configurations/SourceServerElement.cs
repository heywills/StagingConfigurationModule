using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KenticoCommunity.StagingConfigurationModule.Configurations
{
    public class SourceServerElement :  ConfigurationElement
    {
        private const string ExcludedTypeCollectionName = "excludedTypes";
        private const string ExcludedMediaLibrariesCollectionName = "excludedMediaLibraries";

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
    }
}
