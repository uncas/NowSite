using System;
using System.Collections.Generic;

namespace Uncas.NowSite.Domain.Infrastructure
{
    public interface IReadStore
    {
        void Add<T>(T blogPost) where T : ReadModel;
        IEnumerable<T> GetAll<T>() where T : ReadModel;
        T GetById<T>(Guid id) where T : ReadModel;
        void Delete<T>(Guid id) where T : ReadModel;
    }
}