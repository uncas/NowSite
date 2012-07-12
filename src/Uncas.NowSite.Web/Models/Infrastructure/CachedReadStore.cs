using System;
using System.Collections.Generic;
using Uncas.NowSite.Web.Utilities;

namespace Uncas.NowSite.Web.Models.Infrastructure
{
    public abstract class CachedReadStore : ReadStore
    {
        private readonly ICache _cache;

        protected CachedReadStore(
            string path,
            IStringSerializer stringSerializer,
            string modelName,
            ICache cache)
            : base(path, stringSerializer, modelName)
        {
            _cache = cache;
        }

        public override void Add<T>(T model)
        {
            base.Add(model);
            _cache.Remove(GetCacheKey(model.Id));
            _cache.Remove(GetAllCacheKey());
        }

        public override void Delete<T>(Guid id)
        {
            base.Delete<T>(id);
            _cache.Remove(GetCacheKey(id));
            _cache.Remove(GetAllCacheKey());
        }

        public override IEnumerable<T> GetAll<T>()
        {
            return _cache.Get(GetAllCacheKey(), base.GetAll<T>);
        }

        public override T GetById<T>(Guid id)
        {
            return _cache.Get(GetCacheKey(id), () => base.GetById<T>(id));
        }

        private string GetCacheKey(Guid id)
        {
            return string.Format(
                "{0}-{1}",
                _modelName,
                id);
        }

        private string GetAllCacheKey()
        {
            return GetCacheKey(new Guid());
        }
    }
}