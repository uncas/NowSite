using System;
using System.Linq;
using SimpleCqrs.Eventing;
using Uncas.NowSite.Web.Models.Events;

namespace Uncas.NowSite.Web.Models.ReadStores
{
    public class BlogPostDenormalizer :
        IHandleDomainEvents<BlogPostCreatedEvent>,
        IHandleDomainEvents<BlogPostPublishedEvent>,
        IHandleDomainEvents<BlogPostDeletedEvent>,
        IHandleDomainEvents<PictureCreatedEvent>,
        IHandleDomainEvents<PictureAddedToBlogPostEvent>
    {
        private readonly IBlogPostMasterStore _masterStore;
        private readonly IBlogPostReadStore _readStore;
        private readonly IDeletedBlogPostStore _deletedStore;
        private readonly IPictureReadStore _pictureReadStore;
        private readonly IEventStore _eventStore;
        private readonly IEditBlogPostReadStore _editBlogPostReadStore;

        public BlogPostDenormalizer(
            IBlogPostMasterStore masterStore,
            IBlogPostReadStore readStore,
            IDeletedBlogPostStore deletedStore,
            IPictureReadStore pictureReadStore,
            IEditBlogPostReadStore editBlogPostReadStore,
            IEventStore eventStore)
        {
            _masterStore = masterStore;
            _readStore = readStore;
            _deletedStore = deletedStore;
            _pictureReadStore = pictureReadStore;
            _eventStore = eventStore;
            _editBlogPostReadStore = editBlogPostReadStore;
        }

        public void Handle(BlogPostCreatedEvent domainEvent)
        {
            _masterStore.Add(new BlogPostMasterModel
            {
                Id = domainEvent.AggregateRootId
            });
        }

        public void Handle(BlogPostPublishedEvent domainEvent)
        {
            Guid id = domainEvent.AggregateRootId;
            _deletedStore.Delete(id);
            DomainEvent createdEvent =
                _eventStore.GetEventsByEventTypes(
                new[] { typeof(BlogPostCreatedEvent) },
                id).SingleOrDefault();
            var pictures = domainEvent.Pictures.Select(
                pictureId => _pictureReadStore.GetById(pictureId)).
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
            _editBlogPostReadStore.Add(new EditBlogPostReadModel
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
            _readStore.Delete(id);
            _editBlogPostReadStore.Delete(id);
            _deletedStore.Add(new DeletedBlogPostModel
            {
                Id = id,
                Title = domainEvent.Title,
                Content = domainEvent.Content
            });
        }

        public void Handle(PictureCreatedEvent domainEvent)
        {
            _pictureReadStore.Add(new PictureReadModel
            {
                Id = domainEvent.AggregateRootId,
                PictureUrl = domainEvent.PictureUrl
            });
        }

        public void Handle(PictureAddedToBlogPostEvent domainEvent)
        {
            EditBlogPostReadModel readModel =
                _editBlogPostReadStore.GetById(domainEvent.AggregateRootId);
            readModel.Pictures.Add(
                _pictureReadStore.GetById(domainEvent.PictureId));
            _editBlogPostReadStore.Add(readModel);
        }
    }
}