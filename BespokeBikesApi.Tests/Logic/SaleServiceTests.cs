using BespokeBikesApi.Data.Models;
using BespokeBikesApi.Tests.Setup.Data.Factories;
using BespokeBikesApi.Logic;

namespace BespokeBikesApi.Tests.Logic {

    public class SaleServiceTests
    {
        private readonly BespokeBikesContextInMemoryFactory _contextFactory;
        private readonly SaleService _saleService;

        public SaleServiceTests()
        {
            _contextFactory = new BespokeBikesContextInMemoryFactory("SaleServiceTests_Database");
            _saleService = new SaleService(_contextFactory);
        }

        [Fact(DisplayName = "Create Sale Using Service")]
        public void CreateSale()
        {   
            using var context = _contextFactory.CreateDbContext();

            var product = new Product { Type = "Bicycle", Name = "Test Bike", CommissionPercentage = 0.1m };
            context.Products.Add(product);

            var salesperson = new Salesperson { Name = "John Doe", EmployeeId = "EMP123" };
            context.Salespersons.Add(salesperson);

            var customer = new Customer { Name = "Jane Smith", Contact = "jane.smith@gmail.com", ContactType = ContactType.Email };
            context.Customers.Add(customer);

            context.SaveChanges();


            var sale = new Sale
            {
                ProductId = product.Id,
                SalespersonId = salesperson.Id,
                CustomerId = customer.Id,
                SaleDate = DateTime.UtcNow,
                SalePrice = 100.00m,
                Commission = 10.00m
            };

            var saleId = _saleService.AddSale(sale);
            Assert.True(saleId > 0, "Sale ID should be greater than 0 after adding a sale.");
        }

        [Fact(DisplayName = "Create and Find Sale Using Service")]
        public void CreateAndFindSale()
        {
            using var context = _contextFactory.CreateDbContext();

            var product = new Product { Type = "Bicycle", Name = "Road Bike", CommissionPercentage = 0.15m };
            context.Products.Add(product);

            var salesperson = new Salesperson { Name = "Jane Doe", EmployeeId = "EMP456" };
            context.Salespersons.Add(salesperson);

            var customer = new Customer { Name = "Jill Smith", Contact = "jill.smith@gmail.com", ContactType = ContactType.Email };
            context.Customers.Add(customer);

            context.SaveChanges();

            var sale = new Sale
            {
                ProductId = product.Id,
                SalespersonId = salesperson.Id,
                CustomerId = customer.Id,
                SaleDate = DateTime.UtcNow,
                SalePrice = 150.00m,
                Commission = 15.00m
            };

            var saleId = _saleService.AddSale(sale);
            Assert.True(saleId > 0, "Sale ID should be greater than 0 after adding a sale.");

            var foundSale = _saleService.GetSaleById(saleId);
            Assert.NotNull(foundSale);
            Assert.Equal(sale.ProductId, foundSale.ProductId);
            Assert.Equal(sale.SalespersonId, foundSale.SalespersonId);
            Assert.Equal(sale.CustomerId, foundSale.CustomerId);
            Assert.Equal(sale.SalePrice, foundSale.SalePrice);
            Assert.Equal(sale.Commission, foundSale.Commission);
        }

        [Fact(DisplayName = "Find Non-Existent Sale Using Service")]
        public void FindNonExistentSale()
        {
            var sale = _saleService.GetSaleById(9999); // Assuming this ID does not exist
            Assert.Null(sale);
        }

        [Fact(DisplayName = "Create Sale With Non-Existent Foreign Keys Using Service Should Throw Exception")]
        public void CreateSaleWithNonExistentForeignKeys()
        {
            using var context = _contextFactory.CreateDbContext();

            var product = new Product { Type = "Bicycle", Name = "Mountain Bike", CommissionPercentage = 0.15m };
            context.Products.Add(product);

            var salesperson = new Salesperson { Name = "Doug Doe", EmployeeId = "EMP789" };
            context.Salespersons.Add(salesperson);

            var customer = new Customer { Name = "Dan Smith", Contact = "dan.smith@gmail.com", ContactType = ContactType.Email };
            context.Customers.Add(customer);

            context.SaveChanges();

            var invalidProductSale = new Sale
            {
                ProductId = 9999, // Non-existent ProductId
                SalespersonId = salesperson.Id, 
                CustomerId = customer.Id, 
                SaleDate = DateTime.UtcNow,
                SalePrice = 200.00m,
                Commission = 20.00m
            };

            var productException = Assert.Throws<ArgumentException>(() => _saleService.AddSale(invalidProductSale));
            Assert.Contains("Invalid ProductId.", productException.Message);

            var invalidSalespersonSale = new Sale
            {
                ProductId = product.Id,
                SalespersonId = 9999, // Non-existent SalespersonId
                CustomerId = customer.Id,
                SaleDate = DateTime.UtcNow,
                SalePrice = 200.00m,
                Commission = 20.00m
            };

            var salespersonExecption = Assert.Throws<ArgumentException>(() => _saleService.AddSale(invalidSalespersonSale));
            Assert.Contains("Invalid SalespersonId.", salespersonExecption.Message);

            var invalidCustomerSale = new Sale
            {
                ProductId = product.Id,
                SalespersonId = salesperson.Id,
                CustomerId = 9999, // Non-existent CustomerId
                SaleDate = DateTime.UtcNow,
                SalePrice = 200.00m,
                Commission = 20.00m
            };

            var customerException = Assert.Throws<ArgumentException>(() => _saleService.AddSale(invalidCustomerSale));
            Assert.Contains("Invalid CustomerId.", customerException.Message);
        }

        [Fact(DisplayName = "Get Sales By Date Range Using Service")]
        public void GetSalesByDateRange()
        {
            using var context = _contextFactory.CreateDbContext();
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            var product = new Product { Type = "Bicycle", Name = "Hybrid Bike", CommissionPercentage = 0.12m };
            context.Products.Add(product);

            var salesperson = new Salesperson { Name = "Eve Doe", EmployeeId = "EMP321" };
            context.Salespersons.Add(salesperson);

            var customer = new Customer { Name = "Evan Smith", Contact = "esmith@gmail.com", ContactType = ContactType.Email };
            context.Customers.Add(customer);
            context.SaveChanges();

            var sale1 = new Sale
            {
                ProductId = product.Id,
                SalespersonId = salesperson.Id,
                CustomerId = customer.Id,
                SaleDate = new DateTime(2023, 1, 15),
                SalePrice = 120.00m,
                Commission = 12.00m
            };
            var sale2 = new Sale
            {
                ProductId = product.Id,
                SalespersonId = salesperson.Id,
                CustomerId = customer.Id,
                SaleDate = new DateTime(2023, 2, 20),
                SalePrice = 130.00m,
                Commission = 13.00m
            };
            context.Sales.AddRange(sale1, sale2);
            context.SaveChanges();

            var salesInRange = _saleService.GetSalesByDateRange(new DateTime(2023, 1, 1), new DateTime(2023, 1, 31)).ToList();
            Assert.Single(salesInRange);
            Assert.Equal(sale1.SaleDate, salesInRange[0].SaleDate);

            var salesInFullRange = _saleService.GetSalesByDateRange(new DateTime(2023, 1, 1), new DateTime(2023, 3, 1)).ToList();
            Assert.Equal(2, salesInFullRange.Count);
        }

        [Fact(DisplayName = "Get Sales By Invalid Date Range Using Service Should Throw Exception")]
        public void GetSalesByInvalidDateRange()
        {
            var minDateException = Assert.Throws<ArgumentException>(() => _saleService.GetSalesByDateRange(DateTime.MinValue, DateTime.UtcNow));
            Assert.Contains("startDate is required.", minDateException.Message);

            var maxDateException = Assert.Throws<ArgumentException>(() => _saleService.GetSalesByDateRange(DateTime.UtcNow, DateTime.MinValue));
            Assert.Contains("endDate is required.", maxDateException.Message);

            var invalidRangeException = Assert.Throws<ArgumentException>(() => _saleService.GetSalesByDateRange(DateTime.UtcNow, DateTime.UtcNow.AddDays(-1)));
            Assert.Contains("startDate must be less than endDate.", invalidRangeException.Message);
        }

    }
}