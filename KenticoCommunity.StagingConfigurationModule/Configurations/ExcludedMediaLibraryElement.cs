using System.Configuration;

namespace KenticoCommunity.StagingConfigurationModule.Configurations
{
    public class ExcludedMediaLibraryElement : ConfigurationElement
    {
        private const string CodeAttributeName = "code";

        [ConfigurationProperty(CodeAttributeName, DefaultValue = "", IsKey = true, IsRequired = true)]
        public string Code
        {
            get => (string)this[CodeAttributeName];
            set => this[CodeAttributeName] = value;
        }
    }
}
