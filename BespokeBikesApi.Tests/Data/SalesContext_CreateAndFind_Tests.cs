using System.ComponentModel.DataAnnotations;
using BespokeBikesApi.Data;
using BespokeBikesApi.Data.Models;
using BespokeBikesApi.Tests.Setup.Data.Factories;
using Microsoft.EntityFrameworkCore;

namespace BespokeBikesApi.Tests.Data {

    public class SalesContext_CreateAndFind_Tests
    {
        private readonly SalesContextInMemoryFactory _contextFactory;

        public SalesContext_CreateAndFind_Tests()
        {
            _contextFactory = new SalesContextInMemoryFactory("SalesContext_CreateAndFind_TestDatabase");
        }

        [Fact(DisplayName = "Create and Find Customer")]
        public void Customer()
        {
            using(var context = _contextFactory.CreateDbContext())
            {
                context.Customers.Add(new Customer {
                    Name = "Test Customer", 
                    Contact = "kenimich@gmail.com", 
                    ContactType = ContactType.Email 
                    });

                context.SaveChanges();
                var customer = context.Customers.Where(c => c.Name == "Test Customer").FirstOrDefault();
                Assert.Equal("Test Customer", customer?.Name);
                Assert.Equal("kenimich@gmail.com", customer?.Contact);
                Assert.Equal(ContactType.Email, customer?.ContactType);
            }            
        }

        [Fact(DisplayName = "Create and Find Product")]
        public void Product()
        {
            using(var context = _contextFactory.CreateDbContext())
            {
                context.Products.Add(new Product {
                    Name = "Test Product", 
                    CommissionPercentage = 0.1m
                    });

                context.SaveChanges();
                var product = context.Products.Where(product => product.Name == "Test Product").FirstOrDefault();
                Assert.Equal("Test Product", product?.Name);
                Assert.Equal(0.1m, product?.CommissionPercentage);
            }
        }

        [Fact(DisplayName = "Create and Find Salesperson")]
        public void Salesperson()
        {
            using(var context = _contextFactory.CreateDbContext())
            {
                context.Salespersons.Add(new Salesperson {
                    Name = "Test Salesperson", 
                    EmployeeId = "EMP001",
                    });

                context.SaveChanges();
                var salesperson = context.Salespersons.Where(sp => sp.Name == "Test Salesperson").FirstOrDefault();
                Assert.Equal("Test Salesperson", salesperson?.Name);
                Assert.Equal("EMP001", salesperson?.EmployeeId);
            }
        }

        [Fact(DisplayName = "Create and Find Inventory")]
        public void Inventory()
        {
            using(var context = _contextFactory.CreateDbContext())
            {
                context.Products.Add(new Product {
                    Name = "Test Product 2", 
                    CommissionPercentage = 0.2m
                    });

                context.SaveChanges();
                var product = context.Products.Where(product => product.Name == "Test Product 2").FirstOrDefault();
                Assert.Equal("Test Product 2", product?.Name);
                Assert.Equal(0.2m, product?.CommissionPercentage);

                context.Inventories.Add(new Inventory {
                    ProductId = product.Id,
                    Quantity = 10,
                    PurchasePrice = 500.00m,
                    PurchaseDate = new DateTime(2024, 1, 1)
                    });

                context.SaveChanges();
                var inventory = context.Inventories.Find(1);
                Assert.Equal(product.Id, inventory?.ProductId);
                Assert.Equal(10, inventory?.Quantity);
                Assert.Equal(500.00m, inventory?.PurchasePrice);
                Assert.Equal(new DateTime(2024, 1, 1), inventory?.PurchaseDate);
            }
        }

        [Fact(DisplayName = "Create and Find Sale")]
        public void Sale()
        {
            using(var context = _contextFactory.CreateDbContext())
            {
                // First, create necessary related entities
                var product = new Product { Name = "Test Product 3", CommissionPercentage = 0.15m };
                var customer = new Customer { Name = "Test Customer 2", Contact = "kenimich@gmail.com", ContactType = ContactType.Email };
                var salesperson = new Salesperson { Name = "Test Salesperson 2", EmployeeId = "EMP002" };
                context.Products.Add(product);
                context.Customers.Add(customer);
                context.Salespersons.Add(salesperson);
                context.SaveChanges();
                // Now create the sale
                context.Sales.Add(new Sale {
                    ProductId = product.Id,
                    CustomerId = customer.Id,
                    SalesPersonId = salesperson.Id,
                    SaleDate = new DateTime(2024, 2, 1),
                    SalePrice = 1000.00m,
                    Commission = 150.00m
                    }); 
                context.SaveChanges();
                var sale = context.Sales.Where(s => s.SaleDate == new DateTime(2024, 2, 1)).FirstOrDefault();
                Assert.Equal(product.Id, sale?.ProductId);
                Assert.Equal(customer.Id, sale?.CustomerId);
                Assert.Equal(salesperson.Id, sale?.SalesPersonId);
                Assert.Equal(new DateTime(2024, 2, 1), sale?.SaleDate);
                Assert.Equal(1000.00m, sale?.SalePrice);
                Assert.Equal(150.00m, sale?.Commission);
            }
        }
    }
}
