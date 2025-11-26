using BespokeBikesApi.Data.Models;
using BespokeBikesApi.Tests.Setup.Data.Factories;
using BespokeBikesApi.Logic;

namespace BespokeBikesApi.Tests.Logic {

    public class SaleServiceTests
    {
        private readonly SaleService _saleService;

        public SaleServiceTests()
        {
            var contextFactory = new BespokeBikesContextInMemoryFactory("SaleServiceTests_Database");
            _saleService = new SaleService(contextFactory);
        }

        [Fact(DisplayName = "Create Sale Using Service")]
        public void CreateSale()
        {
            var sale = new Sale
            {
                ProductId = 1,
                SalespersonId = 1,
                CustomerId = 1,
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
            var sale = new Sale
            {
                ProductId = 1,
                SalespersonId = 1,
                CustomerId = 1,
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

    }
}