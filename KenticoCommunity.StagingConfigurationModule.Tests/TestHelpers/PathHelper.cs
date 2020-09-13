using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.IO;
using System.Text;
using System.Threading.Tasks;

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
