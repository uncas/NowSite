using System;
using SimpleCqrs.Domain;
using Uncas.NowSite.Web.Models.Events;

namespace Uncas.NowSite.Web.Models.Aggregates
{
    public class BlogPost : AggregateRoot
    {
        public BlogPost()
        {
            Pictures = new BlogPostPictures();
        }

        public BlogPost(Guid id)
            : base()
        {
            Apply(new BlogPostCreatedEvent { AggregateRootId = id });
        }

        public string Title { get; private set; }
        public string Content { get; private set; }
        public DateTime Created { get; private set; }
        public DateTime? Published { get; private set; }
        public BlogPostState State { get; private set; }
        public BlogPostPictures Pictures { get; set; }

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
                Content = Content,
                Pictures = Pictures.GetAll()
            });
        }


        internal void StartEdit()
        {
            Apply(new EditBlogPostStartedEvent { AggregateRootId = Id });
        }

        internal void AddPicture(Guid pictureId)
        {
            Apply(new PictureAddedToBlogPostEvent
            {
                AggregateRootId = Id,
                PictureId = pictureId
            });
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
            Created = blogPostCreated.EventDate;
        }

        public void OnBlogPostInfoAdded(BlogPostInfoAddedEvent blogPostInfoAdded)
        {
            Title = blogPostInfoAdded.Title;
            Content = blogPostInfoAdded.Content;
        }

        public void OnBlogPostPublished(BlogPostPublishedEvent blogPostPublished)
        {
            State = BlogPostState.Published;
            Published = blogPostPublished.EventDate;
        }

        public void OnPictureAddedToBlogPost(
            PictureAddedToBlogPostEvent pictureAdded)
        {
            Pictures.Add(pictureAdded.PictureId);
        }

        public void OnBlogPostDeleted(BlogPostDeletedEvent blogPostDeleted)
        {
            State = BlogPostState.Deleted;
        }
    }
}