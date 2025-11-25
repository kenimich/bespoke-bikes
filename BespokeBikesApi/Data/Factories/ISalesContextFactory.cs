using Microsoft.EntityFrameworkCore.Design;

namespace BespokeBikesApi.Data.Factories {
    
    public interface ISalesContextFactory : IDesignTimeDbContextFactory<SalesContext> {

        public SalesContext CreateDbContext();

        public SalesContext CreateDbContext(string[] args);

    }
}