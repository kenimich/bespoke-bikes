namespace BespokeBikesApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using BespokeBikesApi.Data.Models;
using BespokeBikesApi.Logic;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

/// <summary>
/// The Controller used to create, read, and update customers. 
/// </summary>
[ApiController]
[Route("[controller]")]
public class CustomerController : ControllerBase
{
    private readonly ILogger<CustomerController> _logger;
    private readonly ICustomerService _customerService;

    /// <summary>
    /// Default constructor for the CustomerController class.
    /// </summary>
    /// <param name="logger">A logger implementation for creating error logs.</param>
    /// <param name="customerService">Connection to the business logic for the Customer.</param>
    public CustomerController(ILogger<CustomerController> logger, ICustomerService customerService)
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
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
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

    /// <summary>
    /// Gets a customer by id.
    /// </summary>
    /// <param name="id">The id of the customer to retrieve.</param>
    /// <response code="200">Customer found — returns the matching Customer.</response>
    /// <response code="404">Customer not found.</response>
    [HttpGet("{id}", Name = "GetCustomerById")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(Customer), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public ActionResult<Customer> Read(int id)
    {
        var customer = _customerService.GetCustomerById(id);
        if(customer == null)
        {
            return NotFound();
        }
        return customer;
    }

    /// <summary>
    /// Updates an existing customer.
    /// </summary>
    /// <param name="customer">Customer to update (Id required).</param>
    /// <response code="200">Customer updated successfully.</response>
    /// <response code="400">Bad request — validation errors or update failed. Validation responses use ValidationProblemDetails.</response>
    [HttpPut]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
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