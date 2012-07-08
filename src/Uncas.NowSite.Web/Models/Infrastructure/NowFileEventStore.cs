using System;
using System.Collections.Generic;
using SimpleCqrs;
using SimpleCqrs.Eventing;
using SimpleCqrs.EventStore.File;

namespace Uncas.NowSite.Web
{
    public class NowFileEventStore : IEventStore
    {
        private readonly FileEventStore _fileEventStore;

        public NowFileEventStore(
            string baseDirectory,
            ITypeCatalog typeCatalog)
        {
            _fileEventStore = new FileEventStore(baseDirectory, typeCatalog);
        }

        public IEnumerable<DomainEvent> GetEvents(
            Guid aggregateRootId,
            int startSequence)
        {
            return _fileEventStore.GetEvents(
                aggregateRootId,
                startSequence);
        }

        public IEnumerable<DomainEvent> GetEventsByEventTypes(
            IEnumerable<System.Type> domainEventTypes,
            DateTime startDate,
            DateTime endDate)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<DomainEvent> GetEventsByEventTypes(
            IEnumerable<Type> domainEventTypes,
            Guid aggregateRootId)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<DomainEvent> GetEventsByEventTypes(
            IEnumerable<Type> domainEventTypes)
        {
            throw new System.NotImplementedException();
        }

        public void Insert(IEnumerable<DomainEvent> domainEvents)
        {
            _fileEventStore.Insert(domainEvents);
        }
    }
}