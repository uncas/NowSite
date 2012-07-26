using SimpleCqrs.Domain;
using Uncas.NowSite.Domain.BlogPosts.Commands;

namespace Uncas.NowSite.Domain.BlogPosts.CommandHandlers
{
    public class PublishBlogPostCommandHandler :
        BlogPostCommandHandler<PublishBlogPostCommand>
    {
        public PublishBlogPostCommandHandler(
            IDomainRepository domainRepository)
            : base(domainRepository)
        {
        }

        public override void Handle(PublishBlogPostCommand command)
        {
            ActOnExisting(
                command.Id,
                blogPost => blogPost.Publish());
        }
    }
}