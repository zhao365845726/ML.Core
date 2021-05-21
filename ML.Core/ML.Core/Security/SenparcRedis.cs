using Senparc.CO2NET.Cache;
using Senparc.CO2NET.Cache.CsRedis;
using System;
using System.Collections.Generic;
using System.Text;

namespace ML.Core
{
    public class SenparcRedis
    {
        public SenparcRedis()
        {
        }

        public static void Set(string key, string value, TimeSpan span)
        {
            CacheStrategyFactory.RegisterObjectCacheStrategy(() => RedisObjectCacheStrategy.Instance);
            var cache = CacheStrategyFactory.GetObjectCacheStrategyInstance();
            cache.Set($"ML:{key}", value, span, true);
        }

        public static void Set(string key, object value, int tls)
        {
            CacheStrategyFactory.RegisterObjectCacheStrategy(() => RedisObjectCacheStrategy.Instance);
            var cache = CacheStrategyFactory.GetObjectCacheStrategyInstance();
            TimeSpan span = DateTime.Now.AddSeconds(tls) - DateTime.Now;
            cache.Set($"ML:{key}", value, span, true);
        }

        public static void Set(string key, string value, int tls)
        {
            CacheStrategyFactory.RegisterObjectCacheStrategy(() => RedisObjectCacheStrategy.Instance);
            var cache = CacheStrategyFactory.GetObjectCacheStrategyInstance();
            TimeSpan span = DateTime.Now.AddSeconds(tls) - DateTime.Now;
            cache.Set($"ML:{key}", value, span, true);
        }

        public static object Get(string key)
        {
            //如需立即改用某种缓存策略，只需要这样做（注意：是全局立即生效）：
            CacheStrategyFactory.RegisterObjectCacheStrategy(() => RedisObjectCacheStrategy.Instance);
            var cache = CacheStrategyFactory.GetObjectCacheStrategyInstance();
            object obj = cache.Get<object>($"ML:{key}", true);
            return obj;
        }

        public static void Remove(string key)
        {
            //如需立即改用某种缓存策略，只需要这样做（注意：是全局立即生效）：
            CacheStrategyFactory.RegisterObjectCacheStrategy(() => RedisObjectCacheStrategy.Instance);
            var cache = CacheStrategyFactory.GetObjectCacheStrategyInstance();
            cache.RemoveFromCache($"ML:{key}", true);
        }
    }
}
