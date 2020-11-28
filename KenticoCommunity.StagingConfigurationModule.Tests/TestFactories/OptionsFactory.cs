using KenticoCommunity.StagingConfigurationModule.Tests.TestHelpers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace KenticoCommunity.StagingConfigurationModule.Tests.TestFactories
{
    public static class OptionsFactory
    {
        const string DEFAULT_SECTION_NAME = "stagingConfiguration";
        public static IOptions<T> CreateOptions<T>(string settingsFileName, string sectionName = DEFAULT_SECTION_NAME) where T: class, new()
        {
            var configuration = new ConfigurationBuilder()
               .SetBasePath(PathHelper.GetTestConfigFilesDirectoryPath())
               .AddJsonFile(settingsFileName, false)
               .Build();
            return Options.Create(configuration.GetSection(sectionName).Get<T>());
        }
    }
}
