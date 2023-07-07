﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoldenHour.Services.Interfaces
{
    public interface ILocationService
    {
        Task<LocationHandler> GetLocationAsync();
        Task<LocationHandler> GetCachedLocationAsync();
    }
}
