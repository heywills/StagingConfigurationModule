using System;

namespace KenticoCommunity.StagingConfigurationModule
{
    /// <summary>
    /// Provide helper methods for checking parameter arguments
    /// </summary>
    /// <remarks>This class is also in Patterson.Content.Common. However, currently that library includes
    /// Kentico MVC components and cannot be added to CMSApp projects.  We will eventually remove those components
    /// so that the common library can be reused in the Kentico CMSApp projects</remarks>
    public static class Guard
    {
        /// <summary>
        /// Throws an exception if an argument is null
        /// </summary>
        /// <param name="value">The value to be tested</param>
        /// <param name="name">The name of the argument</param>
        public static void ArgumentNotNull(object value, string name = null)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                name = nameof(value);
            }

            if (value == null)
            {
                throw new ArgumentNullException(name, "Argument " + name + " must not be null");
            }
        }

        /// <summary>
        /// Throws an exception if a string argument is null or empty
        /// </summary>
        /// <param name="value">The value to be tested</param>
        /// <param name="name">The name of the argument</param>
        public static void ArgumentNotNullOrEmpty(string value, string name = null)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                name = nameof(value);
            }

            ArgumentNotNull(value, name);

            if (value == string.Empty)
            {
                throw new ArgumentException($"Argument { name } must not be an empty string", name);
            }
        }

        /// <summary>
        /// Throws an exception if a Guid argument is empty
        /// </summary>
        /// <param name="value">The Guid value to be tested</param>
        /// <param name="name">The name of the argument</param>
        public static void ArgumentGuidNotEmpty(Guid value, string name = null)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                name = nameof(value);
            }

            if (value == Guid.Empty)
            {
                throw new ArgumentException($"Argument { name } must not be an empty Guid", name);
            }
        }
    }
}