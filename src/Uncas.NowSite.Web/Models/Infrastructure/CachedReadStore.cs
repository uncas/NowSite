using System;
using System.Collections.Generic;
using Uncas.NowSite.Web.Utilities;

namespace Uncas.NowSite.Web.Models.Infrastructure
{
    public abstract class CachedReadStore<T> :
        ReadStore<T>
        where T : ReadModel
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

        public override void Add(T model)
        {
            base.Add(model);
            _cache.Add(GetCacheKey(model.Id), model);
            _cache.Remove(GetAllCacheKey());
        }

        public override void Delete(Guid id)
        {
            base.Delete(id);
            _cache.Remove(GetCacheKey(id));
            _cache.Remove(GetAllCacheKey());
        }

        public override IEnumerable<T> GetAll()
        {
            var all = (IEnumerable<T>)_cache.Get(GetAllCacheKey());
            if (all == null)
            {
                all = base.GetAll();
                _cache.Add(GetAllCacheKey(), all);
            }

            return all;
        }

        public override T GetById(Guid id)
        {
            var item = (T)_cache.Get(GetCacheKey(id));
            if (item == null)
            {
                item = base.GetById(id);
                _cache.Add(GetCacheKey(id), item);
            }

            return item;
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