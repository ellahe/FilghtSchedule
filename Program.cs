using System;
using System.IO;
using FlightFilterApp.BusinessLogic;
using FlightFilterApp.DataAccess;
using FlightFilterApp.FlightFilterApp.BusinessLogic;
using FlightFilterApp.FlightFilterApp.DataAccess;
using Microsoft.Extensions.DependencyInjection;

namespace FlightFilterApp
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 3)
            {
                Console.WriteLine("Usage: YourProgram.exe <start_date> <end_date> <agency_id>");
                return;
            }

            DateTime startDate = DateTime.Parse(args[0]);
            DateTime endDate = DateTime.Parse(args[1]);
            int agencyId = int.Parse(args[2]);

            var serviceProvider = new ServiceCollection()
                .AddDbContext<FlightContext>()
                .AddScoped<FlightRepositories>()
                .AddScoped<RouteRepository>()
                .AddScoped<SubscriptionRepository>()
                .AddScoped<FlightService>()
                .AddScoped<LoadDataFromCsvDataProvider>()
                .BuildServiceProvider();

            var flightService = serviceProvider.GetService<FlightService>();

            //Load data from CSV files into the context
           var addDataDataProvider = serviceProvider.GetService<LoadDataFromCsvDataProvider>();
            addDataDataProvider.AddData();

            var filteredFlights = flightService.GetFilteredFlights(startDate, endDate, agencyId);

            using (var writer = new StreamWriter("results.csv"))
            {
                writer.WriteLine("flightId,origin_city_id,destination_city_id,departure_time,arrival_time,airline_id,status");
                foreach (var flight in filteredFlights)
                {
                    writer.WriteLine($"{flight.flightId},{flight.OriginCityId},{flight.DestinationCityId},{flight.DepartureTime},{flight.ArrivalTime},{flight.AirlineId},{flight.Status}");
                }
            }

            Console.WriteLine("Results saved to results.csv");
        }

      
    }
}
