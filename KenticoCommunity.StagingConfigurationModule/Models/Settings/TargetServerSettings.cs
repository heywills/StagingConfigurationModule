using KenticoCommunity.StagingConfigurationModule.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace KenticoCommunity.StagingConfigurationModule.Models.Settings
{
    public class TargetServerSettings
    {
        public List<ParentChildTypePair> ExcludedChildTypes { get; set; }
    }
}
