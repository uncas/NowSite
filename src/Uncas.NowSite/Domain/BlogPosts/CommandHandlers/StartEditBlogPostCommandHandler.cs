using SimpleCqrs.Domain;
using Uncas.NowSite.Domain.BlogPosts.Commands;

namespace Uncas.NowSite.Domain.BlogPosts.CommandHandlers
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