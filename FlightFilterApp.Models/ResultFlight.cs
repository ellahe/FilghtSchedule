using System;

namespace FlightFilterApp.FlightFilterApp.Models
{
    public class ResultFlight
    {
        public int flightId { get; internal set; }
        public int OriginCityId { get; internal set; }
        public int DestinationCityId { get; internal set; }
        public DateTime DepartureTime { get; internal set; }
        public int AirlineId { get; internal set; }
        public string Status { get; internal set; }
        public DateTime ArrivalTime { get; internal set; }
    }
}
