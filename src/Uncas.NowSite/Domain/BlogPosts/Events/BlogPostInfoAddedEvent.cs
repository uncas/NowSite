using SimpleCqrs.Eventing;

namespace Uncas.NowSite.Domain.BlogPosts.Events
{
    public class BlogPostInfoAddedEvent : DomainEvent
    {
        public string Title { get; set; }
        public string Content { get; set; }
    }
}
