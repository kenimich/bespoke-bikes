using BespokeBikesApi.Data;
using BespokeBikesApi.Data.Models;
using BespokeBikesApi.Data.Factories;


namespace BespokeBikesApi.Logic {
    
    public class CustomerService : Interfaces.ICustomerService
    {
        private ISalesContextFactory _contextFactory;

        public CustomerService(ISalesContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public Customer GetCustomerById(int id)
        {
            using var context = _contextFactory.CreateDbContext();
            return context.Customers.Find(id);
        }

        public int AddCustomer(Customer customer)
        {
            using var context = _contextFactory.CreateDbContext();
            context.Customers.Add(customer);
            context.SaveChanges();
            return customer.Id;
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