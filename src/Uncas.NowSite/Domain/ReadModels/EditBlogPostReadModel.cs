using System.Collections.Generic;
using Uncas.NowSite.Domain.Infrastructure;

namespace Uncas.NowSite.Domain.ReadModels
{
    public class EditBlogPostReadModel : ReadModel
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public IList<PictureReadModel> Pictures { get; set; }

        public override string ModelName
        {
            get { return "EditBlogPostReadModel"; }
        }
    }
}