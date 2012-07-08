using SimpleCqrs.Domain;
using Uncas.NowSite.Web.Models.Aggregates;
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
            var blogPost = _domainRepository.GetById<BlogPost>(command.Id);
            blogPost.AddInfo(command.Title, command.Content);
            _domainRepository.Save(blogPost);
        }
    }
}