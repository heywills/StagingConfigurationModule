using KenticoCommunity.StagingConfigurationModule.Configurations;
using KenticoCommunity.StagingConfigurationModule.Interfaces;
using KenticoCommunity.StagingConfigurationModule.Repositories;
using KenticoCommunity.StagingConfigurationModule.Tests.TestFactories;
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

        [TestCase(ConfigFileName.CorrectConfig, 15)]
        [TestCase(ConfigFileName.EmptyCollections, 0)]
        [TestCase(ConfigFileName.MissingCollections, 0)]
        [TestCase(ConfigFileName.MissingSection, 0)]
        [TestCase(ConfigFileName.NoConfig, 0)]
        [TestCase(ConfigFileName.BlankAttributesConfig, 0)]
        [TestCase(ConfigFileName.BadElementNames, 0)]
        [TestCase(ConfigFileName.BadCollectionNames, 0)]
        [TestCase(ConfigFileName.BadSectionName, 0)]
        [TestCase(ConfigFileName.TitleCase, 15)]
        public void GetExcludedTypes_Returns_Expected_List_Count(string configFileName, int expectedCount)
        {
            var stagingConfigurationSettingsOptions = OptionsFactory.CreateOptions<StagingConfigurationSettings>(configFileName);
            var webConfigSettingsRepository = new WebConfigSettingsRepository(stagingConfigurationSettingsOptions);
            var excludedTypes = webConfigSettingsRepository.GetExcludedTypes();
            Assert.AreEqual(expectedCount, excludedTypes.Count);
        }

        [Test()]
        public void GetExcludedTypes_Returns_Trimmed_Name()
        {
            var stagingConfigurationSettingsOptions = OptionsFactory.CreateOptions<StagingConfigurationSettings>(ConfigFileName.Untrimmed);
            var webConfigSettingsRepository = new WebConfigSettingsRepository(stagingConfigurationSettingsOptions);
            var excludedTypes = webConfigSettingsRepository.GetExcludedTypes();
            Assert.AreEqual("cms.form", excludedTypes.FirstOrDefault());
        }

        [TestCase(ConfigFileName.CorrectConfig, 2)]
        [TestCase(ConfigFileName.EmptyCollections, 0)]
        [TestCase(ConfigFileName.MissingCollections, 0)]
        [TestCase(ConfigFileName.MissingSection, 0)]
        [TestCase(ConfigFileName.NoConfig, 0)]
        [TestCase(ConfigFileName.BlankAttributesConfig, 0)]
        [TestCase(ConfigFileName.BadElementNames, 0)]
        [TestCase(ConfigFileName.BadCollectionNames, 0)]
        [TestCase(ConfigFileName.BadSectionName, 0)]
        [TestCase(ConfigFileName.TitleCase, 2)]
        public void GetExcludedMediaLibraries_Returns_Expected_List_Count(string configFileName, int expectedCount)
        {
            var stagingConfigurationSettingsOptions = OptionsFactory.CreateOptions<StagingConfigurationSettings>(configFileName);
            var webConfigSettingsRepository = new WebConfigSettingsRepository(stagingConfigurationSettingsOptions);
            var excludedMediaLibraries = webConfigSettingsRepository.GetExcludedMediaLibraries();
            Assert.AreEqual(expectedCount, excludedMediaLibraries.Count);
        }

        [Test()]
        public void GetExcludedMediaLibraries_Returns_Trimmed_Name()
        {
            var stagingConfigurationSettingsOptions = OptionsFactory.CreateOptions<StagingConfigurationSettings>(ConfigFileName.Untrimmed);
            var webConfigSettingsRepository = new WebConfigSettingsRepository(stagingConfigurationSettingsOptions);
            var excludedMediaLibraries = webConfigSettingsRepository.GetExcludedMediaLibraries();
            Assert.AreEqual("emailimages", excludedMediaLibraries.FirstOrDefault());
        }


        [TestCase(ConfigFileName.CorrectConfig, 1)]
        [TestCase(ConfigFileName.EmptyCollections, 0)]
        [TestCase(ConfigFileName.MissingCollections, 0)]
        [TestCase(ConfigFileName.MissingSection, 0)]
        [TestCase(ConfigFileName.NoConfig, 0)]
        [TestCase(ConfigFileName.BlankAttributesConfig, 0)]
        [TestCase(ConfigFileName.BadElementNames, 0)]
        [TestCase(ConfigFileName.BadCollectionNames, 0)]
        [TestCase(ConfigFileName.BadSectionName, 0)]
        [TestCase(ConfigFileName.TitleCase, 1)]
        public void GetExcludedChildTypes_Returns_Expected_List_Count(string configFileName, int expectedCount)
        {
            var stagingConfigurationSettingsOptions = OptionsFactory.CreateOptions<StagingConfigurationSettings>(configFileName);
            var webConfigSettingsRepository = new WebConfigSettingsRepository(stagingConfigurationSettingsOptions);
            var childTypePairs = webConfigSettingsRepository.GetExcludedChildTypes();
            Assert.AreEqual(expectedCount, childTypePairs.Count);
        }

        [Test()]
        public void GetExcludedChildTypes_Returns_Trimmed_Name()
        {
            var stagingConfigurationSettingsOptions = OptionsFactory.CreateOptions<StagingConfigurationSettings>(ConfigFileName.Untrimmed);
            var webConfigSettingsRepository = new WebConfigSettingsRepository(stagingConfigurationSettingsOptions);
            var childTypePairs = webConfigSettingsRepository.GetExcludedChildTypes();
            var childTypePair = childTypePairs.FirstOrDefault();
            Assert.AreEqual("cms.role", childTypePair?.ParentType);
            Assert.AreEqual("cms.userrole", childTypePair?.ChildType);

        }
    }
}
