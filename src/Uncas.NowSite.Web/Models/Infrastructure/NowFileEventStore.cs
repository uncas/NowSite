using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using SimpleCqrs;
using SimpleCqrs.Eventing;
using SimpleCqrs.EventStore.File;

namespace Uncas.NowSite.Web
{
    public class NowFileEventStore : IEventStore
    {
        private readonly FileEventStore _fileEventStore;
        private readonly string _baseDirectory;
        private readonly DataContractSerializer _serializer;

        public NowFileEventStore(
            string baseDirectory,
            ITypeCatalog typeCatalog)
        {
            _fileEventStore = new FileEventStore(baseDirectory, typeCatalog);
            _baseDirectory = baseDirectory;
            var domainEventDerivedTypes =
                typeCatalog.GetDerivedTypes(typeof(DomainEvent));
            _serializer = new DataContractSerializer(
                typeof(DomainEvent),
                domainEventDerivedTypes);
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
            return GetEventsByEventTypes(domainEventTypes).
                Where(x => startDate <= x.EventDate &&
                    x.EventDate <= endDate);
        }

        public IEnumerable<DomainEvent> GetEventsByEventTypes(
            IEnumerable<Type> domainEventTypes,
            Guid aggregateRootId)
        {
            var eventInfos = GetEventInfosForAggregateRoot(aggregateRootId);
            return GetEventsByEventTypes(domainEventTypes, eventInfos);
        }

        public IEnumerable<DomainEvent> GetEventsByEventTypes(
            IEnumerable<Type> domainEventTypes)
        {
            var eventInfos = GetEventInfos();
            return GetEventsByEventTypes(domainEventTypes, eventInfos);
        }

        public void Insert(IEnumerable<DomainEvent> domainEvents)
        {
            _fileEventStore.Insert(domainEvents);
        }

        private IEnumerable<DomainEvent> GetEventsByEventTypes(
            IEnumerable<Type> domainEventTypes,
            IEnumerable<dynamic> eventInfos)
        {
            var domainEvents = new List<DomainEvent>();
            foreach (var eventInfo in eventInfos)
            {
                using (Stream stream = File.OpenRead(eventInfo.FilePath))
                {
                    var domainEvent =
                        (DomainEvent)_serializer.ReadObject(stream);
                    Type eventType = domainEvent.GetType();
                    if (domainEventTypes.Contains(eventType))
                        domainEvents.Add(domainEvent);
                }
            }

            return domainEvents;
        }

        private IEnumerable<dynamic> GetEventInfos()
        {
            if (!Directory.Exists(_baseDirectory))
                return new List<dynamic>();
            string[] filePaths = Directory.GetFiles(
                _baseDirectory,
                "*.xml",
                SearchOption.AllDirectories);
            return GetEventInfos(filePaths);
        }

        private IEnumerable<dynamic> GetEventInfosForAggregateRoot(
            Guid aggregateRootId)
        {
            var aggregateRootDirectory =
                Path.Combine(
                _baseDirectory,
                aggregateRootId.ToString());
            string[] filePaths = Directory.GetFiles(aggregateRootDirectory);
            return GetEventInfos(filePaths);
        }

        private static IEnumerable<dynamic> GetEventInfos(string[] filePaths)
        {
            return from filePath in filePaths
                   let fileName = Path.GetFileNameWithoutExtension(filePath)
                   where fileName != null
                   let sequence = int.Parse(fileName)
                   orderby sequence
                   select new { Sequence = sequence, FilePath = filePath };
        }
    }
}