namespace BespokeBikesApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using BespokeBikesApi.Data.Models;
using BespokeBikesApi.Logic;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

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
        if(!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            return _customerService.AddCustomer(customer) > 0 ? 
                CreatedAtRoute("GetCustomerById", new { id = customer.Id }, customer) 
                : BadRequest();
        }
        catch(ArgumentException ex)
        {
            ModelState.AddModelError(ex.ParamName ?? nameof(Customer), ex.Message);
            return ValidationProblem(ModelState);
        }
    }

    [HttpGet("{id}", Name = "GetCustomerById")]
    public ActionResult<Customer> Read(int id)
    {
        return _customerService.GetCustomerById(id);
    }

    [HttpPut]
    public IActionResult Update([FromBody] Customer customer)
    {
        if(!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try {
            return _customerService.UpdateCustomer(customer) ? Ok() : BadRequest();
        }
        catch(ArgumentException ex)
        {
            ModelState.AddModelError(ex.ParamName ?? nameof(Customer), ex.Message);
            return ValidationProblem(ModelState);
        }
    }
}