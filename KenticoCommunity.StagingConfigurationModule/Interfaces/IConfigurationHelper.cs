using System.Configuration;

namespace KenticoCommunity.StagingConfigurationModule.Interfaces
{
    public interface IConfigurationHelper
    {
        /// <summary>
        /// Get the .NET Configuration object for the CMSApp app's web.config. This will
        /// load the web.config file whether running in the Kentico Web App or running in
        /// ContinuousIntegration.exe
        /// </summary>
        /// <returns></returns>
        Configuration GetWebConfiguration();

        Configuration OpenConfiguration(string appPath, string configFileName = "web.config");
    }
}