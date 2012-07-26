using System;
using SimpleCqrs.Commanding;

namespace Uncas.NowSite.Domain.BlogPosts.Commands
{
    public class StartEditBlogPostCommand : ICommand
    {
        public StartEditBlogPostCommand(
            Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}