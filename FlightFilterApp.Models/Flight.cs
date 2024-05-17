using System;

namespace FlightFilterApp.Models
{
    public class Flight
    {
        public int FlightId { get; set; }
        public int RouteId { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }
        public int AirlineId { get; set; }
        public string Status { get; set; }
        public Route Route { get; set; }
        public Airline Airline { get; set; }
    }

}
