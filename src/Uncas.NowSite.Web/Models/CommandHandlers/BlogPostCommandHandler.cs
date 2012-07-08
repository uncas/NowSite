using SimpleCqrs.Commanding;
using SimpleCqrs.Domain;

namespace Uncas.NowSite.Web.Models.CommandHandlers
{
    public abstract class BlogPostCommandHandler<T> :
       CommandHandler<T> where T : ICommand
    {
        protected readonly IDomainRepository _domainRepository;

        protected BlogPostCommandHandler(
            IDomainRepository domainRepository)
        {
            _domainRepository = domainRepository;
        }
    }
}