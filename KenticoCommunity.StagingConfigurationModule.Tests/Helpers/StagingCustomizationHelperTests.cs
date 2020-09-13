using System.Collections.Generic;
using System.Linq;
using CMS.Core;
using CMS.IO;
using CMS.MediaLibrary;
using CMS.Membership;
using CMS.Tests;
using KenticoCommunity.StagingConfigurationModule.Helpers;
using KenticoCommunity.StagingConfigurationModule.Interfaces;
using KenticoCommunity.StagingConfigurationModule.Models;
using KenticoCommunity.StagingConfigurationModule.Tests.TestHelpers;
using NUnit.Framework;
using Moq;


namespace KenticoCommunity.StagingConfigurationModule.Tests.Helpers
{
    [TestFixture]

    public class StagingCustomizationHelperTests: UnitTests
    {
        [Test]
        public void IsExcludedObjectType_Returns_True_If_Type_In_Exclusion_List()
        {
            var mockSettingsRepository =
                CreateMockSettingsRepository(excludedTypes: (new List<string>() { "cms.user", "cms.form" }));
            var mockEventLogService = CreateMockEventLogService();
            var stagingCustomizationHelper = new StagingCustomizationHelper(mockSettingsRepository.Object, mockEventLogService.Object);
            Fake<UserInfo>();
            var userInfo = new UserInfo();
            var result = stagingCustomizationHelper.IsExcludedObjectType(userInfo);
            Assert.IsTrue(result);
        }

        [Test]
        public void IsExcludedObjectType_Returns_True_If_Type_Not_In_Exclusion_List()
        {
            var mockSettingsRepository =
                CreateMockSettingsRepository(excludedTypes: (new List<string>() { "cms.form" }));
            var mockEventLogService = CreateMockEventLogService();
            var stagingCustomizationHelper = new StagingCustomizationHelper(mockSettingsRepository.Object, mockEventLogService.Object);
            Fake<UserInfo>();
            var userInfo = new UserInfo();
            var result = stagingCustomizationHelper.IsExcludedObjectType(userInfo);
            Assert.IsFalse(result);
        }

        [Test]
        public void IsExcludedMediaLibraryFile_Returns_True_If_Parent_Library_In_Exclusion_List()
        {
            var mockSettingsRepository =
                CreateMockSettingsRepository(excludedMediaLibraries: (new List<string>() {"emailTemplateAssets", "globalAssets"}));
            var mockEventLogService = CreateMockEventLogService();
            var stagingCustomizationHelper = new StagingCustomizationHelper(mockSettingsRepository.Object, mockEventLogService.Object);
            var mediaFileInfo = GetFakeMediaFileInfo("EMAILtemplateassets");
            var result = stagingCustomizationHelper.IsExcludedMediaLibraryFile(mediaFileInfo);
            Assert.IsTrue(result);
        }

        [Test]
        public void IsExcludedMediaLibraryFile_Returns_False_If_Parent_Library_Not_In_Exclusion_List()
        {
            var mockSettingsRepository =
                CreateMockSettingsRepository(excludedMediaLibraries: (new List<string>() { "globalAssets" }));
            var mockEventLogService = CreateMockEventLogService();
            var stagingCustomizationHelper = new StagingCustomizationHelper(mockSettingsRepository.Object, mockEventLogService.Object);
            var mediaFileInfo = GetFakeMediaFileInfo("emailTemplateAssets");
            var result = stagingCustomizationHelper.IsExcludedMediaLibraryFile(mediaFileInfo);
            Assert.IsFalse(result);
        }

        [Test]
        public void IsExcludedChildType_Returns_True_If_Parent_Type_And_Child_Type_In_Exclusion_List()
        {

        }


        private Mock<ISettingsRepository> CreateMockSettingsRepository(
            List<string> excludedTypes = null,
            List<string> excludedMediaLibraries = null,
            List<ParentChildTypePair> excludedChildTypes = null)
        {
            var moqSettingsRepository = new Mock<ISettingsRepository>();
            moqSettingsRepository.Setup(m => m.GetExcludedTypes()).Returns(excludedTypes);
            moqSettingsRepository.Setup(m => m.GetExcludedMediaLibraries()).Returns(excludedMediaLibraries);
            moqSettingsRepository.Setup(m => m.GetExcludedChildTypes()).Returns(excludedChildTypes);
            return moqSettingsRepository;
        }

        private Mock<IEventLogService> CreateMockEventLogService()
        {
            var mockEventLogService = new Mock<IEventLogService>();
            mockEventLogService.Setup(m =>
                m.LogEvent(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()));
            return mockEventLogService;
        }

        private MediaFileInfo GetFakeMediaFileInfo(string libraryCodeName)
        {
            string fakeMediaFilePath = GetFakeMediaFilePath();
            Fake<MediaLibraryInfo, MediaLibraryInfoProvider>().WithData(
                new MediaLibraryInfo
                {
                    LibraryName = libraryCodeName,
                    LibraryID = 7
                }
            );
            Fake<MediaFileInfo, MediaFileInfoProvider>().WithData(
            );

            var mediaFileInfo = new MediaFileInfo(fakeMediaFilePath, 7);
            return mediaFileInfo;
        }

        private string GetFakeMediaFilePath()
        {
            return Path.Combine(PathHelper.GetTestConfigFilesDirectoryPath(), ConfigFileName.CorrectConfig);
        }

    }
}