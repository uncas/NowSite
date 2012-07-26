using System;
using SimpleCqrs.Commanding;

namespace Uncas.NowSite.Domain.BlogPosts.Commands
{
    public class AddPictureToBlogPostCommand : ICommand
    {
        public Guid BlogPostId { get; set; }
        public Guid PictureId { get; set; }
    }
}