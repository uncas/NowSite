using System;
using System.Linq;
using SimpleCqrs.Eventing;
using Uncas.NowSite.Web.Models.Events;

namespace Uncas.NowSite.Web.Models.ReadStores
{
    public class BlogPostDenormalizer :
        IHandleDomainEvents<BlogPostCreatedEvent>,
        IHandleDomainEvents<BlogPostPublishedEvent>,
        IHandleDomainEvents<BlogPostDeletedEvent>
    {
        private readonly IBlogPostMasterStore _masterStore;
        private readonly IBlogPostReadStore _readStore;
        private readonly IDeletedBlogPostStore _deletedStore;
        private readonly IEventStore _eventStore;

        public BlogPostDenormalizer(
            IBlogPostMasterStore masterStore,
            IBlogPostReadStore readStore,
            IDeletedBlogPostStore deletedStore,
            IEventStore eventStore)
        {
            _masterStore = masterStore;
            _readStore = readStore;
            _deletedStore = deletedStore;
            _eventStore = eventStore;
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
            _readStore.Add(new BlogPostReadModel
            {
                Id = id,
                Title = domainEvent.Title,
                Content = domainEvent.Content,
                Created = createdEvent.EventDate,
                Published = domainEvent.EventDate
            });
        }

        public void Handle(BlogPostDeletedEvent domainEvent)
        {
            _readStore.Delete(domainEvent.AggregateRootId);
            _deletedStore.Add(new DeletedBlogPostModel
            {
                Id = domainEvent.AggregateRootId,
                Title = domainEvent.Title,
                Content = domainEvent.Content
            });
        }
    }
}