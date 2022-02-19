using EasyCaching.Core.Interceptor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Collections;

namespace EasyCaching.ExpressionInterceptor.AspectCore.NAutowired
{
    /// <summary>
    /// 缓存键生成器
    /// </summary>
    public class EasyCachingExpressionKeyGenerator : IEasyCachingExpressionKeyGenerator
    {
        private const char LinkChar = ':';

        /// <summary>
        /// 获取缓存Key
        /// </summary>
        /// <param name="methodInfo"></param>
        /// <param name="args"></param>
        /// <param name="prefix"></param>
        /// <returns></returns>
        public string GetCacheKey(MethodInfo methodInfo, object[] args, string prefix)
        {
            var cacheKeyPrefix = GetCacheKeyPrefix(methodInfo, args, prefix);
            return cacheKeyPrefix.Trim(LinkChar);
        }

        /// <summary>
        /// 获取缓存前缀
        /// </summary>
        /// <param name="methodInfo"></param>
        /// <param name="prefix"></param>
        /// <returns></returns>
        /// <remarks>只有EasyCachingEvict标签会调用此方法，IsAll = true 时会根据此方法返回值移除所有匹配缓存。</remarks>
        public string GetCacheKeyPrefix(MethodInfo methodInfo, string prefix)
        {
            throw new InvalidOperationException("表达式式的键生成器此方法不可用");
        }

        /// <summary>
        /// 获取缓存前缀
        /// </summary>
        /// <param name="methodInfo"></param>
        /// <param name="args"></param>
        /// <param name="prefix"></param>
        /// <returns></returns>
        public string GetCacheKeyPrefix(MethodInfo methodInfo, object[] args, string prefix)
        {
            //包含表达式
            if (prefix.Contains('$'))
            {
                //表达式格式 ${0} ${1:Property}
                var key = Regex.Replace(prefix, @"\$\{(\d+(:[a-zA-Z]+)*)\}", m =>
                {
                    var expElement = m.Groups[1].Value;
                    //访问属性表达式
                    if (expElement.Contains(':'))
                    {
                        string[] posAndProperty = expElement.Split(':');
                        object arg = args[int.Parse(posAndProperty[0])];

                        Type t = arg.GetType();
                        if (!t.IsClass || arg is string || arg is IEnumerable)
                        {
                            throw new ArgumentException("非对象参数不能访问属性");
                        }

                        string prop = posAndProperty[1];
                        return t.GetProperty(posAndProperty[1]).GetValue(arg).ToString();
                    }
                    //普通参数表达式
                    else
                    {
                        return args[int.Parse(expElement)].ToString();
                    }
                });
                return $"{key}{LinkChar}";
            }
            else
            {
                return $"{prefix}{LinkChar}";
            }
        }
    }
}
