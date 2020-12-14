using System;
using System.Collections.Generic;
using System.Text;

namespace KenticoCommunity.StagingConfigurationModule.Models.Settings
{
    public class StagingConfigurationSettings
    {
        public SourceServerSettings SourceServer { get; set; }

        public TargetServerSettings TargetServer { get; set; }

    }
}
