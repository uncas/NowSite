using System;

namespace Uncas.NowSite.Web.Models.Infrastructure
{
    public abstract class ReadModel
    {
        public Guid Id { get; set; }
        public abstract string ModelName { get; }
    }
}