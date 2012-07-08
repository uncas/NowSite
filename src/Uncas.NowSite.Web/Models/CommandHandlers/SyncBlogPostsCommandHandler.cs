using SimpleCqrs.Domain;
using Uncas.NowSite.Web.Models.Aggregates;
using Uncas.NowSite.Web.Models.Commands;
using Uncas.NowSite.Web.Models.ReadStores;

namespace Uncas.NowSite.Web.Models.CommandHandlers
{
    /// <summary>
    /// Syncs blog posts.
    /// </summary>
    public class SyncBlogPostsCommandHandler :
        BlogPostCommandHandler<SyncBlogPostsCommand>
    {
        private readonly IBlogPostMasterStore _masterStore;
        private readonly IBlogPostReadStore _readStore;
        private readonly IDeletedBlogPostStore _deletedStore;

        public SyncBlogPostsCommandHandler(
            IDomainRepository domainRepository,
            IBlogPostMasterStore masterStore,
            IBlogPostReadStore readStore,
            IDeletedBlogPostStore deletedStore) :
            base(domainRepository)
        {
            _masterStore = masterStore;
            _readStore = readStore;
            _deletedStore = deletedStore;
        }

        public override void Handle(SyncBlogPostsCommand command)
        {
            foreach (var item in _masterStore.GetAll())
            {
                var blogPost =
                    _domainRepository.GetById<BlogPost>(item.Id);
                SyncBlogPost(blogPost);
            }
        }

        private void SyncBlogPost(BlogPost blogPost)
        {
            switch (blogPost.State)
            {
                case BlogPostState.Published:
                    _readStore.Add(new BlogPostReadModel
                    {
                        Id = blogPost.Id,
                        Title = blogPost.Title,
                        Content = blogPost.Content
                    });
                    break;
                case BlogPostState.Deleted:
                    _readStore.Delete(blogPost.Id);
                    _deletedStore.Add(new DeletedBlogPostModel
                    {
                        Id = blogPost.Id,
                        Title = blogPost.Title,
                        Content = blogPost.Content
                    });
                    break;
                case BlogPostState.Created:
                default:
                    break;
            }
        }
    }
}