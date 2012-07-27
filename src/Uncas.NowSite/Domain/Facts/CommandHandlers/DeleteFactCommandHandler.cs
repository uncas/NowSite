using SimpleCqrs.Commanding;
using SimpleCqrs.Domain;
using Uncas.NowSite.Domain.Facts.Commands;

namespace Uncas.NowSite.Domain.Facts.CommandHandlers
{
    public class DeleteFactCommandHandler
        : CommandHandler<DeleteFactCommand>
    {
        private readonly IDomainRepository _domainRepository;

        public DeleteFactCommandHandler(
            IDomainRepository domainRepository)
        {
            _domainRepository = domainRepository;
        }

        public override void Handle(DeleteFactCommand command)
        {
            Fact fact = _domainRepository.GetById<Fact>(command.Id);
            fact.Delete();
            _domainRepository.Save(fact);
        }
    }
}