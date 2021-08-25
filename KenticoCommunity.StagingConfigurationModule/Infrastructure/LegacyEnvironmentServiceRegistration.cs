using CMS.Base;
using System.Diagnostics;
using KenticoCommunity.StagingConfigurationModule.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace KenticoCommunity.StagingConfigurationModule.Infrastructure
{
    internal class LegacyEnvironmentServiceRegistration
    {
        public static void EnsureServiceRegistration()
        {
            if(IsRunningInCmsApp() || IsRunningExternal())
            {
                var serviceCollection = new ServiceCollection();
                serviceCollection.AddStagingConfigurationModuleServices();
                serviceCollection.RegisterWithKenticoServiceLocator();
            }
        }

        private static bool IsRunningInCmsApp()
        {
            return (SystemContext.IsCMSRunningAsMainApplication && SystemContext.IsWebSite);
        }

        /// <summary>
        /// Return true, if not running in the context of a web site.
        /// </summary>
        private static bool IsRunningExternal()
        {
            return !SystemContext.IsWebSite;
        }

    }
}
