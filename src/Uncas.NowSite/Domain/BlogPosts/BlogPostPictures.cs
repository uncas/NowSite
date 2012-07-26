using System;
using System.Collections.Generic;
using SimpleCqrs.Domain;

namespace Uncas.NowSite.Domain.BlogPosts
{
    public class BlogPostPictures : Entity
    {
        private readonly IList<Guid> _pictures;

        public BlogPostPictures()
        {
            _pictures = new List<Guid>();
        }

        internal void Add(Guid pictureId)
        {
            _pictures.Add(pictureId);
        }

        internal IEnumerable<Guid> GetAll()
        {
            return _pictures;
        }
    }
}