using System;
using SimpleCqrs.Commanding;

namespace Uncas.NowSite.Domain.BlogPosts.Commands
{
    public class PublishBlogPostCommand : ICommand
    {
        public PublishBlogPostCommand(Guid id)
        {
            this.Id = id;
        }

        public Guid Id { get; set; }
    }
}
