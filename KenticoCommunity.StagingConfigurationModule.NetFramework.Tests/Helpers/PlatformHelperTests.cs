﻿using KenticoCommunity.StagingConfigurationModule.Helpers;
using NUnit.Framework;

namespace KenticoCommunity.StagingConfigurationModule.Tests.Helpers
{
    [TestFixture]
    public class PlatformHelperTests
    {


        [Test()]
        public void IsDotNetFramework_Returns_True()
        {
            bool result = PlatformHelper.IsDotNetFramework();
            Assert.IsTrue(result);
        }
    }
}
