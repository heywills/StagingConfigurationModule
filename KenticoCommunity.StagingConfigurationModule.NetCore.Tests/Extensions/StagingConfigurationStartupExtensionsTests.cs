using KenticoCommunity.StagingConfigurationModule.Extensions;
using KenticoCommunity.StagingConfigurationModule.Helpers;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;

namespace KenticoCommunity.StagingConfigurationModule.Tests.Extensions
{
    [TestFixture]
    public class StagingConfigurationStartupExtensions
    {


        [Test()]
        public void AddStagingConfigurationModuleServices_Does_Not_Throw_Exception_When_Configuration_Is_Null()
        {
            var serviceCollection = new Mock<IServiceCollection>();
            Assert.DoesNotThrow(() => serviceCollection.Object.AddStagingConfigurationModuleServices(null));
        }
    }
}
