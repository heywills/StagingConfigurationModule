using System.Configuration;

namespace KenticoCommunity.StagingConfigurationModule.Configurations
{
    /// <summary>
    /// Represents a configuration element for holding the code name of a Media Library to be excluded from staging task creation on the source server.
    /// </summary>
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
