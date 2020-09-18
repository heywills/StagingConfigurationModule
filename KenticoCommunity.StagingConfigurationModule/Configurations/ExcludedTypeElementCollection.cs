using System.Collections.Generic;
using System.Configuration;

namespace KenticoCommunity.StagingConfigurationModule.Configurations
{
    /// <summary>
    /// Represents a collection of ExcludedTypeElements in the configuration, to provide a list of object types that should be excluded from staging tasks on the source server.
    /// </summary>
    [ConfigurationCollection(typeof(ExcludedTypeElement), AddItemName = ExcludedTypeElementName)]
    public class ExcludedTypeElementCollection : ConfigurationElementCollection, IEnumerable<ExcludedTypeElement>
    {
        private const string ExcludedTypeElementName = "type";

        protected override ConfigurationElement CreateNewElement()
        {
            return new ExcludedTypeElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((ExcludedTypeElement)element).Name;
        }

        /// <summary>Returns an enumerator that iterates through the collection.</summary>
        /// <returns>An enumerator that can be used to iterate through the collection.</returns>
        /// <remarks>Do not simply cast the collection, because it will cause an infinite loop.
        /// Both Enumerable.Cast<TResult>() and Enumerable.AsEnumerable<TResult>() extensions
        /// call IEnumerator<TResult> GetEnumerator()</remarks>
        public new IEnumerator<ExcludedTypeElement> GetEnumerator()
        {
            var count = base.Count;
            for (var i = 0; i < count; i++)
            {
                yield return base.BaseGet(i) as ExcludedTypeElement;
            }
        }
    }
}
