using SimpleCqrs.Domain;
using Uncas.NowSite.Domain.BlogPosts.Commands;

namespace Uncas.NowSite.Domain.BlogPosts.CommandHandlers
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