using SimpleCqrs.Eventing;
using Uncas.NowSite.Domain.Facts.Events;
using Uncas.NowSite.Domain.Infrastructure;
using Uncas.NowSite.Domain.ReadModels;

namespace Uncas.NowSite.Domain.Facts
{
    public class FactDenormalizer :
        IHandleDomainEvents<FactCreatedEvent>
    {
        private readonly IReadStore _readStore;

        public FactDenormalizer(
            IReadStore readStore)
        {
            _readStore = readStore;
        }

        public void Handle(FactCreatedEvent domainEvent)
        {
            _readStore.Add(new FactReadModel
            {
                Id = domainEvent.AggregateRootId,
                Name = domainEvent.Name
            });
        }
    }
}