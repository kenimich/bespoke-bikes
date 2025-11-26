using BespokeBikesApi.Data.Models;
using BespokeBikesApi.Tests.Setup.Data.Factories;
using BespokeBikesApi.Logic.Reports;
using BespokeBikesApi.Logic.DTO;

namespace BespokeBikesApi.Tests.Logic {

    public class QuarterlyReportServiceTests
    {
        private readonly BespokeBikesContextInMemoryFactory _contextFactory;
        private readonly QuarterlyReportService _quarterlyReportService;

        public QuarterlyReportServiceTests()
        {
            _contextFactory = new BespokeBikesContextInMemoryFactory("QuarterlyReportServiceTests_Database");
            _quarterlyReportService = new QuarterlyReportService(_contextFactory);
            SeedData();
        }

        private void SeedData()
        {
            using var context = _contextFactory.CreateDbContext();

            var sales_alice = new Salesperson { Name = "Alice", EmployeeId = "EMP001" };
            var sales_bob = new Salesperson { Name = "Bob", EmployeeId = "EMP002" };
            context.Salespersons.AddRange(sales_alice, sales_bob);
            context.SaveChanges();

            var bike_mtn = new Product { Name = "Mountain Bike", CommissionPercentage = 0.10m };
            var bike_road = new Product { Name = "Road Bike", CommissionPercentage = 0.15m };
            context.Products.AddRange(bike_mtn, bike_road);
            context.SaveChanges();

            var cust_charlie = new Customer { Name = "Charlie", Contact = "charliex@gmail.com", ContactType = ContactType.Email };
            var cust_dana = new Customer { Name = "Dana", Contact = "danab@gmail.com", ContactType = ContactType.Email };
            var cust_ellen = new Customer { Name = "Ellen", Contact = "770-555-1234", ContactType = ContactType.PhoneNumber };
            var cust_frank = new Customer { Name = "Frank", Contact = "770-555-5678", ContactType = ContactType.PhoneNumber };
            var cust_grace = new Customer { Name = "Grace", Contact = "grace.davie@yahoo.com", ContactType = ContactType.Email };
            context.Customers.AddRange(cust_charlie, cust_dana, cust_ellen, cust_frank, cust_grace);
            context.SaveChanges();

            context.Sales.AddRange(
                new Sale { CustomerId = cust_charlie.Id, SalespersonId = sales_alice.Id, ProductId = bike_mtn.Id, SaleDate = new DateTime(2025, 7, 15), SalePrice = 1000m, Commission = 100m },
                new Sale { CustomerId = cust_dana.Id, SalespersonId = sales_alice.Id, ProductId = bike_road.Id, SaleDate = new DateTime(2025, 10, 01), SalePrice = 1500m, Commission = 225m },
                new Sale { CustomerId = cust_ellen.Id, SalespersonId = sales_bob.Id, ProductId = bike_mtn.Id, SaleDate = new DateTime(2025, 11, 30), SalePrice = 1200m, Commission = 120m },
                new Sale { CustomerId = cust_frank.Id, SalespersonId = sales_bob.Id, ProductId = bike_road.Id, SaleDate = new DateTime(2025, 12, 25), SalePrice = 1600m, Commission = 240m },
                new Sale { CustomerId = cust_grace.Id, SalespersonId = sales_alice.Id, ProductId = bike_mtn.Id, SaleDate = new DateTime(2025, 9, 30), SalePrice = 1100m, Commission = 110m }
            );
            context.SaveChanges();
        }

        [Theory(DisplayName = "Get Quarterly Report for Salesperson Successfully")]
        [InlineData(1, 2025, 3, 210)] // Alice Q3
        [InlineData(1, 2025, 4, 225)] // Alice Q4
        [InlineData(2, 2025, 3, 0)] // Bob Q3
        [InlineData(2, 2025, 4, 360)] // Bob Q4
        public void GetQuarterlyReport(int salespersonId, int year, int quarter, decimal expectedCommission)
        {
            var report = _quarterlyReportService.GetQuarterlyReport(salespersonId, year, quarter);

            Assert.NotNull(report);
            Assert.Equal(salespersonId, report.Salesperson.Id);
            Assert.Equal(year, report.Year);
            Assert.Equal(quarter, report.Quarter);
            Assert.Equal(expectedCommission, report.TotalCommission);
        }

        [Theory(DisplayName = "Get Quarterly Report with Invalid Quarter Throws Exception")]
        [InlineData(1, 2025, -1)]
        [InlineData(2, 2025, 0)]
        [InlineData(1, 2025, 5)]
        public void GetQuarterlyReport_InvalidQuarter_ThrowsException(int salespersonId, int year, int quarter)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                _quarterlyReportService.GetQuarterlyReport(salespersonId, year, quarter);
            });
        }

        [Fact(DisplayName = "Get Quarterly Report for Non-Existent Salesperson Throws Exception")]
        public void GetQuarterlyReport_NonExistentSalesperson_ThrowsException()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                _quarterlyReportService.GetQuarterlyReport(9999, 2025, 1);
            });
        }
    }
}