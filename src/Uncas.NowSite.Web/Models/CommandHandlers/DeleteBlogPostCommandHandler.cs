using SimpleCqrs.Domain;
using Uncas.NowSite.Web.Models.Aggregates;
using Uncas.NowSite.Web.Models.Commands;

namespace Uncas.NowSite.Web.Models.CommandHandlers
{
    public class DeleteBlogPostCommandHandler :
        BlogPostCommandHandler<DeleteBlogPostCommand>
    {
        public DeleteBlogPostCommandHandler(
            IDomainRepository domainRepository) :
            base(domainRepository)
        {
        }

        public override void Handle(DeleteBlogPostCommand command)
        {
            var blogPost = _domainRepository.GetById<BlogPost>(command.Id);
            blogPost.Delete();
            _domainRepository.Save(blogPost);
        }
    }
}