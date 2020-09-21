using System.Configuration;

namespace KenticoCommunity.StagingConfigurationModule.Interfaces
{
    /// <summary>
    /// Helper to provide the correct configuration file for the current context.
    /// </summary>
    public interface IConfigurationHelper
    {
        /// <summary>
        /// Get the .NET Configuration object for the current context.
        /// </summary>
        /// <returns></returns>
        Configuration GetWebConfiguration();
    }
}