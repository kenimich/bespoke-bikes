using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using BespokeBikesApi.Data;
using Npgsql.EntityFrameworkCore.PostgreSQL;


namespace BespokeBikesApi.Data.Factories
{
    public class SalesContextPostgresOptionsFactory : ISalesContextOptionsFactory
    {
        private readonly IConfiguration _configuration;

        public SalesContextPostgresOptionsFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public DbContextOptions<SalesContext> CreateDbContextOptions() {
            var optionsBuilder = new DbContextOptionsBuilder<SalesContext>();
            optionsBuilder.UseNpgsql(_configuration.GetConnectionString("PostgreSqlConnection"));
            return optionsBuilder.Options;
        }
    }
}