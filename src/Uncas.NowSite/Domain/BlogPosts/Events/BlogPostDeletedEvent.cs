using SimpleCqrs.Eventing;

namespace Uncas.NowSite.Web.Models.Events
{
    public class BlogPostDeletedEvent : DomainEvent
    {
        public string Title { get; set; }
        public string Content { get; set; }
    }
}