using BespokeBikesApi.Data.Models;
using BespokeBikesApi.Data.Factories;
using Microsoft.EntityFrameworkCore;

namespace BespokeBikesApi.Logic {
    
    public class CustomerService : ICustomerService
    {
        private IBespokeBikesContextFactory _contextFactory;

        public CustomerService(IBespokeBikesContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public int AddCustomer(Customer customer)
        {
            using var context = _contextFactory.CreateDbContext();

            if(context.Customers.Any(c => c.Name == customer.Name && c.Contact == customer.Contact))
            {
                throw new ArgumentException("Name and Contact must be a unique combination.", "NameAndContact");
            }

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
            using var context = _contextFactory.CreateDbContext();

            if(context.Customers.Any(c => c.Name == customer.Name && c.Contact == customer.Contact && c.Id != customer.Id))
            {
                throw new ArgumentException("Name and Contact must be a unique combination.", "NameAndContact");
            }

            context.Customers.Update(customer);
            context.SaveChanges();
            return true;
        }
    }
}