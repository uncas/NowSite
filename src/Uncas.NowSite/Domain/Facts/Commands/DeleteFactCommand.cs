using System;
using SimpleCqrs.Commanding;

namespace Uncas.NowSite.Domain.Facts.Commands
{
    public class DeleteFactCommand : ICommand
    {
        public Guid Id { get; set; }
    }
}