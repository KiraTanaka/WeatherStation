﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Meteo.Models
{
    public class MeteoStationCardContext : DbContext
    {
        public DbSet<MeteoStationCard> MeteoStationCards { get; set; }
    }
}