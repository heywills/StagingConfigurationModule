using CMS.Core;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace KenticoCommunity.StagingConfigurationModule.Extensions
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// With the provided collection of service descriptors, use CMS.Core.Service.Use to register
        /// each service with Kentico Xperience's service locator.  This allows a collection of
        /// service descriptors to be registered with the same DI container used in Xperience's
        /// CMS application.
        /// </summary>
        /// <param name="services"></param>
        public static void RegisterWithKenticoServiceLocator(this IServiceCollection services)
        {
            foreach (var serviceDescriptor in services)
            {
                var transient = (serviceDescriptor.Lifetime == ServiceLifetime.Transient);
                if (serviceDescriptor.Lifetime == ServiceLifetime.Scoped)
                {
                    throw new NotSupportedException(
                        @"A scoped service cannot be registered using Xperience's 
                        service locator, CMS.Core.Service.Use.");
                }
                if (serviceDescriptor.ImplementationInstance != null)
                {
                    Service.Use(serviceDescriptor.ServiceType,
                                serviceDescriptor.ImplementationInstance,
                                null);
                    continue;
                }
                if (serviceDescriptor.ImplementationType != null)
                {
                    Service.Use(serviceDescriptor.ServiceType,
                                serviceDescriptor.ImplementationType,
                                null,
                                transient);
                    continue;
                }
                if (serviceDescriptor.ImplementationFactory != null)
                {
                    throw new NotSupportedException(
                        @"An implementation factory cannot be registered using Xperience's
                        service locator, unless it is wrapped with a local function that 
                        passes null as the IServiceProvider parameter.");
                }
            }
        }
    }

}
