using Uncas.NowSite.Web.Models.Infrastructure;
using Uncas.NowSite.Web.Utilities;

namespace Uncas.NowSite.Web.Models.ReadStores
{
    public class DeletedBlogPostStore : ReadStore, IDeletedBlogPostStore
    {
        public DeletedBlogPostStore(
            string path,
            IStringSerializer stringSerializer)
            : base(
            path,
            stringSerializer)
        {
        }
    }
}