using System;
using SimpleCqrs.Commanding;

namespace Uncas.NowSite.Domain.BlogPosts.Commands
{
    public class DeleteBlogPostCommand : ICommand
    {
        public DeleteBlogPostCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}