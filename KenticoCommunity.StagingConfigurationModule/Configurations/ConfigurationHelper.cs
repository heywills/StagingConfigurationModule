using CMS;
using CMS.Base;
using KenticoCommunity.StagingConfigurationModule.Configurations;
using KenticoCommunity.StagingConfigurationModule.Interfaces;
using System.Configuration;
using System.IO;
using System.Web.Configuration;

[assembly: RegisterImplementation(typeof(IConfigurationHelper), typeof(ConfigurationHelper))]

namespace KenticoCommunity.StagingConfigurationModule.Configurations
{
    public class ConfigurationHelper : IConfigurationHelper
    {
        /// <summary>
        /// Get the .NET Configuration object for the CMSApp app's web.config. This will
        /// load the web.config file whether running in the Kentico Web App or running in
        /// ContinuousIntegration.exe
        /// </summary>
        /// <returns></returns>
        public Configuration GetWebConfiguration()
        {
            var webDirectoryPath = SystemContext.WebApplicationPhysicalPath;
            return OpenConfiguration(webDirectoryPath);
        }

        public Configuration OpenConfiguration(string appPath, string configFileName = "web.config")
        {
            var mapping = new VirtualDirectoryMapping(appPath, true, configFileName);
            var webConfigurationFileMap = new WebConfigurationFileMap();
            webConfigurationFileMap.VirtualDirectories.Add(string.Empty, mapping);
            try
            {
                return WebConfigurationManager.OpenMappedWebConfiguration(webConfigurationFileMap, string.Empty);
            }
            catch
            {
                return ConfigurationManager.OpenMappedExeConfiguration(new ExeConfigurationFileMap
                {
                    ExeConfigFilename = Path.Combine(appPath, configFileName)
                }, ConfigurationUserLevel.None);
            }
        }
    }
}
