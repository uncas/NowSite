using System;
using SimpleCqrs.Domain;
using Uncas.NowSite.Domain.Facts.Events;

namespace Uncas.NowSite.Domain.Facts
{
    public class Fact : AggregateRoot
    {
        public Fact(Guid id, string name)
        {
            Apply(new FactCreatedEvent
            {
                AggregateRootId = id,
                Name = name
            });
        }

        public string Name { get; private set; }

        public void OnFactCreated(FactCreatedEvent factCreated)
        {
            Id = factCreated.AggregateRootId;
            Name = factCreated.Name;
        }
    }
}