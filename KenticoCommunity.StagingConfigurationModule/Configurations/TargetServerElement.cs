using System.Configuration;

namespace KenticoCommunity.StagingConfigurationModule.Configurations
{
    public class TargetServerElement : ConfigurationElement
    {
        private const string ExcludedChildTypeCollectionName = "excludedChildTypes";

        [ConfigurationProperty(ExcludedChildTypeCollectionName, IsDefaultCollection = false)]
        public ExcludedChildTypeElementCollection ExcludedChildTypeElementCollection
        {
            get => (ExcludedChildTypeElementCollection)this[ExcludedChildTypeCollectionName];
            set => this[ExcludedChildTypeCollectionName] = value;
        }
    }
}
