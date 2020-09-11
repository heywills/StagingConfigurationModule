using System.Collections.Generic;
using System.Configuration;

namespace KenticoCommunity.StagingConfigurationModule.Configuration
{
    [ConfigurationCollection(typeof(ExcludedMediaLibraryElement), AddItemName = "mediaLibrary")]
    public class ExcludedMediaLibraryElementCollection : ConfigurationElementCollection, IEnumerable<ExcludedMediaLibraryElement>
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new ExcludedMediaLibraryElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((ExcludedMediaLibraryElement)element).Code;
        }

        /// <summary>Returns an enumerator that iterates through the collection.</summary>
        /// <returns>An enumerator that can be used to iterate through the collection.</returns>
        /// <remarks>Do not simply cast the collection, because it will cause an infinite loop.
        /// Both Enumerable.Cast<TResult>() and Enumerable.AsEnumerable<TResult>() extensions
        /// call IEnumerator<TResult> GetEnumerator()</remarks>
        public new IEnumerator<ExcludedMediaLibraryElement> GetEnumerator()
        {
            var count = base.Count;
            for (var i = 0; i < count; i++)
            {
                yield return base.BaseGet(i) as ExcludedMediaLibraryElement;
            }
        }
    }
}
