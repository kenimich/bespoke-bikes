using BespokeBikesApi.Data.Models;

namespace BespokeBikesApi.Logic.Interfaces {
    
    public interface ICustomerService
    {
        public Customer GetCustomerById(int id);

        public int AddCustomer(Customer customer);

        public bool UpdateCustomer(Customer customer);
    }
}