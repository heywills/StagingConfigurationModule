using System.Configuration;

namespace KenticoCommunity.StagingConfigurationModule.Configurations
{

    /// <summary>
    /// A configuration section for containing settings for the Staging Configuration Module.
    /// </summary>
    public class StagingConfigurationSection : ConfigurationSection
    {
        public const string StagingConfigurationSectionName = "stagingConfiguration";
        public const string SourceServerSectionName = "sourceServer";
        public const string TargetServerSectionName = "targetServer";

        [ConfigurationProperty(SourceServerSectionName)]
        public SourceServerElement SourceServerElement =>
            (this[SourceServerSectionName] as SourceServerElement);

        [ConfigurationProperty(TargetServerSectionName)]
        public TargetServerElement TargetServerSection =>
            (this[TargetServerSectionName] as TargetServerElement);
    }
}