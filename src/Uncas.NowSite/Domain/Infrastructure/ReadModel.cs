using System;

namespace Uncas.NowSite.Domain.Infrastructure
{
    public abstract class ReadModel
    {
        public Guid Id { get; set; }
        public abstract string ModelName { get; }
    }
}