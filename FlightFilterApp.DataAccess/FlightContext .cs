using System.Data.SqlClient;
using FlightFilterApp.Models;
using Microsoft.EntityFrameworkCore;

namespace FlightFilterApp.DataAccess
{
    public class FlightContext : DbContext
    {
        public DbSet<Flight> Flight { get; set; }
        public DbSet<Route> Route { get; set; }
        public DbSet<Subscription> Subscription { get; set; }

        public FlightContext(DbContextOptions<FlightContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString =
            "server=.;database=FlightSchedule;Integrated Security=false;user Id=sa;Password=123456;";
            conn.Open();
            optionsBuilder.UseSqlServer(conn);
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Flight>().HasKey(f => f.FlightId);
            modelBuilder.Entity<Subscription>().HasKey(s => new { s.ID });
            modelBuilder.Entity<Route>().HasKey(s => new { s.RouteId });
        }
    }

}



