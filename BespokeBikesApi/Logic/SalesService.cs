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

            if(!context.Products.Any(p => p.Id == sale.ProductId))
            {
                throw new ArgumentException("Invalid ProductId.", nameof(Sale.ProductId));
            }

            if(!context.Salespersons.Any(s => s.Id == sale.SalespersonId))
            {
                throw new ArgumentException("Invalid SalespersonId.", nameof(Sale.SalespersonId));
            }

            if(!context.Customers.Any(c => c.Id == sale.CustomerId))
            {
                throw new ArgumentException("Invalid CustomerId.", nameof(Sale.CustomerId));
            }

            var productCommissionRate = context.Products.Find(sale.ProductId)?.CommissionPercentage ?? 0m;
            sale.Commission = sale.SalePrice * productCommissionRate;

            context.Sales.Add(sale);
            context.SaveChanges();
            return sale.Id;
        }

        public Sale GetSaleById(int id)
        {
            using var context = _contextFactory.CreateDbContext();
            return context.Sales.Find(id);
        }

        public IEnumerable<Sale> GetSalesByDateRange(DateTime startDate, DateTime endDate)
        {
            if(startDate == DateTime.MinValue)
            {
                throw new ArgumentException("startDate is required.", nameof(startDate));
            }

            if(endDate == DateTime.MinValue)
            {
                throw new ArgumentException("endDate is required.", nameof(endDate));
            }

            if (startDate > endDate)
            {
                throw new ArgumentException("startDate must be less than endDate.", nameof(startDate));
            }

            using var context = _contextFactory.CreateDbContext();
            return context.Sales
                .Where(s => s.SaleDate >= startDate && s.SaleDate <= endDate)
                .ToList();
        }
    }
}