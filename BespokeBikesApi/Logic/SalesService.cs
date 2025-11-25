using BespokeBikesApi.Data;
using BespokeBikesApi.Data.Models;
using BespokeBikesApi.Data.Factories;

namespace BespokeBikesApi.Logic {
    
    public class SaleService
    {
        private IBespokeBikesContextFactory _contextFactory;

        public SaleService(IBespokeBikesContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public int AddSale(Sale sale)
        {
            using var context = _contextFactory.CreateDbContext();
            context.Sales.Add(sale);
            context.SaveChanges();
            return sale.Id;
        }

        public Sale GetSaleById(int id)
        {
            using var context = _contextFactory.CreateDbContext();
            return context.Sales.Find(id);
        }
    }
}