using System;

namespace Uncas.NowSite.Web.Controllers
{
    public class EditBlogPostInputModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
    }
}