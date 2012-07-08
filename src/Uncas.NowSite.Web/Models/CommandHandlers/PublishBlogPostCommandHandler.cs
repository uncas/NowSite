using SimpleCqrs.Domain;
using Uncas.NowSite.Web.Models.Aggregates;
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
            var blogPost = _domainRepository.GetById<BlogPost>(command.Id);
            blogPost.Publish();
            _domainRepository.Save(blogPost);
        }
    }
}