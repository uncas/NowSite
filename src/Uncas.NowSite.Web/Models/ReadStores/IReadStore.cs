using System;
using System.Collections.Generic;

namespace Uncas.NowSite.Web.Models.ReadStores
{
    public interface IReadStore<T>
    {
        void Add(T blogPost);
        IEnumerable<T> GetAll();
        T GetById(Guid id);
    }
}