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
        public BlogPostState State { get; private set; }

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

        internal void Delete()
        {
            Apply(new BlogPostDeletedEvent
            {
                AggregateRootId = Id,
                Title = Title,
                Content = Content
            });
        }

        public void OnBlogPostCreated(BlogPostCreatedEvent blogPostCreated)
        {
            Id = blogPostCreated.AggregateRootId;
            State = BlogPostState.Created;
        }

        public void OnBlogPostInfoAdded(BlogPostInfoAddedEvent blogPostInfoAdded)
        {
            Title = blogPostInfoAdded.Title;
            Content = blogPostInfoAdded.Content;
        }

        public void OnBlogPostPublished(BlogPostPublishedEvent blogPostPublished)
        {
            State = BlogPostState.Published;
        }

        public void OnBlogPostDeleted(BlogPostDeletedEvent blogPostDeleted)
        {
            State = BlogPostState.Deleted;
        }
    }
}