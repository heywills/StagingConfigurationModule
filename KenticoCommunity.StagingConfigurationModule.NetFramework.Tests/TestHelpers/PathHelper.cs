using System.IO;
using System.Reflection;

namespace KenticoCommunity.StagingConfigurationModule.Tests.TestHelpers
{
    public static class PathHelper
    {
        public static string GetTestConfigFilesDirectoryPath()
        {
            return Path.Combine(Directory.GetParent(Assembly.GetExecutingAssembly().Location).FullName, "ConfigFiles");
        }
    }
}
