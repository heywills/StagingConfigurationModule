using System.Configuration;

namespace KenticoCommunity.StagingConfigurationModule.Configurations
{
    /// <summary>
    /// Represents an element in the configuration for storing a parent type/child type relationship
    /// </summary>
    public class ExcludedChildTypeElement : ConfigurationElement
    {
        private const string ParentTypeAttributeName = "parentType";
        private const string ChildTypeAttributeName = "childType";

        [ConfigurationProperty(ParentTypeAttributeName, DefaultValue = "", IsKey = false, IsRequired = true)]
        public string ParentType
        {
            get => (string) this[ParentTypeAttributeName];
            set => this[ParentTypeAttributeName] = value;
        }

        [ConfigurationProperty(ChildTypeAttributeName, DefaultValue = "", IsKey = false, IsRequired = true)]
        public string ChildType
        {
            get => (string) this[ChildTypeAttributeName];
            set => this[ChildTypeAttributeName] = value;
        }
    }
}