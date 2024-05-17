using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using FlightFilterApp.FlightFilterApp.Models;
using FlightFilterApp.Models;

namespace FlightFilterApp.DataAccess
{
    public interface IFlightRepositories
    {
        void Add(IEnumerable<Flight> flights);
        List<ResultFlight> GetFilteredFlights(DateTime startDate, DateTime endDate, int agencyId);
    }

    public class FlightRepositories : IFlightRepositories
    {
        private readonly FlightContext _context;

        public FlightRepositories(FlightContext context)
        {
            _context = context;
        }

        public void Add(IEnumerable<Flight> flights)
        {
            _context.Flight.AddRange(flights);
        }

        public List<ResultFlight> GetFilteredFlights(DateTime startDate, DateTime endDate, int agencyId)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            var tolerance = TimeSpan.FromMinutes(30); // Define tolerance for time comparison
            var daysInTicks = TimeSpan.FromDays(7).Ticks; // Calculate the duration of 7 days in ticks

            var newFlights = from flight in _context.Flight
                             //Join Route with flight for getting destination and Origin Data
                             join route in _context.Route on flight.RouteId equals route.RouteId
                              //جوین برای پراپرتی های مبدا و مقصد
                             
                             join existingFlight in _context.Flight
                             //برای بازیابی پروازهایی که در زمان خاصی و برای مسیر خاصی وجوددارند بصورت گروه بندی شده
                             on new { flight.AirlineId, DepartureTime = flight.DepartureTime.AddTicks(- daysInTicks) }
                             equals new { existingFlight.AirlineId, DepartureTime = existingFlight.DepartureTime } into gj

                             //برای هندل کردن لفت جوین امپتی برمیگردونیم
                             // در این بخض پروازهایی را میخایم که جدید هستن
                             //خالی بودن ساب فلایت به معتی نبودن پرواز در زمان و خط مورد نظر است 

                             from subflight in gj.DefaultIfEmpty()
                             where subflight == null
                             select new ResultFlight
                             {
                                 flightId = flight.FlightId,
                                 OriginCityId = route.OriginCityId,
                                 DestinationCityId = route.DestinationCityId,
                                 DepartureTime = flight.DepartureTime,
                                 ArrivalTime = flight.ArrivalTime,
                                 AirlineId = flight.AirlineId,
                                 Status = "New"
                             };

            var discontinuedFlights = from flight in _context.Flight
                                      join route in _context.Route on flight.RouteId equals route.RouteId
                                      join existingFlight in _context.Flight
                                      on new { flight.AirlineId, DepartureTime = flight.DepartureTime.AddTicks(daysInTicks) }
                                      equals new { existingFlight.AirlineId, DepartureTime = existingFlight.DepartureTime } into gj
                                      from subflight in gj.DefaultIfEmpty()
                                      where subflight == null
                                      select new ResultFlight
                                      {
                                          flightId = flight.FlightId,
                                          OriginCityId = route.OriginCityId,
                                          DestinationCityId = route.DestinationCityId,
                                          DepartureTime = flight.DepartureTime,
                                          ArrivalTime = flight.ArrivalTime,
                                          AirlineId = flight.AirlineId,
                                          Status = "Discontinued"
                                      };

            var flightsWithStatus = newFlights.Concat(discontinuedFlights).ToList();

            stopwatch.Stop();
            TimeSpan timeTaken = stopwatch.Elapsed;

            Console.WriteLine($"Time taken to execute the change detection algorithm: {timeTaken.TotalMilliseconds} ms");

            return flightsWithStatus;
           
        }
    }
}
