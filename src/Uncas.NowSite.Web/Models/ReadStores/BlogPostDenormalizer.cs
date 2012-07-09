using SimpleCqrs.Eventing;
using Uncas.Core;
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

        public BlogPostDenormalizer(
            IBlogPostMasterStore masterStore,
            IBlogPostReadStore readStore,
            IDeletedBlogPostStore deletedStore)
        {
            _masterStore = masterStore;
            _readStore = readStore;
            _deletedStore = deletedStore;
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
            _deletedStore.Delete(domainEvent.AggregateRootId);
            _readStore.Add(new BlogPostReadModel
            {
                Id = domainEvent.AggregateRootId,
                Title = domainEvent.Title,
                Content = domainEvent.Content,
                //Created = domainEvent.Created,
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