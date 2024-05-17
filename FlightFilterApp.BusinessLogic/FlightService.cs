
using System;
using System.Collections.Generic;
using FlightFilterApp.DataAccess;
using FlightFilterApp.FlightFilterApp.Models;

namespace FlightFilterApp.BusinessLogic
{
    public class FlightService
    {

        private readonly IFlightRepositories _flightRepositories;

        public FlightService(FlightContext context)
        {
            _flightRepositories = new FlightRepositories(context);
        }

        public List<ResultFlight> GetFilteredFlights(DateTime startDate, DateTime endDate, int agencyId)
        {
            return _flightRepositories.GetFilteredFlights(startDate, endDate, agencyId);
        }

    }
}
