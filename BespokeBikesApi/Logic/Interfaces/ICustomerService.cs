using BespokeBikesApi.Data.Models;

namespace BespokeBikesApi.Logic {

    public interface ICustomerService {

        public int AddCustomer(Customer customer);

        public Customer GetCustomerById(int id);

        public bool UpdateCustomer(Customer customer);
    }
}