using EasyCaching.Core.Configurations;
using EasyCaching.Core.Interceptor;
using AspectCore.Configuration;
using AspectCore.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Linq;
using System.Reflection;

namespace EasyCaching.ExpressionInterceptor.AspectCore
{
    /// <summary>
    /// Aspectcore interceptor service collection extensions.
    /// </summary>
    public static class EasyCachingInterceptorServiceCollectionExtensions
    {
        /// <summary>
        /// Configures the AspectCore interceptor.
        /// </summary>
        /// <returns>The aspect core interceptor.</returns>
        /// <param name="services">Services.</param>
        /// <param name="options">Easycaching Interceptor config</param>        
        public static void ConfigureAspectCoreExpressionInterceptor(this IServiceCollection services, Action<EasyCachingInterceptorOptions> options)
        {
            services.TryAddSingleton<IEasyCachingKeyGenerator, EasyCachingExpressionKeyGenerator>();
            services.Configure(options);

            services.ConfigureDynamicProxy(config =>
            {
                bool all(MethodInfo x) => x.CustomAttributes.Any(data => typeof(EasyCachingInterceptorAttribute).GetTypeInfo().IsAssignableFrom(data.AttributeType));

                config.Interceptors.AddTyped<EasyCachingExpressionInterceptor>(all);
            });
        }
    }
}

