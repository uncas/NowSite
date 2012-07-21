using SimpleCqrs.Domain;
using Uncas.NowSite.Web.Models.Commands;

namespace Uncas.NowSite.Web.Models.CommandHandlers
{
    public class DeleteBlogPostCommandHandler :
        BlogPostCommandHandler<DeleteBlogPostCommand>
    {
        public DeleteBlogPostCommandHandler(
            IDomainRepository domainRepository) :
            base(domainRepository)
        {
        }

        public override void Handle(DeleteBlogPostCommand command)
        {
            ActOnExisting(
                command.Id,
                blogPost => blogPost.Delete());
        }
    }
}