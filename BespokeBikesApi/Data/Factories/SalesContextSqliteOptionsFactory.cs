using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using BespokeBikesApi.Data;
using Microsoft.EntityFrameworkCore.Sqlite;


namespace BespokeBikesApi.Data.Factories
{
    public class SalesContextSqliteOptionsFactory : ISalesContextOptionsFactory
    {
        private readonly IConfiguration _configuration;

        public SalesContextSqliteOptionsFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public DbContextOptions<SalesContext> CreateDbContextOptions() {
            var optionsBuilder = new DbContextOptionsBuilder<SalesContext>();
            optionsBuilder.UseSqlite(_configuration.GetConnectionString("SqliteConnection"));
            return optionsBuilder.Options;
        }
    }
}