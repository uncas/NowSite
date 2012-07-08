using System;
using SimpleCqrs.Domain;
using Uncas.NowSite.Web.Models.Events;

namespace Uncas.NowSite.Web.Models.Aggregates
{
    public class BlogPost : AggregateRoot
    {
        public BlogPost(Guid id)
        {
            Apply(new BlogPostCreatedEvent { AggregateRootId = id });
        }

        public void OnBlogPostCreated(BlogPostCreatedEvent blogPostCreated)
        {
            Id = blogPostCreated.AggregateRootId;
        }
    }
}