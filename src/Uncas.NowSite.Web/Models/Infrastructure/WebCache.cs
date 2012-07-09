using System;
using System.Web;
using System.Web.Caching;

namespace Uncas.NowSite.Web.Models.Infrastructure
{
    public class WebCache : ICache
    {
        private static Cache _container =
            HttpContext.Current.Cache;

        public void Add(string key, object model)
        {
            _container.Add(
                key,
                model,
                null,
                DateTime.Now.AddMinutes(30d),
                TimeSpan.Zero,
                CacheItemPriority.Normal,
                null);
        }

        public void Remove(string key)
        {
            _container.Remove(key);
        }

        public object Get(string key)
        {
            return _container.Get(key);
        }
    }
}