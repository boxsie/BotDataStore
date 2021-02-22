using BotData.Data.Entity.BotUser;
using BotData.Data.Entity.Game;
using BotData.Data.Entity.GeoSniff;
using Microsoft.EntityFrameworkCore;

namespace BotData.Data.Context
{
    public class BotDataContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<GuessGame> GuessGames { get; set; }
        public DbSet<Location> GeoLocations { get; set; }

        public BotDataContext(DbContextOptions<BotDataContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(x => x.DiscordId);
                entity.HasIndex(x => x.Name);
            });

            modelBuilder.Entity<GuessGame>(entity =>
            {
                entity.HasOne(x => x.User)
                    .WithMany(x => x.GuessGames)
                    .HasForeignKey(x => x.DiscordId);
            });

            modelBuilder.Entity<GuessGameAttempt>(entity =>
            {
                entity.HasOne(x => x.Game)
                    .WithMany(x => x.Attempts)
                    .HasForeignKey(x => x.GameId);
                entity.HasOne(x => x.User)
                    .WithMany(x => x.GuessGameAttempts)
                    .HasForeignKey(x => x.DiscordId);
            });

            modelBuilder.Entity<Location>()
                .HasIndex(x => x.Country);
        }
    }
}