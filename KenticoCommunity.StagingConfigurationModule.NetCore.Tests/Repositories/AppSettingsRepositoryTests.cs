using KenticoCommunity.StagingConfigurationModule.Models.Settings;
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
    public class AppSettingsRepositoryTests
    {

        [TestCase(AppConfigFileName.CorrectConfig, 15)]
        [TestCase(AppConfigFileName.EmptyCollections, 0)]
        [TestCase(AppConfigFileName.MissingCollections, 0)]
        [TestCase(AppConfigFileName.MissingSection, 0)]
        [TestCase(AppConfigFileName.NoConfig, 0)]
        [TestCase(AppConfigFileName.BlankAttributesConfig, 0)]
        [TestCase(AppConfigFileName.BadElementNames, 0)]
        [TestCase(AppConfigFileName.BadCollectionNames, 0)]
        [TestCase(AppConfigFileName.BadSectionName, 0)]
        [TestCase(AppConfigFileName.TitleCase, 15)]
        public void GetExcludedTypes_Returns_Expected_List_Count(string configFileName, int expectedCount)
        {
            var stagingConfigurationSettingsOptions = OptionsFactory.CreateOptions<StagingConfigurationSettings>(configFileName);
            var appSettingsRepository = new AppSettingsRepository(stagingConfigurationSettingsOptions);
            var excludedTypes = appSettingsRepository.GetExcludedTypes();
            Assert.AreEqual(expectedCount, excludedTypes.Count);
        }

        [Test()]
        public void GetExcludedTypes_Returns_Trimmed_Name()
        {
            var stagingConfigurationSettingsOptions = OptionsFactory.CreateOptions<StagingConfigurationSettings>(AppConfigFileName.Untrimmed);
            var appSettingsRepository = new AppSettingsRepository(stagingConfigurationSettingsOptions);
            var excludedTypes = appSettingsRepository.GetExcludedTypes();
            Assert.AreEqual("cms.form", excludedTypes.FirstOrDefault());
        }

        [TestCase(AppConfigFileName.CorrectConfig, 2)]
        [TestCase(AppConfigFileName.EmptyCollections, 0)]
        [TestCase(AppConfigFileName.MissingCollections, 0)]
        [TestCase(AppConfigFileName.MissingSection, 0)]
        [TestCase(AppConfigFileName.NoConfig, 0)]
        [TestCase(AppConfigFileName.BlankAttributesConfig, 0)]
        [TestCase(AppConfigFileName.BadElementNames, 0)]
        [TestCase(AppConfigFileName.BadCollectionNames, 0)]
        [TestCase(AppConfigFileName.BadSectionName, 0)]
        [TestCase(AppConfigFileName.TitleCase, 2)]
        public void GetExcludedMediaLibraries_Returns_Expected_List_Count(string configFileName, int expectedCount)
        {
            var stagingConfigurationSettingsOptions = OptionsFactory.CreateOptions<StagingConfigurationSettings>(configFileName);
            var appSettingsRepository = new AppSettingsRepository(stagingConfigurationSettingsOptions);
            var excludedMediaLibraries = appSettingsRepository.GetExcludedMediaLibraries();
            Assert.AreEqual(expectedCount, excludedMediaLibraries.Count);
        }

        [Test()]
        public void GetExcludedMediaLibraries_Returns_Trimmed_Name()
        {
            var stagingConfigurationSettingsOptions = OptionsFactory.CreateOptions<StagingConfigurationSettings>(AppConfigFileName.Untrimmed);
            var appSettingsRepository = new AppSettingsRepository(stagingConfigurationSettingsOptions);
            var excludedMediaLibraries = appSettingsRepository.GetExcludedMediaLibraries();
            Assert.AreEqual("emailimages", excludedMediaLibraries.FirstOrDefault());
        }


        [TestCase(AppConfigFileName.CorrectConfig, 1)]
        [TestCase(AppConfigFileName.EmptyCollections, 0)]
        [TestCase(AppConfigFileName.MissingCollections, 0)]
        [TestCase(AppConfigFileName.MissingSection, 0)]
        [TestCase(AppConfigFileName.NoConfig, 0)]
        [TestCase(AppConfigFileName.BlankAttributesConfig, 0)]
        [TestCase(AppConfigFileName.BadElementNames, 0)]
        [TestCase(AppConfigFileName.BadCollectionNames, 0)]
        [TestCase(AppConfigFileName.BadSectionName, 0)]
        [TestCase(AppConfigFileName.TitleCase, 1)]
        public void GetExcludedChildTypes_Returns_Expected_List_Count(string configFileName, int expectedCount)
        {
            var stagingConfigurationSettingsOptions = OptionsFactory.CreateOptions<StagingConfigurationSettings>(configFileName);
            var appSettingsRepository = new AppSettingsRepository(stagingConfigurationSettingsOptions);
            var childTypePairs = appSettingsRepository.GetExcludedChildTypes();
            Assert.AreEqual(expectedCount, childTypePairs.Count);
        }

        [Test()]
        public void GetExcludedChildTypes_Returns_Trimmed_Name()
        {
            var stagingConfigurationSettingsOptions = OptionsFactory.CreateOptions<StagingConfigurationSettings>(AppConfigFileName.Untrimmed);
            var appSettingsRepository = new AppSettingsRepository(stagingConfigurationSettingsOptions);
            var childTypePairs = appSettingsRepository.GetExcludedChildTypes();
            var childTypePair = childTypePairs.FirstOrDefault();
            Assert.AreEqual("cms.role", childTypePair?.ParentType);
            Assert.AreEqual("cms.userrole", childTypePair?.ChildType);

        }
    }
}
