using GoldenHour.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoldenHour.Model
{
    public class LocationHandler
    {
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public double? Altitude { get; set; }
        public LocationHandler(double longitude, double latitude, double? altitude)
        {
            //TODO --> Handle this better - or check to make sure values are good

            Longitude = longitude;
            Latitude = latitude;
            Altitude = altitude;
        }
        
    }

    
}
