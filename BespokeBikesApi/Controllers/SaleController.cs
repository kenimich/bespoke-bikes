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
        return _saleService.AddSale(sale) > 0 ? new JsonResult(sale) : BadRequest();
    }

    [HttpGet("{id}")]
    public Sale Read(int id)
    {
        return _saleService.GetSaleById(id);
    }
}