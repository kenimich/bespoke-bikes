using BespokeBikesApi.Data;
using BespokeBikesApi.Data.Models;
using BespokeBikesApi.Data.Factories;
using BespokeBikesApi.Logic.DTO;

namespace BespokeBikesApi.Logic.Reports
{
    public class QuarterlyReportService
    {
        private IBespokeBikesContextFactory _contextFactory;

        public QuarterlyReportService(IBespokeBikesContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public QuarterlyReport GetQuarterlyReport(int salespersonId, int year, int quarter)
        {

            if (quarter < 1 || quarter > 4)
            {
                throw new ArgumentException("Quarter must be between 1 and 4.");
            }
            
            using var context = _contextFactory.CreateDbContext();

            var salesperson = context.Salespersons.Find(salespersonId);
            if (salesperson == null)
            {
                throw new ArgumentException($"Salesperson with ID {salespersonId} not found.");
            }

            var startDate = new DateTime(year, (quarter - 1) * 3 + 1, 1);
            var endDate = startDate.AddMonths(3).AddDays(-1);

            var sales = context.Sales
                .Where(s => s.SalespersonId == salespersonId && s.SaleDate >= startDate && s.SaleDate <= endDate)
                .ToList();

            var totalCommission = sales.Any() ? sales.Sum(s => s.Commission) : 0m;

            return new QuarterlyReport
            {
                Salesperson = salesperson,
                Year = year,
                Quarter = quarter,
                TotalCommission = totalCommission
            };
        }
    }
}