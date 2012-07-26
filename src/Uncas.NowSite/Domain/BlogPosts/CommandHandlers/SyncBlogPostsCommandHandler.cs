using SimpleCqrs.Commanding;
using SimpleCqrs.Utilites;
using Uncas.NowSite.Domain.BlogPosts.Commands;
using Uncas.NowSite.Domain.BlogPosts.Denormalizers;

namespace Uncas.NowSite.Domain.BlogPosts.CommandHandlers
{
    /// <summary>
    /// Syncs blog posts by replaying events.
    /// </summary>
    public class SyncBlogPostsCommandHandler :
        CommandHandler<SyncBlogPostsCommand>
    {
        private readonly DomainEventReplayer _replayer;

        public SyncBlogPostsCommandHandler(
            DomainEventReplayer replayer)
        {
            _replayer = replayer;
        }

        public override void Handle(SyncBlogPostsCommand command)
        {
            _replayer.ReplayEventsForHandlerType(
                typeof(BlogPostDenormalizer));
        }
    }
}