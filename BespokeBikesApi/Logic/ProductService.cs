using BespokeBikesApi.Data;
using BespokeBikesApi.Data.Models;
using BespokeBikesApi.Data.Factories;
using BespokeBikesApi.Logic.DTO;

namespace BespokeBikesApi.Logic
{
    public class ProductService
    {
        private readonly IBespokeBikesContextFactory _contextFactory;

        public ProductService(IBespokeBikesContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public ProductCurrentInventory AddProduct(ProductNewInventory product)
        {
            using var context = _contextFactory.CreateDbContext();

            if(context.Products.Any(p => p.Name == product.Name && p.Type == product.Type))
            {
                throw new ArgumentException("Product Name must be unique.", nameof(Product.Name));
            }

            context.Products.Add(product);
            context.SaveChanges();

            context.Inventories.Add(new Inventory
            {
                ProductId = product.Id,
                Quantity = product.QuantityPurchased,
                PurchasePrice = product.PurchasePrice,
                PurchaseDate = product.PurchaseDate
            });
            context.SaveChanges();

            return GetProductById(product.Id);
        }

        public ProductCurrentInventory AddInventory(ProductNewInventory product)
        {
            using var context = _contextFactory.CreateDbContext();

            var existingProduct = context.Products.Find(product.Id);
            if(existingProduct == null)
            {
                throw new ArgumentException("Product does not exist.", nameof(Product.Id));
            }

            var inventory = new Inventory
            {
                ProductId = product.Id,
                Quantity = product.QuantityPurchased,
                PurchasePrice = product.PurchasePrice,
                PurchaseDate = product.PurchaseDate
            };

            context.Inventories.Add(inventory);
            context.SaveChanges();

            return GetProductById(product.Id);
        }

        public ProductCurrentInventory GetProductById(int id)
        {
            using var context = _contextFactory.CreateDbContext();
            var productCurrentInventory = context.Products
                .Where(p => p.Id == id)
                .Select(p => new ProductCurrentInventory
                {
                    Id = p.Id,
                    Type = p.Type,
                    Name = p.Name,
                    CommissionPercentage = p.CommissionPercentage,
                    QuantityInStock = context.Inventories
                        .Where(i => i.ProductId == p.Id)
                        .Sum(i => i.Quantity),
                    PurchasePrices = context.Inventories
                        .Where(i => i.ProductId == p.Id)
                        .ToList()
                })
                .FirstOrDefault();

            if(productCurrentInventory == null)
            {
                throw new ArgumentException("Product does not exist.", nameof(id));
            }

            productCurrentInventory.QuantityInStock -= context.Sales
                .Where(s => s.ProductId == id)
                .Count();

            return productCurrentInventory;
        }

        public bool UpdateProduct(Product product)
        {
            using var context = _contextFactory.CreateDbContext();

            if(context.Products.Any(p => p.Name == product.Name && p.Id != product.Id))
            {
                throw new ArgumentException("Product Name must be unique.", nameof(Product.Name));
            }

            context.Products.Update(product);
            context.SaveChanges();
            return true;
        }
    }
}