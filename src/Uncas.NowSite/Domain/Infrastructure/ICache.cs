using System;

namespace Uncas.NowSite.Web.Models.Infrastructure
{
    public interface ICache
    {
        void Add(string key, object model);
        void Remove(string key);
        object Get(string key);
        T Get<T>(string key, Func<T> get);
    }
}