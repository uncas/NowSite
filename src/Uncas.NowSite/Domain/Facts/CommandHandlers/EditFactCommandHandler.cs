using SimpleCqrs.Commanding;
using SimpleCqrs.Domain;
using Uncas.NowSite.Domain.Facts.Commands;

namespace Uncas.NowSite.Domain.Facts.CommandHandlers
{
    public class EditFactCommandHandler
        : CommandHandler<EditFactCommand>
    {
        private readonly IDomainRepository _domainRepository;

        public EditFactCommandHandler(
            IDomainRepository domainRepository)
        {
            _domainRepository = domainRepository;
        }

        public override void Handle(EditFactCommand command)
        {
            Fact fact = _domainRepository.GetById<Fact>(command.Id);
            fact.ChangeName(command.Name);
            _domainRepository.Save(fact);
        }
    }
}