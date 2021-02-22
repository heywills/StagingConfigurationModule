using KenticoCommunity.StagingConfigurationModule.Helpers;
using KenticoCommunity.StagingConfigurationModule.Interfaces;
using KenticoCommunity.StagingConfigurationModule.Repositories;
using KenticoCommunity.StagingConfigurationModule.Tests.TestHelpers;
using Moq;
using NUnit.Framework;
using System.Configuration;
using System.Linq;

namespace KenticoCommunity.StagingConfigurationModule.Tests.Repositories
{
    [TestFixture]
    public class WebConfigSettingsRepositoryTests
    {
        private readonly string _testProcessPath = PathHelper.GetTestConfigFilesDirectoryPath();
        private readonly ConfigurationHelper _configurationHelper = new ConfigurationHelper();

        [TestCase(ConfigFileName.CorrectConfig, 15)]
        [TestCase(ConfigFileName.EmptyCollections, 0)]
        [TestCase(ConfigFileName.MissingCollections, 0)]
        [TestCase(ConfigFileName.MissingSection, 0)]
        [TestCase(ConfigFileName.NoConfig, 0)]
        [TestCase(ConfigFileName.BlankAttributesConfig, 0)]
        public void GetExcludedTypes_Returns_Expected_List_Count(string configFileName, int expectedCount)
        {
            var mockConfigurationHelper = CreateMockConfigurationHelperForFile(configFileName);
            var webConfigSettingsRepository = new WebConfigSettingsRepository(mockConfigurationHelper.Object);
            var excludedTypes = webConfigSettingsRepository.GetExcludedTypes();
            Assert.AreEqual(expectedCount, excludedTypes.Count);
        }

        [Test()]
        public void GetExcludedTypes_Returns_Trimmed_Name()
        {
            var mockConfigurationHelper = CreateMockConfigurationHelperForFile(ConfigFileName.Untrimmed);
            var webConfigSettingsRepository = new WebConfigSettingsRepository(mockConfigurationHelper.Object);
            var excludedTypes = webConfigSettingsRepository.GetExcludedTypes();
            Assert.AreEqual("cms.form", excludedTypes.FirstOrDefault());
        }

        [TestCase(ConfigFileName.CorrectConfig, 2)]
        [TestCase(ConfigFileName.EmptyCollections, 0)]
        [TestCase(ConfigFileName.MissingCollections, 0)]
        [TestCase(ConfigFileName.MissingSection, 0)]
        [TestCase(ConfigFileName.NoConfig, 0)]
        [TestCase(ConfigFileName.BlankAttributesConfig, 0)]
        public void GetExcludedMediaLibraries_Returns_Expected_List_Count(string configFileName, int expectedCount)
        {
            var mockConfigurationHelper = CreateMockConfigurationHelperForFile(configFileName);
            var webConfigSettingsRepository = new WebConfigSettingsRepository(mockConfigurationHelper.Object);
            var excludedMediaLibraries = webConfigSettingsRepository.GetExcludedMediaLibraries();
            Assert.AreEqual(expectedCount, excludedMediaLibraries.Count);
        }

        [Test()]
        public void GetExcludedMediaLibraries_Returns_Trimmed_Name()
        {
            var mockConfigurationHelper = CreateMockConfigurationHelperForFile(ConfigFileName.Untrimmed);
            var webConfigSettingsRepository = new WebConfigSettingsRepository(mockConfigurationHelper.Object);
            var excludedMediaLibraries = webConfigSettingsRepository.GetExcludedMediaLibraries();
            Assert.AreEqual("emailimages", excludedMediaLibraries.FirstOrDefault());
        }


        [TestCase(ConfigFileName.CorrectConfig, 1)]
        [TestCase(ConfigFileName.EmptyCollections, 0)]
        [TestCase(ConfigFileName.MissingCollections, 0)]
        [TestCase(ConfigFileName.MissingSection, 0)]
        [TestCase(ConfigFileName.NoConfig, 0)]
        [TestCase(ConfigFileName.BlankAttributesConfig, 0)]
        public void GetExcludedChildTypes_Returns_Expected_List_Count(string configFileName, int expectedCount)
        {
            var mockConfigurationHelper = CreateMockConfigurationHelperForFile(configFileName);
            var webConfigSettingsRepository = new WebConfigSettingsRepository(mockConfigurationHelper.Object);
            var childTypePairs = webConfigSettingsRepository.GetExcludedChildTypes();
            Assert.AreEqual(expectedCount, childTypePairs.Count);
        }

        [Test()]
        public void GetExcludedChildTypes_Returns_Trimmed_Name()
        {
            var mockConfigurationHelper = CreateMockConfigurationHelperForFile(ConfigFileName.Untrimmed);
            var webConfigSettingsRepository = new WebConfigSettingsRepository(mockConfigurationHelper.Object);
            var childTypePairs = webConfigSettingsRepository.GetExcludedChildTypes();
            var childTypePair = childTypePairs.FirstOrDefault();
            Assert.AreEqual("cms.role", childTypePair?.ParentType);
            Assert.AreEqual("cms.userrole", childTypePair?.ChildType);

        }
        [TestCase(ConfigFileName.MissingAttributes)]
        [TestCase(ConfigFileName.BadElementNames)]
        [TestCase(ConfigFileName.BadCollectionNames)]
        [TestCase(ConfigFileName.BadSectionName)]
        public void WebConfigSettingsRepository_Constructor_Throws_ConfigurationErrorsException_If_InvalidConfiguration(string configFileName)
        {
            var mockConfigurationHelper = CreateMockConfigurationHelperForFile(configFileName);
            Assert.That(() => (new WebConfigSettingsRepository(mockConfigurationHelper.Object)), Throws.TypeOf<ConfigurationErrorsException>());
        }


        private Mock<IConfigurationHelper> CreateMockConfigurationHelperForFile(string configFileName)
        {
            var configuration = _configurationHelper.OpenConfiguration(_testProcessPath, configFileName);
            var mockConfigurationHelper = new Mock<IConfigurationHelper>();
            mockConfigurationHelper.Setup(m => m.GetWebConfiguration()).Returns(configuration);
            return mockConfigurationHelper;
        }
    }
}
