using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace BotData.Data.Context
{
    public class ProjectStoreContextFactory : IDesignTimeDbContextFactory<BotDataContext>
    {
        public BotDataContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<BotDataContext>();

            var dbCreds = DbCreds.GetCreds();

            optionsBuilder.UseNpgsql(dbCreds.ConnectionString);

            return new BotDataContext(optionsBuilder.Options);
        }
    }
}