namespace BespokeBikesApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using BespokeBikesApi.Data.Models;
using BespokeBikesApi.Logic;

/// <summary>
/// Controller to create and read sales information.
/// </summary>
[ApiController]
[Route("[controller]")]
public class SaleController : ControllerBase
{
    private readonly ILogger<SaleController> _logger;
    private readonly SaleService _saleService;

    /// <summary>
    /// Default constructor for the Sales Controller class.
    /// </summary>
    /// <param name="logger">A logger implementation for creating error logs.</param>
    /// <param name="saleService">Connection to the business logic for sales information.</param>
    public SaleController(ILogger<SaleController> logger, SaleService saleService)
    {
        _logger = logger;
        _saleService = saleService;
    }

    /// <summary>
    /// Creates a new sales record in the system.
    /// </summary>
    /// <param name="sale">The sale object to be created. Must conform to model validation rules.</param>
    /// <response code="201">Sale created — returns the created Sale object and a Location header pointing to the new resource.</response>
    /// <response code="400">If the provided Sale object is invalid (e.g., failed model validation).</response>
    [HttpPost]
    [Produces("application/json")]
    [ProducesResponseType(typeof(Sale), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public ActionResult<Sale> Create([FromBody] Sale sale)
    {
        if(!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        return _saleService.AddSale(sale) > 0 ? 
            CreatedAtRoute("GetSaleById", new { id = sale.Id }, sale) 
            : BadRequest();
    }

    /// <summary>
    /// Retrieves a specific sales record by its unique identifier.
    /// </summary>
    /// <param name="id">The unique ID of the sale record to retrieve.</param>
    /// <response code="200">Sale found — returns the matching Sale object.</response>
    /// <response code="404">Sale not found with the provided ID.</response>
    [HttpGet("{id}", Name = "GetSaleById")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(Sale), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<Sale> Read(int id)
    {
        var sale = _saleService.GetSaleById(id);
        if(sale == null)
        {
            return NotFound();
        }
        return sale;
    }

    /// <summary>
    /// Retrieves a collection of sales records within a specified date range.
    /// </summary>
    /// <param name="startDate">The starting date for the sales search (inclusive, in ISO 8601 format like YYYY-MM-DD).</param>
    /// <param name="endDate">The ending date for the sales search (inclusive, in ISO 8601 format like YYYY-MM-DD).</param>
    /// <returns>A list of sales that fall within the specified date range.</returns>
    /// <response code="200">Returns the list of sales (may be empty).</response>
    /// <response code="400">If the date range is invalid (e.g., startDate is after endDate).</response>
    [HttpGet("{startDate}/{endDate}")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(IEnumerable<Sale>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public ActionResult<IEnumerable<Sale>> Read(DateTime startDate, DateTime endDate)
    {
        try {
            return _saleService.GetSalesByDateRange(startDate, endDate).ToList();
        }
        catch(ArgumentException ex)
        {
            ModelState.AddModelError(ex.ParamName ?? "DateRange", ex.Message);
            return ValidationProblem(ModelState);
        }
    }
}