using BespokeBikesApi.Data;
using BespokeBikesApi.Data.Models;
using BespokeBikesApi.Data.Factories;

namespace BespokeBikesApi.Logic {
    
    public class CustomerService
    {
        private IBespokeBikesContextFactory _contextFactory;

        public CustomerService(IBespokeBikesContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public int AddCustomer(Customer customer)
        {
            using var context = _contextFactory.CreateDbContext();
            context.Customers.Add(customer);
            context.SaveChanges();
            return customer.Id;
        }

        public Customer GetCustomerById(int id)
        {
            using var context = _contextFactory.CreateDbContext();
            return context.Customers.Find(id);
        }

        public bool UpdateCustomer(Customer customer)
        {
            //TODO: Attach to database.
            using var context = _contextFactory.CreateDbContext();
            context.Customers.Update(customer);
            context.SaveChanges();
            return true;
        }
    }
}