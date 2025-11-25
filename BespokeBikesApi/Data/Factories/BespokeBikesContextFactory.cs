using System;
using System.IO;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BespokeBikesApi.Data.Factories {
    
    public class BespokeBikesContextFactory : IBespokeBikesContextFactory {
        private readonly IConfiguration _configuration;

        public BespokeBikesContextFactory() {

            var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false)
                .AddJsonFile($"appsettings.{environmentName}.json", optional: true)
                .AddEnvironmentVariables();

            if (environmentName == "Development")
            {
                builder.AddUserSecrets<BespokeBikesContext>();
            }

            _configuration = builder.Build();
        }
        
        public BespokeBikesContextFactory(IConfiguration configuration) {
            _configuration = configuration;
        }

        public BespokeBikesContext CreateDbContext() {
            var databaseType = _configuration.GetValue<string>("DatabaseType");
            IBespokeBikesContextOptionsFactory optionsFactory;

            if (databaseType == "Sqlite") {
                optionsFactory = new BespokeBikesContextSqliteOptionsFactory(_configuration);
            } else if (databaseType == "PostgreSQL") {
                optionsFactory = new BespokeBikesContextPostgresOptionsFactory(_configuration);
            } else {
                throw new InvalidOperationException("Unsupported database type");
            }

            var options = optionsFactory.CreateDbContextOptions();
            return new BespokeBikesContext(options);
        }

        public BespokeBikesContext CreateDbContext(string[] args) {
            return CreateDbContext();
        }

    }
}