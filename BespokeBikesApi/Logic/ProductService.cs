using BespokeBikesApi.Data;
using BespokeBikesApi.Data.Models;
using BespokeBikesApi.Data.Factories;

namespace BespokeBikesApi.Logic
{
    public class ProductService
    {
        private readonly IBespokeBikesContextFactory _contextFactory;

        public ProductService(IBespokeBikesContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public int AddProduct(Product product)
        {
            using var context = _contextFactory.CreateDbContext();
            context.Products.Add(product);
            context.SaveChanges();
            return product.Id;
        }

        public Product GetProductById(int id)
        {
            using var context = _contextFactory.CreateDbContext();
            return context.Products.Find(id);
        }

        public bool UpdateProduct(Product product)
        {
            using var context = _contextFactory.CreateDbContext();
            context.Products.Update(product);
            context.SaveChanges();
            return true;
        }
    }
}