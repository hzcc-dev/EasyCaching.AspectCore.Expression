using EasyCaching.Core.Interceptor;
using System;
using System.Collections.Generic;
using System.Text;

namespace EasyCaching.ExpressionInterceptor.AspectCore
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class EasyCachingDelAttribute : EasyCachingEvictAttribute
    {

    }
}
