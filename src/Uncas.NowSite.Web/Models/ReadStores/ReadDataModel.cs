﻿using System;

namespace Uncas.NowSite.Web.Models.ReadStores
{
    public class ReadDataModel
    {
        public Guid Id { get; set; }
        public string Model { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
    }
}