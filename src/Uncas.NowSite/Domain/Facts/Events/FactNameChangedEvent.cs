using SimpleCqrs.Eventing;

namespace Uncas.NowSite.Domain.Facts.Events
{
    public class FactNameChangedEvent : DomainEvent
    {
        public string Name { get; set; }
    }
}