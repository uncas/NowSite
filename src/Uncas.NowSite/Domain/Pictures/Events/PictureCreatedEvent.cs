using SimpleCqrs.Eventing;

namespace Uncas.NowSite.Web.Models.Events
{
    public class PictureCreatedEvent : DomainEvent
    {
        public string PictureUrl { get; set; }
    }
}