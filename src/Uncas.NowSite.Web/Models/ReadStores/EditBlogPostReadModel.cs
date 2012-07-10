using System.Collections.Generic;
using Uncas.NowSite.Web.Models.Infrastructure;

namespace Uncas.NowSite.Web.Models.ReadStores
{
    public class EditBlogPostReadModel : ReadModel
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public IList<PictureReadModel> Pictures { get; set; }
    }
}