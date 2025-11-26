using BespokeBikesApi.Data.Models;
using BespokeBikesApi.Tests.Setup.Data.Factories;
using BespokeBikesApi.Logic;

namespace BespokeBikesApi.Tests.Logic {

    public class ProductServiceTests
    {
        private readonly ProductService _productService;

        public ProductServiceTests()
        {
            var contextFactory = new BespokeBikesContextInMemoryFactory("ProductServiceTests_Database");
            _productService = new ProductService(contextFactory);
        }

        [Fact(DisplayName = "Create Product Using Service")]
        public void CreateProduct()
        {
            var productId = _productService.AddProduct(new Product {
                Name = "Service Test Product", 
                CommissionPercentage = 0.10m 
                });

            Assert.True(productId > 0, "Product ID should be greater than 0 after creation.");
        }

        [Fact(DisplayName = "Create and Find Product Using Service")]
        public void CreateAndFindProduct()
        {
            var productId = _productService.AddProduct(new Product {
                Name = "Service Test Product 2", 
                CommissionPercentage = 0.15m 
                });

            Assert.True(productId > 0, "Product ID should be greater than 0 after creation.");

            var product = _productService.GetProductById(productId);
            Assert.NotNull(product);
            Assert.Equal("Service Test Product 2", product.Name);
            Assert.Equal(0.15m, product.CommissionPercentage);
        }

        [Fact(DisplayName = "Find Non-Existent Product Using Service")]
        public void FindNonExistentProduct()
        {
            var product = _productService.GetProductById(9999); // Assuming this ID does not exist
            Assert.Null(product);
        }

        [Fact(DisplayName = "Update Product Using Service")]
        public void UpdateProduct()
        {
            var productId = _productService.AddProduct(new Product {
                Name = "Service Test Product 3", 
                CommissionPercentage = 0.20m 
                });

            Assert.True(productId > 0, "Product ID should be greater than 0 after creation.");

            var product = _productService.GetProductById(productId);
            Assert.NotNull(product);
            Assert.Equal("Service Test Product 3", product.Name);
            Assert.Equal(0.20m, product.CommissionPercentage);
            
            var productToUpdate = new Product {
                Id = product.Id,
                Name = product.Name,
                CommissionPercentage = 0.25m
            };
            var updateResult =  _productService.UpdateProduct(productToUpdate);
            Assert.True(updateResult, "Product update should return true.");

            var updatedProduct = _productService.GetProductById(productId);
            Assert.NotNull(updatedProduct);
            Assert.Equal("Service Test Product 3", updatedProduct.Name);
            Assert.Equal(0.25m, updatedProduct.CommissionPercentage);
        }
    }
}