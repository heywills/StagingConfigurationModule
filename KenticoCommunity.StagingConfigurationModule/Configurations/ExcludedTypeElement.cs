using System.Configuration;

namespace KenticoCommunity.StagingConfigurationModule.Configurations
{
    public class ExcludedTypeElement : ConfigurationElement
    {
        private const string NameAttributeName = "name";

        [ConfigurationProperty(NameAttributeName, DefaultValue = "", IsKey = true, IsRequired = true)]
        public string Name
        {
            get => (string)this[NameAttributeName];
            set => this[NameAttributeName] = value;
        }
    }
}
