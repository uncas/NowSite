using System;
using System.Collections.Generic;

namespace Uncas.NowSite.Web.Models.InputModels
{
    public class EditBlogPostInputModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public IEnumerable<PictureInputModel> Pictures { get; set; }
    }
}