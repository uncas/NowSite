using System.Collections.Generic;
using System.Linq;

namespace Uncas.NowSite.Web.Models.ReadStores
{
    public class BlogPostReadStore : IBlogPostReadStore
    {
        private static readonly IList<BlogPostReadModel> _documentStore=
            new List<BlogPostReadModel>();

        public void AddBlogPost(BlogPostReadModel blogPost)
        {
            _documentStore.Add(blogPost);
        }

        public IEnumerable<BlogPostReadModel> GetBlogPosts()
        {
            return _documentStore;
        }
    }
}