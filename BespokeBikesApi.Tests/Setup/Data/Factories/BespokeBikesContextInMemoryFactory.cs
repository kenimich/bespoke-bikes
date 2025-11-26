using BespokeBikesApi.Data;
using BespokeBikesApi.Data.Factories;
using Microsoft.EntityFrameworkCore;

namespace BespokeBikesApi.Tests.Setup.Data.Factories {

    public class BespokeBikesContextInMemoryFactory : IBespokeBikesContextFactory
    {
        private readonly string _databaseName;

        public BespokeBikesContextInMemoryFactory()
        {
            _databaseName = "InMemorySalesTestDatabase";
        }

        public BespokeBikesContextInMemoryFactory(string databaseName)
        {
            _databaseName = databaseName;
        }

        public BespokeBikesContext CreateDbContext()
        {
            var options = new DbContextOptionsBuilder<BespokeBikesContext>()
                .UseInMemoryDatabase(databaseName: _databaseName)
                .EnableSensitiveDataLogging()
                .Options;

            return new BespokeBikesContext(options);
        }
        
        public BespokeBikesContext CreateDbContext(string[] args)
        {
            if(args != null && args.Length > 0)
            {
                //the expected argument is the database name
                var options = new DbContextOptionsBuilder<BespokeBikesContext>()
                    .UseInMemoryDatabase(databaseName: args[0])
                    .Options;

                return new BespokeBikesContext(options);
            }

            return CreateDbContext();
        }
    }
}
        