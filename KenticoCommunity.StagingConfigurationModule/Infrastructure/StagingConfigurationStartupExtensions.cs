using KenticoCommunity.StagingConfigurationModule.Models.Settings;
using KenticoCommunity.StagingConfigurationModule.Helpers;
using KenticoCommunity.StagingConfigurationModule.Interfaces;
using KenticoCommunity.StagingConfigurationModule.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace KenticoCommunity.StagingConfigurationModule.Infrastructure
{
    public static class StagingConfigurationStartupExtensions
    {
        private const string DefaultConfigurationKey = "stagingConfiguration";

        public static IServiceCollection AddStagingModuleConfigurations(this IServiceCollection services, IConfiguration configuration)
        {
            return services.AddStagingConfigurationModuleServices(configuration, DefaultConfigurationKey);
        }

        public static IServiceCollection AddStagingConfigurationModuleServices(this IServiceCollection services, IConfiguration configuration, string configurationKey)
        {
            services.AddSingleton<ISettingsRepository, AppSettingsRepository>();
            services.AddSingleton<IStagingConfigurationHelper, StagingConfigurationHelper>();
            services.AddOptions<StagingConfigurationSettings>()
                .Bind(configuration.GetSection(configurationKey));
            return services;
        }
    }
}
