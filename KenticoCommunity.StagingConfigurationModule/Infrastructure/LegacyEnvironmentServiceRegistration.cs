using CMS.Base;
using KenticoCommunity.StagingConfigurationModule.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace KenticoCommunity.StagingConfigurationModule.Infrastructure
{
    internal class LegacyEnvironmentServiceRegistration
    {
        public static void EnsureServiceRegistration()
        {
            if(IsRunningInCmsApp())
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

    }
}
