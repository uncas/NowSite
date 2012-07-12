using Uncas.NowSite.Web.Models.Infrastructure;
using Uncas.NowSite.Web.Models.ReadModels;
using Uncas.NowSite.Web.Utilities;

namespace Uncas.NowSite.Web.Models.ReadStores
{
    public class BlogPostMasterStore :
        ReadStore,
        IBlogPostMasterStore
    {
        public BlogPostMasterStore(
            string path,
            IStringSerializer stringSerializer)
            : base(
            path,
            stringSerializer,
            "BlogPostMasterModel")
        {
        }
    }
}