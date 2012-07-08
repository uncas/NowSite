using System;
using SimpleCqrs.Commanding;

namespace Uncas.NowSite.Web.Controllers
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