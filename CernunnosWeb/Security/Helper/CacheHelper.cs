using CernunnosWeb.Security.Principal;
using System;
using System.Web;

namespace CernunnosWeb.Security.Helper
{
    /// <summary>
    /// Classe que gerencia o cache do sistema.
    /// </summary>
    public static class CacheHelper
    {
        /// <summary>
        /// Insert value into the cache using appropriate name/value pairs.
        /// </summary>
        /// <param name="key">Name of item.</param>
        /// <param name="o">Item to be cached.</param>
        public static void Add<T>(string key, T o) where T : class
        {
            HttpContext.Current.Cache.Insert(
                                             key,
                                             o,
                                             null,
                                             DateTime.UtcNow.AddMinutes(10),
                                             System.Web.Caching.Cache.NoSlidingExpiration);
        }

        /// <summary>
        /// Insert value into the cache using
        /// appropriate name/value pairs.
        /// </summary>
        /// <param name="key">Name of item.</param>
        /// <param name="o">Item to be cached.</param>
        /// <param name="min">Minutes do expire.</param>
        public static void Add<T>(string key, T o, int min) where T : class
        {
            HttpContext.Current.Cache.Insert(
                                             key,
                                             o,
                                             null,
                                             DateTime.UtcNow.AddMinutes(min),
                                             System.Web.Caching.Cache.NoSlidingExpiration);
        }

        /// <summary>
        /// Remove item from cache.
        /// </summary>
        /// <param name="key">Name of cached item.</param>
        public static void Clear(string key)
        {
            HttpContext.Current.Cache.Remove(key);
        }

        /// <summary>
        /// Check for item in cache.
        /// </summary>
        /// <param name="key">Name of cached item.</param>
        /// <returns>If key exists.</returns>
        public static bool Exists(string key)
        {
            return HttpContext.Current.Cache[key] != null;
        }

        /// <summary>
        /// Retrieve cached item.
        /// </summary>
        /// <typeparam name="T">Type of cached item.</typeparam>
        /// <param name="key">Name of cached item.</param>
        /// <returns>Cached item as type.</returns>
        public static T Get<T>(string key) where T : class
        {
            try
            {
                return (T)HttpContext.Current.Cache[key];
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Set cached item.
        /// </summary>
        /// <typeparam name="T">Type of cached item.</typeparam>
        /// <param name="key">Name of cached item.</param>
        /// <param name="o">Item to be cached.</param>
        public static void Set<T>(string key, T o) where T : class
        {
            if (Exists(key))
            {
                HttpContext.Current.Cache[key] = o;
            }
        }

        /// <summary>
        /// Insert value into the cache using
        /// appropriate name/value pairs.
        /// </summary>
        /// <param name="key">Name of item.</param>
        /// <param name="o">Item to be cached.</param>
        /// <param name="min">Minutes do expire.</param>
        public static void AddToUser<T>(string key, T o, int min) where T : class
        {
            if (HttpContext.Current != null && HttpContext.Current.User != null && HttpContext.Current.User.Identity is CustomIdentity)
            {
                key += ((CustomIdentity)HttpContext.Current.User.Identity).IdUsuario.ToString();
            }

            HttpContext.Current.Cache.Insert(
                                             key,
                                             o,
                                             null,
                                             DateTime.UtcNow.AddMinutes(min),
                                             System.Web.Caching.Cache.NoSlidingExpiration);
        }

        /// <summary>
        /// Remove item from cache.
        /// </summary>
        /// <param name="key">Name of cached item.</param>
        public static void ClearFromUser(string key)
        {
            if (HttpContext.Current != null && HttpContext.Current.User != null && HttpContext.Current.User.Identity is CustomIdentity)
            {
                key += ((CustomIdentity)HttpContext.Current.User.Identity).IdUsuario.ToString();
            }

            HttpContext.Current.Cache.Remove(key);
        }

        /// <summary>
        /// Retrieve cached item.
        /// </summary>
        /// <typeparam name="T">Type of cached item.</typeparam>
        /// <param name="key">Name of cached item.</param>
        /// <returns>Cached item as type.</returns>
        public static T GetFromUser<T>(string key) where T : class
        {
            try
            {
                if (HttpContext.Current != null && HttpContext.Current.User != null && HttpContext.Current.User.Identity is CustomIdentity)
                {
                    key += ((CustomIdentity)HttpContext.Current.User.Identity).IdUsuario.ToString();
                }

                return (T)HttpContext.Current.Cache[key];
            }
            catch
            {
                return null;
            }
        }
    }
}
