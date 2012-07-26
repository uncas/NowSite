using SimpleCqrs.Eventing;

namespace Uncas.NowSite.Domain.Pictures.Events
{
    public class PictureCreatedEvent : DomainEvent
    {
        public string PictureUrl { get; set; }
    }
}