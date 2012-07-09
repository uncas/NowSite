using SimpleCqrs.Commanding;
using SimpleCqrs.Utilites;
using Uncas.NowSite.Web.Models.Commands;
using Uncas.NowSite.Web.Models.ReadStores;

namespace Uncas.NowSite.Web.Models.CommandHandlers
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