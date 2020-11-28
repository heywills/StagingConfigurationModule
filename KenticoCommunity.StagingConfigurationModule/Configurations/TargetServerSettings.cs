using KenticoCommunity.StagingConfigurationModule.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace KenticoCommunity.StagingConfigurationModule.Configurations
{
    public class TargetServerSettings
    {
        public List<ParentChildTypePair> ExcludedChildTypes { get; set; }
    }
}
