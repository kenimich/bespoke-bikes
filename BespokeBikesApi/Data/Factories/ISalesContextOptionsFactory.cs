namespace BespokeBikesApi.Data.Factories
{
    using Microsoft.EntityFrameworkCore;

    public interface ISalesContextOptionsFactory
    {
        public DbContextOptions<SalesContext> CreateDbContextOptions();
    }
}