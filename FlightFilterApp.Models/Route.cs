using System;

namespace FlightFilterApp.Models
{
    public class Route
    {
        public int RouteId { get; set; }
        public int OriginCityId { get; set; }
        public int DestinationCityId { get; set; }
        public DateTime Departure_date { get; set; }
    }

}
