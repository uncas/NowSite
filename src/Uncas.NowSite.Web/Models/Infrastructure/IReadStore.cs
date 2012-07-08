using System;
using System.Collections.Generic;

namespace Uncas.NowSite.Web.Models.Infrastructure
{
    public interface IReadStore<T>
    {
        void Add(T blogPost);
        IEnumerable<T> GetAll();
        T GetById(Guid id);
        void Delete(Guid id);
    }
}