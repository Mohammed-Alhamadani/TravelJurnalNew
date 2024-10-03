using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TravelJurnal.Models;

namespace TravelJurnal.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {

        // This map to the Traveller Profile Class.
        public DbSet<TravellerProfile> TravellerProfile { get; set; }

        // This Map to the Trip Class.
        public DbSet<Trip>  Trip { get; set; }

        //This map to Destination Class.
        public DbSet<Destination> Destination { get; set; }

        // This map to Entry Class.
        public DbSet<Entry> Entry { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
