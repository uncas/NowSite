using SimpleCqrs.Eventing;

namespace Uncas.NowSite.Domain.Facts.Events
{
    public class FactCreatedEvent : DomainEvent
    {
        public string Name { get; set; }
    }
}