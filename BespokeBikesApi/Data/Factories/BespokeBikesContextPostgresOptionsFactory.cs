using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using BespokeBikesApi.Data;
using Npgsql.EntityFrameworkCore.PostgreSQL;


namespace BespokeBikesApi.Data.Factories
{
    public class BespokeBikesContextPostgresOptionsFactory : IBespokeBikesContextOptionsFactory
    {
        private readonly IConfiguration _configuration;

        public BespokeBikesContextPostgresOptionsFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public DbContextOptions<BespokeBikesContext> CreateDbContextOptions() {
            var optionsBuilder = new DbContextOptionsBuilder<BespokeBikesContext>();
            optionsBuilder.UseNpgsql(_configuration.GetConnectionString("PostgreSqlConnection"));
            return optionsBuilder.Options;
        }
    }
}