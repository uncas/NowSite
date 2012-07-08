using System;
using SimpleCqrs.Commanding;

namespace Uncas.NowSite.Web.Models.Commands
{
    public class CreateBlogPostCommand : ICommand
    {
        public CreateBlogPostCommand(Guid id)
        {
            this.Id = id;
        }

        public Guid Id { get; set; }
    }
}
