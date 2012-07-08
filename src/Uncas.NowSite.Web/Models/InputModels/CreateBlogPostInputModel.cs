using System;

namespace Uncas.NowSite.Web.Models.InputModels
{
    public class CreateBlogPostInputModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
    }
}