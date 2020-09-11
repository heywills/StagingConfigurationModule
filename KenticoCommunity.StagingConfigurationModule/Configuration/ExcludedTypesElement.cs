using System.Configuration;

namespace KenticoCommunity.StagingConfigurationModule.Configuration
{
    public class ExcludedTypesElement : ConfigurationElement
    {
        [ConfigurationProperty("name", DefaultValue = "", IsKey = true, IsRequired = true)]
        public string Name
        {
            get => (string)this["name"];
            set => this["name"] = value;
        }
    }
}
