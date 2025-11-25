using BespokeBikesApi.Data;
using BespokeBikesApi.Data.Models;
using BespokeBikesApi.Data.Factories;

namespace BespokeBikesApi.Logic
{
    public class SalespersonService
    {
        private readonly IBespokeBikesContextFactory _contextFactory;

        public SalespersonService(IBespokeBikesContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public int AddSalesperson(Salesperson salesperson)
        {
            using var context = _contextFactory.CreateDbContext();
            context.Salespersons.Add(salesperson);
            context.SaveChanges();
            return salesperson.Id;
        }

        public Salesperson GetSalespersonById(int id)
        {
            using var context = _contextFactory.CreateDbContext();
            return context.Salespersons.Find(id);
        }

        public bool UpdateSalesperson(Salesperson salesperson)
        {
            using var context = _contextFactory.CreateDbContext();
            context.Salespersons.Update(salesperson);
            context.SaveChanges();
            return true;
        }
    }
}