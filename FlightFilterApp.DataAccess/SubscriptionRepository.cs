using System.Collections.Generic;
using FlightFilterApp.DataAccess;
using FlightFilterApp.Models;

namespace FlightFilterApp.FlightFilterApp.DataAccess
{
    public interface ISubscriptionRepository
    {
        void Add(IEnumerable<Subscription> routes);
    }

    public class SubscriptionRepository : ISubscriptionRepository
    {
        private readonly FlightContext _context;

        public SubscriptionRepository(FlightContext context)
        {
            _context = context;
        }

        public void Add(IEnumerable<Subscription> routes)
        {
            _context.Subscription.AddRange(routes);
        }
    }
}
