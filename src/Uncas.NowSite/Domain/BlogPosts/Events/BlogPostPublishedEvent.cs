using System;
using System.Collections.Generic;
using SimpleCqrs.Eventing;

namespace Uncas.NowSite.Web.Models.Events
{
    public class BlogPostPublishedEvent : DomainEvent
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public IEnumerable<Guid> Pictures { get; set; }
    }
}