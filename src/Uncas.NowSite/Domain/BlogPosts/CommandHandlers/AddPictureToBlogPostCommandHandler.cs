using SimpleCqrs.Domain;
using Uncas.NowSite.Domain.BlogPosts.Commands;

namespace Uncas.NowSite.Domain.BlogPosts.CommandHandlers
{
    public class AddPictureToBlogPostCommandHandler :
        BlogPostCommandHandler<AddPictureToBlogPostCommand>
    {
        public AddPictureToBlogPostCommandHandler(
            IDomainRepository domainRepository) :
            base(domainRepository)
        {
        }

        public override void Handle(
            AddPictureToBlogPostCommand command)
        {
            ActOnExisting(
                command.BlogPostId,
                blogPost => blogPost.AddPicture(command.PictureId));
        }
    }
}