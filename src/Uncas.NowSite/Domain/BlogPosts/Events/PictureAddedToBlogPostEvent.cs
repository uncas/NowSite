using System;
using SimpleCqrs.Eventing;

namespace Uncas.NowSite.Domain.BlogPosts.Events
{
    public class PictureAddedToBlogPostEvent : DomainEvent
    {
        public Guid PictureId { get; set; }
    }
}