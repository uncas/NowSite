using SimpleCqrs.Commanding;
using SimpleCqrs.Domain;
using Uncas.NowSite.Web.Models.Aggregates;
using Uncas.NowSite.Web.Models.Commands;

namespace Uncas.NowSite.Web.Models.CommandHandlers
{
    public class CreateBlogPostCommandHandler :
        CommandHandler<CreateBlogPostCommand>
    {
        private readonly IDomainRepository _domainRepository;

        public CreateBlogPostCommandHandler(
            IDomainRepository domainRepository)
        {
            _domainRepository = domainRepository;
        }

        public override void Handle(CreateBlogPostCommand command)
        {
            var blogPost = new BlogPost(command.Id);
            _domainRepository.Save(blogPost);
        }
    }
}