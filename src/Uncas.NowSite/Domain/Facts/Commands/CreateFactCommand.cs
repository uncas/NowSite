using System;
using SimpleCqrs.Commanding;

namespace Uncas.NowSite.Domain.Facts.Commands
{
    public class CreateFactCommand : ICommand
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}