using System.Collections.Generic;

namespace Uncas.NowSite.Web.Models.ReadStores
{
    public interface IBlogPostReadStore
    {
        void AddBlogPost(BlogPostReadModel blogPost);
        IEnumerable<BlogPostReadModel> GetBlogPosts();
    }
}