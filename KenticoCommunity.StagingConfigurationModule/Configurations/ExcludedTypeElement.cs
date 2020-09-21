using System.Configuration;

namespace KenticoCommunity.StagingConfigurationModule.Configurations
{
    /// <summary>
    /// Represents a configuration element for holding the type name of an object to be excluded from staging task creation on
    /// the source server.
    /// </summary>
    public class ExcludedTypeElement : ConfigurationElement
    {
        private const string NameAttributeName = "name";

        [ConfigurationProperty(NameAttributeName, DefaultValue = "", IsKey = true, IsRequired = true)]
        public string Name
        {
            get => (string) this[NameAttributeName];
            set => this[NameAttributeName] = value;
        }
    }
}