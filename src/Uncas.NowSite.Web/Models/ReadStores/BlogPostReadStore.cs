using Uncas.NowSite.Web.Models.Infrastructure;
using Uncas.NowSite.Web.Utilities;

namespace Uncas.NowSite.Web.Models.ReadStores
{
    public class BlogPostReadStore :
        ReadStore<BlogPostReadModel>,
        IBlogPostReadStore
    {
        public BlogPostReadStore(
            string path,
            IStringSerializer stringSerializer)
            : base(
            path,
            stringSerializer,
            "BlogPostReadModel")
        {
        }
    }
}