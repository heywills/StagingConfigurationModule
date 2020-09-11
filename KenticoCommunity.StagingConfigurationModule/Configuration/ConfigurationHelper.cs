using System.Configuration;
using System.IO;
using System.Web.Configuration;
using CMS.Base;

namespace KenticoCommunity.StagingConfigurationModule.Configuration
{
    public class ConfigurationHelper
    {
        /// <summary>
        /// Get the .NET Configuration object for the CMSApp app's web.config. This will
        /// load the web.config file whether running in the Kentico Web App or running in
        /// ContinuousIntegration.exe
        /// </summary>
        /// <returns></returns>
        public static System.Configuration.Configuration GetWebConfiguration()
        {
            var webDirectoryPath = SystemContext.WebApplicationPhysicalPath;
            return OpenConfiguration(webDirectoryPath);
        }

        internal static System.Configuration.Configuration OpenConfiguration(string appPath)
        {
            VirtualDirectoryMapping mapping = new VirtualDirectoryMapping(appPath, true, "web.config");
            WebConfigurationFileMap webConfigurationFileMap = new WebConfigurationFileMap();
            webConfigurationFileMap.VirtualDirectories.Add(string.Empty, mapping);
            try
            {
                return WebConfigurationManager.OpenMappedWebConfiguration(webConfigurationFileMap, string.Empty);
            }
            catch
            {
                return ConfigurationManager.OpenMappedExeConfiguration(new ExeConfigurationFileMap
                {
                    ExeConfigFilename = Path.Combine(appPath, "web.config")
                }, ConfigurationUserLevel.None);
            }
        }
    }
}
