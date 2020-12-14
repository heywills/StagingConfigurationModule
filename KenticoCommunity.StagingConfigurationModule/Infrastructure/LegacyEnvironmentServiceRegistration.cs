using CMS.Base;
using CMS.Core;
using CMS.SiteProvider;
using KenticoCommunity.StagingConfigurationModule.Helpers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace KenticoCommunity.StagingConfigurationModule.Infrastructure
{
    internal class LegacyEnvironmentServiceRegistration
    {
        public static void EnsureServiceRegistration()
        {
            if(IsRunningInCmsApp())
            {
                var serviceCollection = new ServiceCollection();
                var configuration = BuildAppSettingsConfiguration();
                serviceCollection.AddStagingModuleConfigurations(configuration);
                RegisterServiceCollection(serviceCollection);
            }
        }

        public static IConfiguration BuildAppSettingsConfiguration()
        {
            var environmentName = Environment.GetEnvironmentVariable("ASPNET_ENVIRONMENT") ??
                                  Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            return new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{environmentName}.json", optional: true, reloadOnChange: true)
                .Build();
        }

        private static void RegisterServiceCollection(IServiceCollection serviceCollection)
        {
            foreach(var serviceDescriptor in serviceCollection)
            {
                if(serviceDescriptor.Lifetime == ServiceLifetime.Scoped)
                {
                    // Exception
                }
                if(serviceDescriptor.ImplementationFactory != null)
                {
                    Func<object> implementationFactoryWrapper = () =>
                    {
                        return serviceDescriptor.ImplementationFactory(null);
                    };
                    Service.Use(serviceDescriptor.ServiceType, implementationFactoryWrapper, null, false);
                    continue;
                }
                var transient = (serviceDescriptor.Lifetime == ServiceLifetime.Transient);

                if (serviceDescriptor.ImplementationInstance != null)
                {
                    Service.Use(serviceDescriptor.ServiceType, serviceDescriptor.ImplementationInstance, null);
                    continue;
                }
                if (serviceDescriptor.ImplementationType != null)
                {
                    Service.Use(serviceDescriptor.ServiceType, serviceDescriptor.ImplementationType, null, transient);
                    continue;
                }
            }
        }

        private static bool IsRunningInCmsApp()
        {
            return (SystemContext.IsCMSRunningAsMainApplication && SystemContext.IsWebSite);
        }

    }
}
