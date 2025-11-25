using BespokeBikesApi.Data;
using BespokeBikesApi.Data.Factories;
using Microsoft.EntityFrameworkCore;

namespace BespokeBikesApi.Tests.Setup.Data.Factories {

    public class SalesContextInMemoryFactory : ISalesContextFactory
    {
        private readonly string _databaseName;

        public SalesContextInMemoryFactory()
        {
            _databaseName = "InMemorySalesTestDatabase";
        }

        public SalesContextInMemoryFactory(string databaseName)
        {
            _databaseName = databaseName;
        }

        public SalesContext CreateDbContext()
        {
            var options = new DbContextOptionsBuilder<SalesContext>()
                .UseInMemoryDatabase(databaseName: _databaseName)
                .Options;

            return new SalesContext(options);
        }
        
        public SalesContext CreateDbContext(string[] args)
        {
            if(args != null && args.Length > 0)
            {
                //the expected argument is the database name
                var options = new DbContextOptionsBuilder<SalesContext>()
                    .UseInMemoryDatabase(databaseName: args[0])
                    .Options;

                return new SalesContext(options);
            }

            return CreateDbContext();
        }
    }
}
        