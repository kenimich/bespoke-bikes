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

    /// <summary>
    /// Creates a new customer.
    /// </summary>
    /// <param name="customer">Customer to create.</param>
    /// <response code="201">Customer created — returns the created Customer with Location header.</response>
    /// <response code="400">Bad request, validation or creation failed.</response>
    [HttpPost]
    [Produces("application/json")]
    [ProducesResponseType(typeof(Customer), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<Customer> Create([FromBody] Customer customer)
    {
        return _customerService.AddCustomer(customer) > 0 ? CreatedAtRoute("GetCustomerById", new { id = customer.Id }, customer) : BadRequest();
    }

    [HttpGet("{id}", Name = "GetCustomerById")]
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