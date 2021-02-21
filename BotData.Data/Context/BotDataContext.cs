using BotData.Data.Entity.GeoSniff;
using BotData.Data.Entity.User;
using Microsoft.EntityFrameworkCore;

namespace BotData.Data.Context
{
    public class BotDataContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Location> GeoLocations { get; set; }

        public BotDataContext(DbContextOptions<BotDataContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // INDEXES
            modelBuilder.Entity<User>()
                .HasIndex(x => x.DiscordId);
            modelBuilder.Entity<User>()
                .HasIndex(x => x.Name);

            modelBuilder.Entity<Location>()
                .HasIndex(x => x.Country);
        }
    }
}