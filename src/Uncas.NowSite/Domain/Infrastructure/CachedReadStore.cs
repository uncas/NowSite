using System;
using System.Collections.Generic;
using Uncas.NowSite.Web.Utilities;

namespace Uncas.NowSite.Web.Models.Infrastructure
{
    public class CachedReadStore : ReadStore
    {
        private readonly ICache _cache;

        public CachedReadStore(
            string path,
            IStringSerializer stringSerializer,
            ICache cache)
            : base(path, stringSerializer)
        {
            _cache = cache;
        }

        public override void Add<T>(T model)
        {
            base.Add(model);
            _cache.Remove(GetCacheKey<T>(model.Id));
            _cache.Remove(GetAllCacheKey<T>());
        }

        public override void Delete<T>(Guid id)
        {
            base.Delete<T>(id);
            _cache.Remove(GetCacheKey<T>(id));
            _cache.Remove(GetAllCacheKey<T>());
        }

        public override IEnumerable<T> GetAll<T>()
        {
            return _cache.Get(GetAllCacheKey<T>(), base.GetAll<T>);
        }

        public override T GetById<T>(Guid id)
        {
            return _cache.Get(GetCacheKey<T>(id), () => base.GetById<T>(id));
        }

        private string GetCacheKey<T>(Guid id) where T : ReadModel
        {
            return string.Format(
                "{0}-{1}",
                GetModelName<T>(),
                id);
        }

        private string GetAllCacheKey<T>() where T : ReadModel
        {
            return string.Format(
               "{0}-All",
               GetModelName<T>());
        }
    }
}