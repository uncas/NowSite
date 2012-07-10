using SimpleCqrs.Domain;
using Uncas.NowSite.Web.Models.Aggregates;
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
            var blogPost = _domainRepository.GetById<BlogPost>(
                command.BlogPostId);
            blogPost.AddPicture(command.PictureId);
            _domainRepository.Save(blogPost);
        }
    }
}