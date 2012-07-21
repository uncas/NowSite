using System;
using SimpleCqrs.Eventing;

namespace Uncas.NowSite.Web.Models.Events
{
    public class PictureAddedToBlogPostEvent : DomainEvent
    {
        public Guid PictureId { get; set; }
    }
}