using BotData.Data.Entity.User;
using Microsoft.EntityFrameworkCore;

namespace BotData.Api
{
    public class BotDataContext : DbContext
    {
        public DbSet<User> Users { get; set; }

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
        }
    }
}