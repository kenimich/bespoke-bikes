namespace BespokeBikesApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using BespokeBikesApi.Data.Models;
using BespokeBikesApi.Logic;

/// <summary>
/// Controller to create, read, and update salesperson information.
/// </summary>
[ApiController]
[Route("[controller]")]
public class SalespersonController : ControllerBase
{
    private readonly ILogger<SalespersonController> _logger;
    private readonly SalespersonService _salespersonService;
    
    /// <summary>
    /// Default constructor for the Salesperson Controller class.
    /// </summary>
    /// <param name="logger">A logger implementation for creating error logs.</param>
    /// <param name="salespersonService">Connection to the business logic for salespersons.</param>
    public SalespersonController(ILogger<SalespersonController> logger, SalespersonService salespersonService)
    {
        _logger = logger;
        _salespersonService = salespersonService;
    }

    /// <summary>
    /// Creates a new salesperson record in the system.
    /// </summary>
    /// <param name="salesperson">The salesperson object to be created, provided in the request body.</param>
    /// <response code="201">Salesperson created — returns the new Salesperson object and a Location header.</response>
    /// <response code="400">If the provided Salesperson object is invalid (e.g., failed model validation or business rules).</response>
    [HttpPost]
    [Produces("application/json")]
    [ProducesResponseType(typeof(Salesperson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public IActionResult Create([FromBody] Salesperson salesperson)
    {
        if(!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        try {
            return _salespersonService.AddSalesperson(salesperson) > 0 ? 
                CreatedAtRoute("GetSalespersonById", new { id = salesperson.Id }, salesperson) 
                : BadRequest();
        }
        catch(ArgumentException ex)
        {
            ModelState.AddModelError(ex.ParamName ?? nameof(Salesperson), ex.Message);
            return ValidationProblem(ModelState);
        }
    }

    /// <summary>
    /// Retrieves a specific salesperson record by their unique identifier.
    /// </summary>
    /// <param name="id">The unique ID of the salesperson record to retrieve.</param>
    /// <response code="200">Salesperson found — returns the matching Salesperson object.</response>
    /// <response code="404">Salesperson not found with the provided ID.</response>
    [HttpGet("{id}", Name = "GetSalespersonById")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(Salesperson), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<Salesperson> Read(int id)
    {
        var salesperson = _salespersonService.GetSalespersonById(id);
        if(salesperson == null)
        {
            return NotFound();
        }
        return salesperson;
    }

    /// <summary>
    /// Fully updates an existing salesperson record.
    /// </summary>
    /// <param name="salesperson">The salesperson object containing the new data. The ID must be valid.</param>
    /// <response code="200">Salesperson updated successfully.</response>
    /// <response code="400">If the provided Salesperson object is invalid (failed validation or business rules).</response>
    [HttpPut]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)] // Success, returning 200 OK (no body, or an empty body)
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public IActionResult Update([FromBody] Salesperson salesperson)
    {
        if(!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        try {
            return _salespersonService.UpdateSalesperson(salesperson) ? Ok() : BadRequest();
        }
        catch(ArgumentException ex)
        {
            ModelState.AddModelError(ex.ParamName ?? nameof(Salesperson), ex.Message);
            return ValidationProblem(ModelState);
        }
    }
}