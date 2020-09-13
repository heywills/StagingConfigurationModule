using System.Collections.Generic;
using System.Configuration;

namespace KenticoCommunity.StagingConfigurationModule.Configurations
{
    [ConfigurationCollection(typeof(ExcludedChildTypeElement), AddItemName = ChildTypeElementName)]
    public class ExcludedChildTypeElementCollection : ConfigurationElementCollection, IEnumerable<ExcludedChildTypeElement>
    {
        private const string ChildTypeElementName = "childType";

        protected override ConfigurationElement CreateNewElement()
        {
            return new ExcludedChildTypeElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            var excludedChildTypeElement = (ExcludedChildTypeElement)element;
            return $"{excludedChildTypeElement.ParentType}|{excludedChildTypeElement.ChildType}";
        }

        /// <summary>Returns an enumerator that iterates through the collection.</summary>
        /// <returns>An enumerator that can be used to iterate through the collection.</returns>
        /// <remarks>Do not simply cast the collection, because it will cause an infinite loop.
        /// Both Enumerable.Cast<TResult>() and Enumerable.AsEnumerable<TResult>() extensions
        /// call IEnumerator<TResult> GetEnumerator()</remarks>
        public new IEnumerator<ExcludedChildTypeElement> GetEnumerator()
        {
            var count = base.Count;
            for (var i = 0; i < count; i++)
            {
                yield return base.BaseGet(i) as ExcludedChildTypeElement;
            }
        }
    }
}
