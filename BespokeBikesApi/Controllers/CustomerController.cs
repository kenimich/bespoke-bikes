namespace BespokeBikesApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using BespokeBikesApi.Data.Models;
using BespokeBikesApi.Logic;

[ApiController]
[Route("[controller]")]
public class CustomerController : ControllerBase
{
    private readonly ILogger<CustomerController> _logger;
    private readonly CustomerService _customerService;

    public CustomerController(ILogger<CustomerController> logger, CustomerService customerService)
    {
        _logger = logger;
        _customerService = customerService;
    }

    [HttpPost]
    public IActionResult Create([FromBody] Customer customer)
    {
        return _customerService.AddCustomer(customer) > 0 ? Ok(customer.Id) : BadRequest();
    }

    [HttpGet("{id}")]
    public Customer Read(int id)
    {
        return _customerService.GetCustomerById(id);
    }

    [HttpPut]
    public IActionResult Update([FromBody] Customer customer)
    {
        return _customerService.UpdateCustomer(customer) ? Ok() : BadRequest();
    }
}