using System;
using System.Collections.Generic;

namespace Uncas.NowSite.Web.Models.ReadStores
{
    public interface IBlogPostReadStore
    {
        void Add(BlogPostReadModel blogPost);
        IEnumerable<BlogPostReadModel> GetAll();
        BlogPostReadModel GetById(Guid id);
    }
}