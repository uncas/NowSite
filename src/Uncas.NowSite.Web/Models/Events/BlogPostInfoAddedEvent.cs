using SimpleCqrs.Eventing;

namespace Uncas.NowSite.Web.Models.Aggregates
{
    public class BlogPostInfoAddedEvent : DomainEvent
    {
        public string Title { get; set; }
        public string Content { get; set; }
    }
}
