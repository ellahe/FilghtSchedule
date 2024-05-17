using System.Collections.Generic;
using FlightFilterApp.DataAccess;
using FlightFilterApp.Models;

namespace FlightFilterApp.FlightFilterApp.DataAccess
{
    public interface IRouteRepository
    {
        void Add(IEnumerable<Route> routes);
    }

    public class RouteRepository : IRouteRepository
    {
        private readonly FlightContext _context;

        public RouteRepository(FlightContext context)
        {
            _context = context;
        }

        public void Add(IEnumerable<Route> routes)
        {
            _context.Route.AddRange(routes);
        }
    }
}
