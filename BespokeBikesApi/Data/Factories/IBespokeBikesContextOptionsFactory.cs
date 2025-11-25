namespace BespokeBikesApi.Data.Factories
{
    using Microsoft.EntityFrameworkCore;

    public interface IBespokeBikesContextOptionsFactory
    {
        public DbContextOptions<BespokeBikesContext> CreateDbContextOptions();
    }
}