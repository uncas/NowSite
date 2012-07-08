using SimpleCqrs.Eventing;
using Uncas.Core;
using Uncas.NowSite.Web.Models.Events;

namespace Uncas.NowSite.Web.Models.ReadStores
{
    public class BlogPostDenormalizer :
        IHandleDomainEvents<BlogPostPublishedEvent>,
        IHandleDomainEvents<BlogPostCreatedEvent>
    {
        private readonly IBlogPostReadStore _readStore;
        private readonly IBlogPostMasterStore _masterStore;

        public BlogPostDenormalizer(
            IBlogPostReadStore readStore,
            IBlogPostMasterStore masterStore)
        {
            _readStore = readStore;
            _masterStore = masterStore;
        }

        public void Handle(BlogPostPublishedEvent domainEvent)
        {
            _readStore.Add(new BlogPostReadModel
            {
                Id = domainEvent.AggregateRootId,
                Title = domainEvent.Title,
                Content = domainEvent.Content,
                Created = SystemTime.Now()
            });
        }

        public void Handle(BlogPostCreatedEvent domainEvent)
        {
            _masterStore.Add(new BlogPostMasterModel
            {
                Id = domainEvent.AggregateRootId
            });
        }
    }
}