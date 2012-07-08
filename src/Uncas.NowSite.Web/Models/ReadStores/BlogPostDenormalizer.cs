using SimpleCqrs.Eventing;
using Uncas.Core;
using Uncas.NowSite.Web.Models.Events;

namespace Uncas.NowSite.Web.Models.ReadStores
{
    public class BlogPostDenormalizer :
         IHandleDomainEvents<BlogPostPublishedEvent>
    {
        private readonly IBlogPostReadStore _readStore;

        public BlogPostDenormalizer(IBlogPostReadStore readStore)
        {
            _readStore = readStore;
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
    }
}