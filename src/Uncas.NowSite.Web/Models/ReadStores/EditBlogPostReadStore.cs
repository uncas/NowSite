using Uncas.NowSite.Web.Models.Infrastructure;
using Uncas.NowSite.Web.Models.ReadModels;
using Uncas.NowSite.Web.Utilities;

namespace Uncas.NowSite.Web.Models.ReadStores
{
    public class EditBlogPostReadStore :
          ReadStore,
          IEditBlogPostReadStore
    {
        public EditBlogPostReadStore(
              string path,
              IStringSerializer stringSerializer)
            : base(
                path,
                stringSerializer,
                "EditBlogPostReadModel")
        {
        }
    }
}