using SimpleCqrs.Domain;
using Uncas.NowSite.Web.Models.Commands;

namespace Uncas.NowSite.Web.Models.CommandHandlers
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