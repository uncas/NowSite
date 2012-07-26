using System;
using SimpleCqrs.Domain;
using Uncas.NowSite.Domain.Pictures.Events;

namespace Uncas.NowSite.Domain.Pictures
{
    public class Picture : AggregateRoot
    {
        public Picture()
        {
        }

        public Picture(Guid id, string pictureUrl)
        {
            Id = id;
            PictureUrl = pictureUrl;
            Apply(new PictureCreatedEvent
            {
                AggregateRootId = id,
                PictureUrl = pictureUrl
            });
        }

        public string PictureUrl { get; private set; }
    }
}