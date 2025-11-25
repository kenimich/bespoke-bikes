using Microsoft.EntityFrameworkCore.Design;

namespace BespokeBikesApi.Data.Factories {
    
    public interface IBespokeBikesContextFactory : IDesignTimeDbContextFactory<BespokeBikesContext> {

        public BespokeBikesContext CreateDbContext();

        public BespokeBikesContext CreateDbContext(string[] args);

    }
}