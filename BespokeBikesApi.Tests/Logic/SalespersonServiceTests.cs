using BespokeBikesApi.Data.Models;
using BespokeBikesApi.Tests.Setup.Data.Factories;
using BespokeBikesApi.Logic;

namespace BespokeBikesApi.Tests.Logic {

    public class SalespersonServiceTests
    {
        private readonly SalespersonService _salespersonService;

        public SalespersonServiceTests()
        {
            var contextFactory = new BespokeBikesContextInMemoryFactory("SalespersonServiceTests_Database");
            _salespersonService = new SalespersonService(contextFactory);
        }

        [Fact(DisplayName = "Create Salespseron Using Service")]
        public void CreateSalesperson()
        {
            var salespersonId = _salespersonService.AddSalesperson(new Salesperson {
                Name = "Service Test Salesperson", 
                EmployeeId = "EMP123"
                });

            Assert.True(salespersonId > 0, "Salesperson ID should be greater than 0 after creation.");
        }

        [Fact(DisplayName = "Create and Find Salespseron Using Service")]
        public void CreateAndFindSalespseron()
        {
            var salespersonId = _salespersonService.AddSalesperson(new Salesperson {
                Name = "Service Test Salesperson 2", 
                EmployeeId = "EMP345"
                });

            Assert.True(salespersonId > 0, "Salesperson ID should be greater than 0 after creation.");

            var salesperson = _salespersonService.GetSalespersonById(salespersonId);
            Assert.NotNull(salesperson);
            Assert.Equal("Service Test Salesperson 2", salesperson.Name);
            Assert.Equal("EMP345", salesperson.EmployeeId);
        }

        [Fact(DisplayName = "Find Non-Existent Salesperson Using Service")]
        public void FindNonExistentSalesperson()
        {
            var salesperson = _salespersonService.GetSalespersonById(9999); // Assuming this ID does not exist
            Assert.Null(salesperson);
        }

        [Fact(DisplayName = "Update Salesperson Using Service")]
        public void UpdateSalesperson()
        {
            var salespersonId = _salespersonService.AddSalesperson(new Salesperson {
                Name = "Service Test Salesperson 3", 
                EmployeeId = "EMP678"
                });

            Assert.True(salespersonId > 0, "Salesperson ID should be greater than 0 after creation.");

            var salesperson = _salespersonService.GetSalespersonById(salespersonId);
            Assert.NotNull(salesperson);
            Assert.Equal("Service Test Salesperson 3", salesperson.Name);
            Assert.Equal("EMP678", salesperson.EmployeeId);
            
            var salespersonToUpdate = new Salesperson {
                Id = salesperson.Id,
                Name = "Last Name Changed",
                EmployeeId = salesperson.EmployeeId
            };
            var updateResult =  _salespersonService.UpdateSalesperson(salespersonToUpdate);
            Assert.True(updateResult, "Salesperson update should return true.");

            var updatedSalesperson = _salespersonService.GetSalespersonById(salespersonId);
            Assert.NotNull(updatedSalesperson);
            Assert.Equal("Last Name Changed", updatedSalesperson.Name);
            Assert.Equal("EMP678", updatedSalesperson.EmployeeId);
        }
    }
}