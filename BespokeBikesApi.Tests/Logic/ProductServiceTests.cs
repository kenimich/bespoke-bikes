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
                Type = "Bicycle",
                Name = "Service Test Product", 
                CommissionPercentage = 0.10m 
                });

            Assert.True(productId > 0, "Product ID should be greater than 0 after creation.");
        }

        [Fact(DisplayName = "Create and Find Product Using Service")]
        public void CreateAndFindProduct()
        {
            var productId = _productService.AddProduct(new Product {
                Type = "Bicycle",
                Name = "Service Test Product 2", 
                CommissionPercentage = 0.15m 
                });

            Assert.True(productId > 0, "Product ID should be greater than 0 after creation.");

            var product = _productService.GetProductById(productId);
            Assert.NotNull(product);
            Assert.Equal(productId, product.Id);
            Assert.Equal("Bicycle", product.Type);
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
                Type = "Bicycle",
                Name = "Service Test Product 3", 
                CommissionPercentage = 0.20m 
                });

            Assert.True(productId > 0, "Product ID should be greater than 0 after creation.");

            var product = _productService.GetProductById(productId);
            Assert.NotNull(product);
            Assert.Equal(productId, product.Id);
            Assert.Equal("Bicycle", product.Type);
            Assert.Equal("Service Test Product 3", product.Name);
            Assert.Equal(0.20m, product.CommissionPercentage);
            
            var productToUpdate = new Product {
                Id = product.Id,
                Type = product.Type,
                Name = product.Name,
                CommissionPercentage = 0.25m
            };
            var updateResult =  _productService.UpdateProduct(productToUpdate);
            Assert.True(updateResult, "Product update should return true.");

            var updatedProduct = _productService.GetProductById(productId);
            Assert.NotNull(updatedProduct);
            Assert.Equal(productId, updatedProduct.Id);
            Assert.Equal("Bicycle", updatedProduct.Type);
            Assert.Equal("Service Test Product 3", updatedProduct.Name);
            Assert.Equal(0.25m, updatedProduct.CommissionPercentage);
        }

        [Fact(DisplayName = "Create Product With Duplicate Name Using Service")]
        public void CreateProductWithDuplicateName()
        {
            var productId = _productService.AddProduct(new Product {
                Type = "Bicycle",
                Name = "Unique Product 3", 
                CommissionPercentage = 0.10m 
                });

            Assert.True(productId > 0, "Product ID should be greater than 0 after creation.");

            var duplicateProduct = new Product {
                Type = "Bicycle",
                Name = "Unique Product 3", // Duplicate name
                CommissionPercentage = 0.15m 
            };

            var exception = Assert.Throws<ArgumentException>(() => _productService.AddProduct(duplicateProduct));
            Assert.Equal("Product Name must be unique. (Parameter 'Name')", exception.Message);
        }

        [Fact(DisplayName = "Update Product With Duplicate Name Using Service")]
        public void UpdateProductWithDuplicateName()
        {
            var productId1 = _productService.AddProduct(new Product {
                Type = "Bicycle",
                Name = "Unique Product 1", 
                CommissionPercentage = 0.10m 
                });

            var productId2 = _productService.AddProduct(new Product {
                Type = "Bicycle",
                Name = "Unique Product 2", 
                CommissionPercentage = 0.15m 
                });

            Assert.True(productId1 > 0, "First Product ID should be greater than 0 after creation.");
            Assert.True(productId2 > 0, "Second Product ID should be greater than 0 after creation.");

            var productToUpdate = new Product {
                Id = productId2,
                Type = "Bicycle",
                Name = "Unique Product 1", // Duplicate name
                CommissionPercentage = 0.20m
            };

            var exception = Assert.Throws<ArgumentException>(() => _productService.UpdateProduct(productToUpdate));
            Assert.Equal("Product Name must be unique. (Parameter 'Name')", exception.Message);
        }
    }
}