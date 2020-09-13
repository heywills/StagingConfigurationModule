using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KenticoCommunity.StagingConfigurationModule.Tests.TestHelpers
{
    public static class ConfigFileName
    {
        public const string CorrectConfig = "correct.config";
        public const string EmptyCollections = "empty-collections.config";
        public const string MissingCollections = "missing-collections.config";
        public const string MissingSection = "missing-section.config";
        public const string NoConfig = "no-config.config";
        public const string BlankAttributesConfig = "blank-attributes.config";
        public const string MissingAttributes = "missing-attributes.config";
        public const string BadElementNames = "bad-element-names.config";
        public const string BadSectionName = "bad-section-name.config";
        public const string BadCollectionNames = "bad-collection-names.config";
        public const string Untrimmed = "untrimmed.config";
        public const string MixedValidity = "mixed-validity.config";
    }
}
