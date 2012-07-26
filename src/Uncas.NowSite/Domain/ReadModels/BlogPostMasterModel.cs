using System;
using Uncas.NowSite.Domain.Infrastructure;

namespace Uncas.NowSite.Domain.ReadModels
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