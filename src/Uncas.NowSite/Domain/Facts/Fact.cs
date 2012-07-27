using System;
using SimpleCqrs.Domain;
using Uncas.NowSite.Domain.Facts.Events;

namespace Uncas.NowSite.Domain.Facts
{
    public class Fact : AggregateRoot
    {
        public Fact()
        {
        }

        public Fact(Guid id, string name)
        {
            Apply(new FactCreatedEvent
            {
                AggregateRootId = id,
                Name = name
            });
        }

        public string Name { get; private set; }

        internal void ChangeName(string name)
        {
            if (string.Equals(name, Name))
            {
                return;
            }

            Apply(new FactNameChangedEvent
            {
                AggregateRootId = Id,
                Name = name
            });
        }

        internal void Delete()
        {
            Apply(new FactDeletedEvent { AggregateRootId = Id });
        }

        public void OnFactCreated(FactCreatedEvent factCreated)
        {
            Id = factCreated.AggregateRootId;
            Name = factCreated.Name;
        }

        public void OnFactNameChanged(
            FactNameChangedEvent factNameChanged)
        {
            Name = factNameChanged.Name;
        }

        public void OnFactDeleted(
            FactDeletedEvent factDeleted)
        {
        }
    }
}