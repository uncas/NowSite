using System;
using SimpleCqrs.Domain;
using Uncas.NowSite.Web.Models.Events;

namespace Uncas.NowSite.Web.Models.CommandHandlers
{
    public class Picture : AggregateRoot
    {
        private string _pictureUrl;

        public Picture(Guid id, string pictureUrl)
        {
            Id = id;
            _pictureUrl = pictureUrl;
            Apply(new PictureCreatedEvent
            {
                AggregateRootId = id,
                PictureUrl = pictureUrl
            });
        }
    }
}