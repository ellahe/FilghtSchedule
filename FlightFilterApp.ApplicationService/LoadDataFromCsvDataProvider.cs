using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CsvHelper;
using CsvHelper.Configuration;
using FlightFilterApp.DataAccess;
using FlightFilterApp.FlightFilterApp.ApplicationService.DTOS;
using FlightFilterApp.FlightFilterApp.DataAccess;
using FlightFilterApp.Models;

namespace FlightFilterApp.FlightFilterApp.BusinessLogic
{
    public interface ILoadDataFromCsvDataProvider
    {
        void AddData();
    }

    public class LoadDataFromCsvDataProvider : ILoadDataFromCsvDataProvider
    {
        private readonly IFlightRepositories _flightRepositories;
        private readonly IRouteRepository _routeRepository;
        private readonly ISubscriptionRepository _subscriptionRepository;
        private readonly FlightContext _context;

        public LoadDataFromCsvDataProvider(
            FlightRepositories flightRepositories,
            RouteRepository routeRepository,
            SubscriptionRepository subscriptionRepository,
            FlightContext context)
        {
            _flightRepositories = flightRepositories;
            _routeRepository = routeRepository;
            _subscriptionRepository = subscriptionRepository;
            _context = context;
        }

        public void AddData()
        {
            using (var reader = new StreamReader("routes.csv"))
            using (var csv = new CsvReader(reader, new CsvConfiguration() { HasHeaderRecord = true }))
            {
                var routes = csv.GetRecords<RouteDTO>().ToList();
                var data = routes.Select(x => ToPocoRoute(x)).DistinctBy(x =>
                 new { x.OriginCityId, x.DestinationCityId, x.Departure_date });
                _routeRepository.Add(data);
                _context.SaveChanges();
            }

            using (var reader = new StreamReader("flights.csv"))
            using (var csv = new CsvReader(reader, new CsvConfiguration() { HasHeaderRecord = true }))
            {
                var flights = csv.GetRecords<FlightDTO>().ToList();
                var flightpocos = flights.Select(x => ToPocoFlight(x));

                _flightRepositories.Add(flightpocos);
                _context.SaveChanges();
            }

            using (var reader = new StreamReader("subscriptions.csv"))
            using (var csv = new CsvReader(reader, new CsvConfiguration() { HasHeaderRecord = true }))
            {
                var subscriptions = csv.GetRecords<SubscriptionDTO>().ToList();
                _subscriptionRepository.Add(subscriptions.Select(x => ToPocoSubscription(x)));
                _context.SaveChanges();
            }

            _context.SaveChanges();
        }

        private Route ToPocoRoute(RouteDTO routeCsv)
        {
            return new Route()
            {
                RouteId = routeCsv.route_id,
                DestinationCityId = routeCsv.destination_city_id,
                OriginCityId = routeCsv.origin_city_id,
                Departure_date = routeCsv.departure_date
            };
        }

        private Subscription ToPocoSubscription(SubscriptionDTO subscriptionDTO)
        {
            return new Subscription()
            {
                AgencyId = subscriptionDTO.agency_id,
                OriginCityId = subscriptionDTO.origin_city_id,
                DestinationCityId = subscriptionDTO.destination_city_id,
            };
        }

        private Flight ToPocoFlight(FlightDTO flightDTO)
        {
            return new Flight()
            {
                FlightId = flightDTO.flight_id,
                RouteId = flightDTO.route_id,
                DepartureTime = flightDTO.departure_time,
                ArrivalTime = flightDTO.departure_time,
                AirlineId = flightDTO.airline_id,
            };
        }

    }

}
