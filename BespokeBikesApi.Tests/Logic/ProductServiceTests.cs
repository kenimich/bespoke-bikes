using BespokeBikesApi.Data.Models;
using BespokeBikesApi.Logic;
using BespokeBikesApi.Logic.DTO;
using BespokeBikesApi.Tests.Setup.Data.Factories;

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
            var productCurrentInventory = _productService.AddProduct(new ProductNewInventory {
                Type = "Bicycle",
                Name = "Service Test Product", 
                CommissionPercentage = 0.10m,
                QuantityPurchased = 10,
                PurchasePrice = 500.00m,
                PurchaseDate = DateTime.UtcNow
                });

            Assert.True(productCurrentInventory.Id > 0, "Product ID should be greater than 0 after creation.");
            Assert.Equal("Bicycle", productCurrentInventory.Type);
            Assert.Equal("Service Test Product", productCurrentInventory.Name);
            Assert.Equal(0.10m, productCurrentInventory.CommissionPercentage);
            Assert.Equal(10, productCurrentInventory.QuantityInStock);
            Assert.Single(productCurrentInventory.PurchasePrices);
            Assert.Equal(500.00m, productCurrentInventory.PurchasePrices[0].PurchasePrice);
        }

        [Fact(DisplayName = "Create and Find Product Using Service")]
        public void CreateAndFindProduct()
        {
            var productCurrentInventory = _productService.AddProduct(new ProductNewInventory {
                Type = "Bicycle",
                Name = "Service Test Product 2", 
                CommissionPercentage = 0.15m,
                QuantityPurchased = 5,
                PurchasePrice = 600.00m,
                PurchaseDate = DateTime.UtcNow
                });

            Assert.True(productCurrentInventory.Id > 0, "Product ID should be greater than 0 after creation.");
            Assert.Equal("Bicycle", productCurrentInventory.Type);
            Assert.Equal("Service Test Product 2", productCurrentInventory.Name);
            Assert.Equal(0.15m, productCurrentInventory.CommissionPercentage);
            Assert.Equal(5, productCurrentInventory.QuantityInStock);
            Assert.Single(productCurrentInventory.PurchasePrices);
            Assert.Equal(600.00m, productCurrentInventory.PurchasePrices[0].PurchasePrice);

            var product = _productService.GetProductById(productCurrentInventory.Id);
            Assert.NotNull(product);
            Assert.Equal(productCurrentInventory.Id, product.Id);
            Assert.Equal("Bicycle", product.Type);
            Assert.Equal("Service Test Product 2", product.Name);
            Assert.Equal(0.15m, product.CommissionPercentage);
            Assert.Equal(5, product.QuantityInStock);
            Assert.Single(product.PurchasePrices);
            Assert.Equal(600.00m, product.PurchasePrices[0].PurchasePrice);
        }

        [Fact(DisplayName = "Find Non-Existent Product Using Service")]
        public void FindNonExistentProduct()
        {
            var exception = Assert.Throws<ArgumentException>(() => _productService.GetProductById(9999)); // Assuming this ID does not exist
            Assert.Equal("Product does not exist. (Parameter 'id')", exception.Message);

        }

        [Fact(DisplayName = "Update Product Using Service")]
        public void UpdateProduct()
        {
            var productCurrentInventory = _productService.AddProduct(new ProductNewInventory {
                Type = "Bicycle",
                Name = "Service Test Product 3", 
                CommissionPercentage = 0.20m,
                QuantityPurchased = 8,
                PurchasePrice = 700.00m,
                PurchaseDate = DateTime.UtcNow
                });

            Assert.True(productCurrentInventory.Id > 0, "Product ID should be greater than 0 after creation.");
            Assert.Equal("Bicycle", productCurrentInventory.Type);
            Assert.Equal("Service Test Product 3", productCurrentInventory.Name);
            Assert.Equal(0.20m, productCurrentInventory.CommissionPercentage);
            Assert.Equal(8, productCurrentInventory.QuantityInStock);
            Assert.Single(productCurrentInventory.PurchasePrices);
            Assert.Equal(700.00m, productCurrentInventory.PurchasePrices[0].PurchasePrice);

            var product = _productService.GetProductById(productCurrentInventory.Id);
            Assert.NotNull(product);
            Assert.Equal(productCurrentInventory.Id, product.Id);
            Assert.Equal("Bicycle", product.Type);
            Assert.Equal("Service Test Product 3", product.Name);
            Assert.Equal(0.20m, product.CommissionPercentage);
            Assert.Equal(8, product.QuantityInStock);
            Assert.Single(product.PurchasePrices);
            Assert.Equal(700.00m, product.PurchasePrices[0].PurchasePrice);
            
            var productToUpdate = new Product {
                Id = product.Id,
                Type = product.Type,
                Name = product.Name,
                CommissionPercentage = 0.25m
            };
            var updateResult =  _productService.UpdateProduct(productToUpdate);
            Assert.True(updateResult, "Product update should return true.");

            var updatedProduct = _productService.GetProductById(productToUpdate.Id);
            Assert.NotNull(updatedProduct);
            Assert.Equal(productCurrentInventory.Id, updatedProduct.Id);
            Assert.Equal("Bicycle", updatedProduct.Type);
            Assert.Equal("Service Test Product 3", updatedProduct.Name);
            Assert.Equal(0.25m, updatedProduct.CommissionPercentage);
            Assert.Equal(8, updatedProduct.QuantityInStock);
            Assert.Single(updatedProduct.PurchasePrices);
            Assert.Equal(700.00m, updatedProduct.PurchasePrices[0].PurchasePrice);
        }

        [Fact(DisplayName = "Create Product With Duplicate Name Using Service")]
        public void CreateProductWithDuplicateName()
        {
            var productCurrentInventory = _productService.AddProduct(new ProductNewInventory {
                Type = "Bicycle",
                Name = "Unique Product 3", 
                CommissionPercentage = 0.10m,
                QuantityPurchased = 12,
                PurchasePrice = 800.00m,
                PurchaseDate = DateTime.UtcNow
                });

            Assert.True(productCurrentInventory.Id > 0, "Product ID should be greater than 0 after creation.");
            Assert.Equal("Bicycle", productCurrentInventory.Type);
            Assert.Equal("Unique Product 3", productCurrentInventory.Name);

            var duplicateProduct = new ProductNewInventory {
                Type = "Bicycle",
                Name = "Unique Product 3", // Duplicate name
                CommissionPercentage = 0.15m,
                QuantityPurchased = 5,
                PurchasePrice = 900.00m,
                PurchaseDate = DateTime.UtcNow
            };

            var exception = Assert.Throws<ArgumentException>(() => _productService.AddProduct(duplicateProduct));
            Assert.Equal("Product Name must be unique. (Parameter 'Name')", exception.Message);
        }

        [Fact(DisplayName = "Update Product With Duplicate Name Using Service")]
        public void UpdateProductWithDuplicateName()
        {
            var product1 = _productService.AddProduct(new ProductNewInventory {
                Type = "Bicycle",
                Name = "Unique Product 1", 
                CommissionPercentage = 0.10m,
                QuantityPurchased = 7,
                PurchasePrice = 400.00m,
                PurchaseDate = DateTime.UtcNow
                });

            var product2 = _productService.AddProduct(new ProductNewInventory {
                Type = "Bicycle",
                Name = "Unique Product 2", 
                CommissionPercentage = 0.15m,
                QuantityPurchased = 9,
                PurchasePrice = 450.00m,
                PurchaseDate = DateTime.UtcNow
                });

            Assert.True(product1.Id > 0, "First Product ID should be greater than 0 after creation.");
            Assert.True(product2.Id > 0, "Second Product ID should be greater than 0 after creation.");

            var productToUpdate = new Product {
                Id = product2.Id,
                Type = "Bicycle",
                Name = "Unique Product 1", // Duplicate name
                CommissionPercentage = 0.20m
            };

            var exception = Assert.Throws<ArgumentException>(() => _productService.UpdateProduct(productToUpdate));
            Assert.Equal("Product Name must be unique. (Parameter 'Name')", exception.Message);
        }

        [Fact(DisplayName = "Add Inventory To Existing Product Using Service")]
        public void AddInventoryToExistingProduct()
        {
            var productCurrentInventory = _productService.AddProduct(new ProductNewInventory {
                Type = "Bicycle",
                Name = "Inventory Test Product", 
                CommissionPercentage = 0.10m,
                QuantityPurchased = 10,
                PurchasePrice = 500.00m,
                PurchaseDate = DateTime.UtcNow
                });

            Assert.Equal(10, productCurrentInventory.QuantityInStock);

            var additionalInventory = new ProductNewInventory {
                Id = productCurrentInventory.Id,
                Type = "Bicycle",
                Name = "Inventory Test Product", 
                CommissionPercentage = 0.10m,
                QuantityPurchased = 5,
                PurchasePrice = 450.00m,
                PurchaseDate = DateTime.UtcNow
            };

            var updatedProduct = _productService.AddInventory(additionalInventory);
            Assert.Equal(15, updatedProduct.QuantityInStock);
            Assert.Equal(2, updatedProduct.PurchasePrices.Count);
        }

        [Fact(DisplayName = "Add Inventory To Non-Existent Product Using Service")]
        public void AddInventoryToNonExistentProduct()
        {
            var nonExistentProduct = new ProductNewInventory {
                Id = 9999, // Assuming this ID does not exist
                Type = "Bicycle",
                Name = "Non-Existent Product", 
                CommissionPercentage = 0.10m,
                QuantityPurchased = 5,
                PurchasePrice = 300.00m,
                PurchaseDate = DateTime.UtcNow
            };

            var exception = Assert.Throws<ArgumentException>(() => _productService.AddInventory(nonExistentProduct));
            Assert.Equal("Product does not exist. (Parameter 'Id')", exception.Message);
        }
    }
}