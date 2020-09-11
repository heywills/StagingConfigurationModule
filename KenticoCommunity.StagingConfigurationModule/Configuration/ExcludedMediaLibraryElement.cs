using System.Configuration;

namespace KenticoCommunity.StagingConfigurationModule.Configuration
{
    public class ExcludedMediaLibraryElement : ConfigurationElement
    {
        [ConfigurationProperty("code", DefaultValue = "", IsKey = true, IsRequired = true)]
        public string Code
        {
            get => (string)this["code"];
            set => this["code"] = value;
        }
    }
}
