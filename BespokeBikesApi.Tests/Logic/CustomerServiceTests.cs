using BespokeBikesApi.Data.Models;
using BespokeBikesApi.Tests.Setup.Data.Factories;
using BespokeBikesApi.Logic;

namespace BespokeBikesApi.Tests.Logic {

    public class CustomerServiceTests
    {
        private readonly CustomerService _customerService;

        public CustomerServiceTests()
        {
            var contextFactory = new BespokeBikesContextInMemoryFactory("CustomerServiceTests_Database");
            _customerService = new CustomerService(contextFactory);
        }

        [Fact(DisplayName = "Create Customer Using Service")]
        public void CreateCustomer()
        {
            var customerId = _customerService.AddCustomer(new Customer {
                Name = "Service Test Customer", 
                Contact = "kenimich@gmail.com", 
                ContactType = ContactType.Email 
                });

            Assert.True(customerId > 0, "Customer ID should be greater than 0 after creation.");
        }

        [Fact(DisplayName = "Create and Find Customer Using Service")]
        public void CreateAndFindCustomer()
        {
            var customerId = _customerService.AddCustomer(new Customer {
                Name = "Service Test Customer 2", 
                Contact = "kenimich2@gmail.com", 
                ContactType = ContactType.Email 
                });

            Assert.True(customerId > 0, "Customer ID should be greater than 0 after creation.");

            var customer = _customerService.GetCustomerById(customerId);
            Assert.NotNull(customer);
            Assert.Equal("Service Test Customer 2", customer.Name);
            Assert.Equal("kenimich2@gmail.com", customer.Contact);
            Assert.Equal(ContactType.Email, customer.ContactType);
        } 

        [Fact(DisplayName = "Find Non-Existent Customer Using Service")]
        public void FindNonExistentCustomer()
        {
            var customer = _customerService.GetCustomerById(9999); // Assuming this ID does not exist
            Assert.Null(customer);
        }

        [Fact(DisplayName = "Update Customer Using Service")]
        public void UpdateCustomer()
        {
            var customerId = _customerService.AddCustomer(new Customer {
                Name = "Service Test Customer 3", 
                Contact = "kenimich3@gmail.com", 
                ContactType = ContactType.Email 
                });

            Assert.True(customerId > 0, "Customer ID should be greater than 0 after creation.");

            var customer = _customerService.GetCustomerById(customerId);
            Assert.NotNull(customer);
            Assert.Equal("Service Test Customer 3", customer.Name);
            Assert.Equal("kenimich3@gmail.com", customer.Contact);
            Assert.Equal(ContactType.Email, customer.ContactType);

            var customerToUpdate = new Customer
            {
                Id = customer.Id,
                Name = customer.Name,
                Contact = "770-555-1234",
                ContactType = ContactType.PhoneNumber
            };
            var updateResult = _customerService.UpdateCustomer(customerToUpdate);
            Assert.True(updateResult, "Customer update should return true.");

            var customerUpdated = _customerService.GetCustomerById(customerId);
            Assert.NotNull(customerUpdated);
            Assert.Equal("770-555-1234", customerUpdated.Contact);
            Assert.Equal(ContactType.PhoneNumber, customerUpdated.ContactType);
        }
    }
}
