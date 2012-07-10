using Uncas.NowSite.Web.Models.Infrastructure;
using Uncas.NowSite.Web.Models.ReadModels;
using Uncas.NowSite.Web.Utilities;

namespace Uncas.NowSite.Web.Models.ReadStores
{
    public class BlogPostReadStore :
        CachedReadStore<BlogPostReadModel>,
        IBlogPostReadStore
    {
        public BlogPostReadStore(
            string path,
            IStringSerializer stringSerializer,
            ICache cache)
            : base(
            path,
            stringSerializer,
            "BlogPostReadModel",
            cache)
        {
        }
    }
}