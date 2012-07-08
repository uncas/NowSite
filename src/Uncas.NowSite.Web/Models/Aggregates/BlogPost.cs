using System;
using SimpleCqrs.Domain;
using Uncas.NowSite.Web.Models.Events;

namespace Uncas.NowSite.Web.Models.Aggregates
{
    public class BlogPost : AggregateRoot
    {
        public BlogPost()
        {
        }

        public BlogPost(Guid id)
        {
            Apply(new BlogPostCreatedEvent { AggregateRootId = id });
        }

        public string Title { get; private set; }
        public string Content { get; private set; }

        internal void AddInfo(string title, string content)
        {
            Apply(new BlogPostInfoAddedEvent
            {
                AggregateRootId = Id,
                Title = title,
                Content = content
            });
        }

        internal void Publish()
        {
            Apply(new BlogPostPublishedEvent
            {
                AggregateRootId = Id,
                Title = Title,
                Content = Content
            });
        }

        internal void StartEdit()
        {
            Apply(new EditBlogPostStartedEvent { AggregateRootId = Id });
        }

        public void OnBlogPostCreated(BlogPostCreatedEvent blogPostCreated)
        {
            Id = blogPostCreated.AggregateRootId;
        }

        public void OnBlogPostInfoAdded(BlogPostInfoAddedEvent blogPostInfoAdded)
        {
            Title = blogPostInfoAdded.Title;
            Content = blogPostInfoAdded.Content;
        }
    }
}