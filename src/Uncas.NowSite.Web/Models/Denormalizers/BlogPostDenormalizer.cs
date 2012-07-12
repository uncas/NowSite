using System;
using System.Collections.Generic;
using System.Linq;
using SimpleCqrs.Eventing;
using Uncas.NowSite.Web.Models.Events;
using Uncas.NowSite.Web.Models.Infrastructure;
using Uncas.NowSite.Web.Models.ReadModels;

namespace Uncas.NowSite.Web.Models.Denormalizers
{
    public class BlogPostDenormalizer :
        IHandleDomainEvents<BlogPostCreatedEvent>,
        IHandleDomainEvents<BlogPostPublishedEvent>,
        IHandleDomainEvents<BlogPostDeletedEvent>,
        IHandleDomainEvents<PictureCreatedEvent>,
        IHandleDomainEvents<PictureAddedToBlogPostEvent>
    {
        private readonly IReadStore _readStore;
        private readonly IEventStore _eventStore;

        public BlogPostDenormalizer(
            IReadStore readStore,
            IEventStore eventStore)
        {
            _readStore = readStore;
            _eventStore = eventStore;
        }

        public void Handle(BlogPostCreatedEvent domainEvent)
        {
            Guid id = domainEvent.AggregateRootId;
            _readStore.Add(new BlogPostMasterModel
            {
                Id = id
            });
            _readStore.Add(new EditBlogPostReadModel
            {
                Id = id,
                Pictures = new List<PictureReadModel>()
            });
        }

        public void Handle(BlogPostPublishedEvent domainEvent)
        {
            Guid id = domainEvent.AggregateRootId;
            _readStore.Delete<DeletedBlogPostModel>(id);
            DomainEvent createdEvent =
                _eventStore.GetEventsByEventTypes(
                new[] { typeof(BlogPostCreatedEvent) },
                id).SingleOrDefault();
            IList<PictureReadModel> pictures = domainEvent.Pictures.Select(
                pictureId => _readStore.GetById<PictureReadModel>(pictureId)).
                ToList();
            _readStore.Add(new BlogPostReadModel
            {
                Id = id,
                Title = domainEvent.Title,
                Content = domainEvent.Content,
                Created = createdEvent.EventDate,
                Published = domainEvent.EventDate,
                Pictures = pictures
            });
            _readStore.Add(new EditBlogPostReadModel
            {
                Id = id,
                Title = domainEvent.Title,
                Content = domainEvent.Content,
                Pictures = pictures
            });
        }

        public void Handle(BlogPostDeletedEvent domainEvent)
        {
            Guid id = domainEvent.AggregateRootId;
            _readStore.Delete<BlogPostReadModel>(id);
            _readStore.Delete<EditBlogPostReadModel>(id);
            _readStore.Add(new DeletedBlogPostModel
            {
                Id = id,
                Title = domainEvent.Title,
                Content = domainEvent.Content
            });
        }

        public void Handle(PictureCreatedEvent domainEvent)
        {
            _readStore.Add(new PictureReadModel
            {
                Id = domainEvent.AggregateRootId,
                PictureUrl = domainEvent.PictureUrl
            });
        }

        public void Handle(PictureAddedToBlogPostEvent domainEvent)
        {
            EditBlogPostReadModel readModel =
                _readStore.GetById<EditBlogPostReadModel>(domainEvent.AggregateRootId);
            readModel.Pictures.Add(
                _readStore.GetById<PictureReadModel>(domainEvent.PictureId));
            _readStore.Add(readModel);
        }
    }
}