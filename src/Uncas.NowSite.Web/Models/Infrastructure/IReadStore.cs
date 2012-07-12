using System;
using System.Collections.Generic;

namespace Uncas.NowSite.Web.Models.Infrastructure
{
    public interface IReadStore<T> where T : ReadModel
    {
        void Add(T blogPost);
        IEnumerable<T> GetAll<T>();
        T GetById<T>(Guid id);
        void Delete<T>(Guid id);
    }
}