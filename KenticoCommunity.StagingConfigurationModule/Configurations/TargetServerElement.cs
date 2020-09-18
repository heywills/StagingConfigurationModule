using System.Configuration;

namespace KenticoCommunity.StagingConfigurationModule.Configurations
{
    /// <summary>
    /// A configuration element for containing settings that belong to the target staging service.
    /// </summary>
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
