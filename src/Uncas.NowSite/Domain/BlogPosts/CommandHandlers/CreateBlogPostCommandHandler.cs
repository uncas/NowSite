using SimpleCqrs.Domain;
using Uncas.NowSite.Domain.BlogPosts.Commands;

namespace Uncas.NowSite.Domain.BlogPosts.CommandHandlers
{
    public class CreateBlogPostCommandHandler :
        BlogPostCommandHandler<CreateBlogPostCommand>
    {
        public CreateBlogPostCommandHandler(
            IDomainRepository domainRepository) :
            base(domainRepository)
        {
        }

        public override void Handle(CreateBlogPostCommand command)
        {
            var blogPost = new BlogPost(command.Id);
            _domainRepository.Save(blogPost);
        }
    }
}