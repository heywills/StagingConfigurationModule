using System;
using System.Collections.Generic;
using System.Text;

namespace KenticoCommunity.StagingConfigurationModule.Models.Settings
{
    public class SourceServerSettings
    {
        public List<string> ExcludedTypes { get; set; }

        public List<string> ExcludedMediaLibraries { get; set; }
    }
}
