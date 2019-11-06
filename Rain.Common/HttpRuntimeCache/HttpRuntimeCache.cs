using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Caching;

namespace Rain.Common
{
    public class HttpRuntimeCache
    {

        /// <summary>
        /// 缓存指定对象，设置缓存
        /// </summary>
        public static bool Set(string key, object value, long expireSencond = 1800)
        {
            return Set(key, value, null, DateTime.UtcNow.AddSeconds(expireSencond), System.Web.Caching.Cache.NoSlidingExpiration,
                CacheItemPriority.Default, null);
        }

        /// <summary>
        ///  缓存指定对象，设置缓存
        /// </summary>
        public static bool Set(string key, object value, string path)
        {
            try
            {
                var cacheDependency = new CacheDependency(path);
                return Set(key, value, cacheDependency);
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 缓存指定对象，设置缓存
        /// </summary>
        public static bool Set(string key, object value, CacheDependency cacheDependency)
        {
            return Set(key, value, cacheDependency, System.Web.Caching.Cache.NoAbsoluteExpiration, System.Web.Caching.Cache.NoSlidingExpiration,
                CacheItemPriority.Default, null);
        }

        /// <summary>
        /// 缓存指定对象，设置缓存
        /// </summary>
        public static bool Set(string key, object value, double seconds, bool isAbsulute)
        {
            return Set(key, value, null, (isAbsulute ? DateTime.Now.AddSeconds(seconds) : System.Web.Caching.Cache.NoAbsoluteExpiration),
                (isAbsulute ? System.Web.Caching.Cache.NoSlidingExpiration : TimeSpan.FromSeconds(seconds)), CacheItemPriority.Default,
                null);
        }

        /// <summary>
        /// 获取缓存对象
        /// </summary>
        public static object Get(string key)
        {
            return GetPrivate(key);
        }

        /// <summary>
        /// 判断缓存中是否含有缓存该键
        /// </summary>
        public static bool Exists(string key)
        {
            return (GetPrivate(key) != null);
        }

        /// <summary>
        /// 移除缓存对象
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool Remove(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                return false;
            }
            HttpRuntime.Cache.Remove(key);
            return true;
        }

        /// <summary>
        /// 移除所有缓存
        /// </summary>
        /// <returns></returns>
        public static bool RemoveAll()
        {
            IDictionaryEnumerator iDictionaryEnumerator = HttpRuntime.Cache.GetEnumerator();
            while (iDictionaryEnumerator.MoveNext())
            {
                HttpRuntime.Cache.Remove(Convert.ToString(iDictionaryEnumerator.Key));
            }
            return true;
        }

        public static bool Set(string key, object value, CacheDependency cacheDependency, DateTime dateTime,
            TimeSpan timeSpan, CacheItemPriority cacheItemPriority, CacheItemRemovedCallback cacheItemRemovedCallback)
        {
            if (string.IsNullOrEmpty(key) || value == null)
            {
                return false;
            }
            HttpRuntime.Cache.Insert(key, value, cacheDependency, dateTime, timeSpan, cacheItemPriority,
                cacheItemRemovedCallback);
            return true;
        }

        /// <summary>
        /// 获取缓存
        /// </summary>
        private static object GetPrivate(string key)
        {
            return string.IsNullOrEmpty(key) ? null : HttpRuntime.Cache.Get(key);
        }
    }
}
