using System;
using System.Collections.Generic;
using System.Linq;
using Uncas.NowSite.Domain.Infrastructure;

namespace Uncas.NowSite.Domain.ReadModels
{
    public class BlogPostReadModel : ReadModel
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime Created { get; set; }
        public DateTime Published { get; set; }
        public IEnumerable<PictureReadModel> Pictures { get; set; }

        public override string ModelName
        {
            get { return "BlogPostReadModel"; }
        }

        public PictureReadModel PrimaryPicture
        {
            get
            {
                return Pictures != null ?
                    Pictures.FirstOrDefault() :
                    null;
            }
        }
    }
}