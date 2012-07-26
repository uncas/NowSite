using SimpleCqrs.Domain;
using Uncas.NowSite.Domain.BlogPosts.Commands;

namespace Uncas.NowSite.Domain.BlogPosts.CommandHandlers
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