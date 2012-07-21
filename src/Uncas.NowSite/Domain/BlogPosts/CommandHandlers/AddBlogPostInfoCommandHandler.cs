using SimpleCqrs.Domain;
using Uncas.NowSite.Web.Models.Commands;

namespace Uncas.NowSite.Web.Models.CommandHandlers
{
    public class AddBlogPostInfoCommandHandler :
        BlogPostCommandHandler<AddBlogPostInfoCommand>
    {
        public AddBlogPostInfoCommandHandler(
            IDomainRepository domainRepository) :
            base(domainRepository)
        {
        }

        public override void Handle(AddBlogPostInfoCommand command)
        {
            ActOnExisting(
                command.Id,
                blogPost => blogPost.AddInfo(
                    command.Title,
                    command.Content));
        }
    }
}