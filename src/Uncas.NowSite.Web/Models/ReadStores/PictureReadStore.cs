﻿using Uncas.NowSite.Web.Models.Infrastructure;
using Uncas.NowSite.Web.Utilities;

namespace Uncas.NowSite.Web.Models.ReadStores
{
    public class PictureReadStore : ReadStore, IPictureReadStore
    {
        public PictureReadStore(
            string path,
            IStringSerializer stringSerializer)
            : base(path, stringSerializer)
        {
        }
    }
}