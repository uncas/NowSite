using SimpleCqrs.Domain;
using Uncas.NowSite.Web.Models.Commands;

namespace Uncas.NowSite.Web.Models.CommandHandlers
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