using System;
using System.Runtime.InteropServices;

namespace KenticoCommunity.StagingConfigurationModule.Helpers
{
    public static class PlatformHelper
    {
        /// <summary>
        /// Return true if running in .NET Framework
        /// </summary>
        /// <returns></returns>
        public static bool IsDotNetFramework()
        {
            return RuntimeInformation.FrameworkDescription.StartsWith(".NET Framework", StringComparison.OrdinalIgnoreCase);
        }
    }
}
