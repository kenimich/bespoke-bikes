using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BespokeBikesApi.Controllers;
using BespokeBikesApi.Data.Models;
using BespokeBikesApi.Logic;

namespace BespokeBikesApi.Controllers.Tests
{
    public class CustomerControllerTest
    {

        [Fact("POST Customer - Respond with CreateAtActionResult with Location and Object When Customer Created")]
        public void Create_ReturnsCustomer_WhenCreated()
        {
            var customer = new Customer
            {
              Id = 1,
              Name = "Matt Johnson",
              Contact = "m.johnson@yahoo.com",
              ContactType = ContactType.Email
            };

            var expectedId = 1;
            var mockLogger = new Mock<ILogger<CustomerController>>();
            var mockService = new Mock<ICustomerService>();
            mockService.Setup(s => s.AddCustomer(customer)).Returns(expectedId);

            var controller = new CustomerController(mockLogger.Object, mockService.Object);

            var result = controller.Create(customer);

            Assert.NotNull(result);
            Assert.NotNull(result.Result);
            Assert.IsType<ActionResult<Customer>>(result);
            Assert.IsType<CreatedAtRouteResult>(result.Result);
            
            var routeResult = result.Result as CreatedAtRouteResult;
            Assert.Equal(customer, routeResult?.Value);
            Assert.Equal("GetCustomerById", routeResult?.RouteName);
            Assert.NotNull(routeResult?.RouteValues);
            if(routeResult?.RouteValues != null)    {
                Assert.Single(routeResult?.RouteValues!);
                Assert.Equal(expectedId, routeResult?.RouteValues["id"]);
            }
        }

        [Fact("POST Customer - Respond with Validation Problem Details if Customer is Missing Fields")]
        public void Create_ReturnsValidationProblemDetails_WhenMissionFields()
        {
            var customer = new Customer
            {
              Id = 1,
              Contact = "m.johnson@yahoo.com",
              ContactType = ContactType.Email
            };

            var expectedId = 1;            
            var mockLogger = new Mock<ILogger<CustomerController>>();
            var mockService = new Mock<ICustomerService>();
            mockService.Setup(s => s.AddCustomer(customer)).Returns(expectedId);

            var controller = new CustomerController(mockLogger.Object, mockService.Object);
            controller.ModelState.AddModelError(nameof(Customer.Name), "The Name field is required.");

            var result = controller.Create(customer);

            Assert.NotNull(result);
            Assert.Null(result.Value); // not returning an object
            Assert.IsType<ActionResult<Customer>>(result);
            Assert.IsType<BadRequestObjectResult>(result.Result);

            var badResult = result.Result as BadRequestObjectResult;
            Assert.NotNull(badResult?.Value);
            Assert.IsType<SerializableError>(badResult?.Value);

            if(badResult?.Value != null && badResult?.Value is SerializableError)
            {
                var error = badResult?.Value as SerializableError;
                Assert.NotEmpty(error!);
                Assert.Single(error!);
                var pair = error!.First();
                Assert.Equal(nameof(Customer.Name), pair.Key);
                var messages = Assert.IsType<string[]>(pair.Value);
                Assert.Single(messages);
                Assert.Equal("The Name field is required.", messages[0]);
            }
        }

        [Fact("GET Customer - Respond with 200 OK and Customer When Customer Service Returns Customer")]
        public void Read_ReturnsCustomer_WhenFound()
        {
            var id = 1;
            var expected = new Customer { Id = id };
            var mockLogger = new Mock<ILogger<CustomerController>>();
            var mockService = new Mock<ICustomerService>();
            mockService.Setup(s => s.GetCustomerById(id)).Returns(expected);

            var controller = new CustomerController(mockLogger.Object, mockService.Object);

            var result = controller.Read(id);

            Assert.NotNull(result);
            Assert.Null(result.Result); // means direct value was returned
            Assert.NotNull(result.Value);
            Assert.Equal(id, result.Value.Id);
            Assert.IsType<ActionResult<Customer>>(result);
        }

        [Fact("GET Customer - Respond with 404 Not Found When Customer Service Returns Null")]
        public void Read_ReturnsNotFound_WhenMissing()
        {
            var id = 99;
            var mockLogger = new Mock<ILogger<CustomerController>>();
            var mockService = new Mock<ICustomerService>();
            mockService.Setup(s => s.GetCustomerById(id)).Returns(() => null);

            var controller = new CustomerController(mockLogger.Object, mockService.Object);

            var result = controller.Read(id);

            Assert.NotNull(result);
            Assert.Null(result.Value);
            Assert.IsType<NotFoundResult>(result.Result);
        }
    }
}