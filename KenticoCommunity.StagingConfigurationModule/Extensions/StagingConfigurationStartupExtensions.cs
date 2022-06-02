using KenticoCommunity.StagingConfigurationModule.Helpers;
using KenticoCommunity.StagingConfigurationModule.Interfaces;
using KenticoCommunity.StagingConfigurationModule.Models.Settings;
using KenticoCommunity.StagingConfigurationModule.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace KenticoCommunity.StagingConfigurationModule.Extensions
{
    /// <summary>
    /// Extension methods for registering the services required for this library.
    /// If used in .net core, the IConfiguration object should be provided. 
    /// Otherwise, it can be null.
    /// </summary>
    public static class StagingConfigurationStartupExtensions
    {
        private const string DefaultConfigurationKey = "stagingConfiguration";

        public static IServiceCollection AddStagingConfigurationModuleServices(this IServiceCollection services)
        {
            return services.AddStagingConfigurationModuleServices(null, DefaultConfigurationKey);
        }


        public static IServiceCollection AddStagingConfigurationModuleServices(this IServiceCollection services, IConfiguration configuration)
        {
            return services.AddStagingConfigurationModuleServices(configuration, DefaultConfigurationKey);
        }

        /// <summary>
        /// Add the service registrations needed to the provided service collection.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <param name="configurationKey"></param>
        /// <returns></returns>
        public static IServiceCollection AddStagingConfigurationModuleServices(this IServiceCollection services, IConfiguration configuration, string configurationKey)
        {
            if(PlatformHelper.IsDotNetFramework())
            {
                services.AddSingleton<ISettingsRepository, WebConfigSettingsRepository>();
            }
            else
            {
                if (configuration != null)
                {
                    services.AddOptions<StagingConfigurationSettings>()
                            .Bind(configuration.GetSection(configurationKey));
                }
                services.AddSingleton<ISettingsRepository, AppSettingsRepository>();
            }
            services.AddTransient<IConfigurationHelper, ConfigurationHelper>();
            services.AddSingleton<IStagingConfigurationHelper, StagingConfigurationHelper>();
            return services;
        }
    }
}
