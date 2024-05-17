using System;

namespace FlightFilterApp.FlightFilterApp.ApplicationService.DTOS
{
    public class RouteDTO
    {
        public int route_id { get; set; }
        public int origin_city_id { get; set; }
        public int destination_city_id { get; set; }
        public DateTime departure_date { get; set; }
    }

}
