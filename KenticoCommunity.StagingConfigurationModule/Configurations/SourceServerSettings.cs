using System;
using System.Collections.Generic;
using System.Text;

namespace KenticoCommunity.StagingConfigurationModule.Configurations
{
    public class SourceServerSettings
    {
        public List<string> ExcludedTypes { get; set; }

        public List<string> ExcludedMediaLibraries { get; set; }
    }
}
