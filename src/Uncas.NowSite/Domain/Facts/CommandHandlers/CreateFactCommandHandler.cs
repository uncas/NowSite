using SimpleCqrs.Commanding;
using SimpleCqrs.Domain;
using Uncas.NowSite.Domain.Facts.Commands;

namespace Uncas.NowSite.Domain.Facts.CommandHandlers
{
    public class CreateFactCommandHandler
        : CommandHandler<CreateFactCommand>
    {
        private readonly IDomainRepository _domainRepository;

        public CreateFactCommandHandler(
            IDomainRepository domainRepository)
        {
            _domainRepository = domainRepository;
        }

        public override void Handle(CreateFactCommand command)
        {
            var fact = new Fact(command.Id, command.Name);
            _domainRepository.Save(fact);
        }
    }
}