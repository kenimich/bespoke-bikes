using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using BespokeBikesApi.Data;
using Microsoft.EntityFrameworkCore.Sqlite;


namespace BespokeBikesApi.Data.Factories
{
    public class BespokeBikesContextSqliteOptionsFactory : IBespokeBikesContextOptionsFactory
    {
        private readonly IConfiguration _configuration;

        public BespokeBikesContextSqliteOptionsFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public DbContextOptions<BespokeBikesContext> CreateDbContextOptions() {
            var optionsBuilder = new DbContextOptionsBuilder<BespokeBikesContext>();
            optionsBuilder.UseSqlite(_configuration.GetConnectionString("SqliteConnection"));
            return optionsBuilder.Options;
        }
    }
}