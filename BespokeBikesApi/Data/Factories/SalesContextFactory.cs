using System;
using System.IO;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BespokeBikesApi.Data.Factories {
    
    public class SalesContextFactory : ISalesContextFactory {
        private readonly IConfiguration _configuration;

        public SalesContextFactory() {

            var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false)
                .AddJsonFile($"appsettings.{environmentName}.json", optional: true)
                .AddEnvironmentVariables();

            if (environmentName == "Development")
            {
                builder.AddUserSecrets<SalesContext>();
            }

            _configuration = builder.Build();
        }
        
        public SalesContextFactory(IConfiguration configuration) {
            _configuration = configuration;
        }

        public SalesContext CreateDbContext() {
            var databaseType = _configuration.GetValue<string>("DatabaseType");
            ISalesContextOptionsFactory optionsFactory;

            if (databaseType == "Sqlite") {
                optionsFactory = new SalesContextSqliteOptionsFactory(_configuration);
            } else if (databaseType == "PostgreSQL") {
                optionsFactory = new SalesContextPostgresOptionsFactory(_configuration);
            } else {
                throw new InvalidOperationException("Unsupported database type");
            }

            var options = optionsFactory.CreateDbContextOptions();
            return new SalesContext(options);
        }

        public SalesContext CreateDbContext(string[] args) {
            return CreateDbContext();
        }

    }
}