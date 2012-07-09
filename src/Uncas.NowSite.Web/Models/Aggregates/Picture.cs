using System;
using SimpleCqrs.Domain;
using Uncas.NowSite.Web.Models.Events;

namespace Uncas.NowSite.Web.Models.CommandHandlers
{
    public class Picture : AggregateRoot
    {
        private Guid _id;
        private string _pictureUrl;

        public Picture(Guid id, string pictureUrl)
        {
            _id = id;
            _pictureUrl = pictureUrl;
            Apply(new PictureCreatedEvent
            {
                AggregateRootId = id,
                PictureUrl = pictureUrl
            });
        }
    }
}