using System.ComponentModel.DataAnnotations;
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
    }
}