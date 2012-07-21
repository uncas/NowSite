using System;
using SimpleCqrs.Commanding;

namespace Uncas.NowSite.Web.Models.Commands
{
    public class AddBlogPostInfoCommand : ICommand
    {
        public AddBlogPostInfoCommand(
            Guid id,
            string title,
            string content)
        {
            Id = id;
            Title = title;
            Content = content;

        }

        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
    }
}
