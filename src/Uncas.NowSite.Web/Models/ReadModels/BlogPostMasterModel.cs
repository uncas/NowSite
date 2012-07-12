using System;
using Uncas.NowSite.Web.Models.Infrastructure;

namespace Uncas.NowSite.Web.Models.ReadModels
{
    public class BlogPostMasterModel : ReadModel
    {
        public override string ModelName
        {
            get { return "BlogPostMasterModel"; }
        }

        public DateTime Created { get; set; }
    }
}