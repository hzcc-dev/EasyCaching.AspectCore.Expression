using EasyCaching.Core.Interceptor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace EasyCaching.ExpressionInterceptor.AspectCore
{
    public interface IEasyCachingExpressionKeyGenerator : IEasyCachingKeyGenerator
    {
        /// <summary>
        /// Generate key prefix with expression supported.
        /// </summary>
        /// <param name="methodInfo"></param>
        /// <param name="args"></param>
        /// <param name="prefix"></param>
        /// <returns></returns>
        string GetCacheKeyPrefix(MethodInfo methodInfo, object[] args, string prefix);
    }
}
