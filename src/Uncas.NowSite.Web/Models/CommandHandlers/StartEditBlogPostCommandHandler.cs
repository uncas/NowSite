using SimpleCqrs.Domain;
using Uncas.NowSite.Web.Controllers;
using Uncas.NowSite.Web.Models.Aggregates;

namespace Uncas.NowSite.Web.Models.CommandHandlers
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
            var blogPost = _domainRepository.GetById<BlogPost>(command.Id);
            blogPost.StartEdit();
            _domainRepository.Save(blogPost);
        }
    }
}