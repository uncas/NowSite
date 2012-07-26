﻿using System;
using Uncas.NowSite.Web.Models.Infrastructure;

namespace Uncas.NowSite.Web.Models.ReadModels
{
    public class DeletedBlogPostModel : ReadModel
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime Created { get; set; }

        public override string ModelName
        {
            get { return "DeletedBlogPostModel"; }
        }
    }
}