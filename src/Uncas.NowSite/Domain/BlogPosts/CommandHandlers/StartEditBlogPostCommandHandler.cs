using SimpleCqrs.Domain;
using Uncas.NowSite.Web.Controllers;

namespace Uncas.NowSite.Web.Models.CommandHandlers
{
    public class StartEditBlogPostCommandHandler :
        BlogPostCommandHandler<StartEditBlogPostCommand>
    {
        public StartEditBlogPostCommandHandler(
            IDomainRepository domainRepository) :
            base(domainRepository)
        {
        }

        public override void Handle(StartEditBlogPostCommand command)
        {
            ActOnExisting(
                command.Id,
                blogPost => blogPost.StartEdit());
        }
    }
}