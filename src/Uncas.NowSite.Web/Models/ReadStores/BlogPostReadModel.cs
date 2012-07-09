using System;
using Uncas.NowSite.Web.Models.Infrastructure;

namespace Uncas.NowSite.Web.Models.ReadStores
{
    public class BlogPostReadModel : ReadModel
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime Created { get; set; }
        public DateTime Published { get; set; }
    }
}