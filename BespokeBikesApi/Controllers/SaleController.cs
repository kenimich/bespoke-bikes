namespace BespokeBikesApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using BespokeBikesApi.Data.Models;
using BespokeBikesApi.Logic;

[ApiController]
[Route("[controller]")]
public class SaleController : ControllerBase
{
    private readonly ILogger<SaleController> _logger;
    private readonly SaleService _saleService;

    public SaleController(ILogger<SaleController> logger, SaleService saleService)
    {
        _logger = logger;
        _saleService = saleService;
    }

    [HttpPost]
    public IActionResult Create([FromBody] Sale sale)
    {
        if(!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        return _saleService.AddSale(sale) > 0 ? 
            CreatedAtRoute("GetSaleById", new { id = sale.Id }, sale) 
            : BadRequest();
    }

    [HttpGet("{id}", Name = "GetSaleById")]
    public ActionResult<Sale> Read(int id)
    {
        return _saleService.GetSaleById(id);
    }

    [HttpGet("{startDate}/{endDate}")]
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