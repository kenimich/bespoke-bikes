namespace BespokeBikesApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using BespokeBikesApi.Data.Models;
using BespokeBikesApi.Logic;

[ApiController]
[Route("[controller]")]
public class SaleController : ControllerBase
{
    private readonly ILogger<SaleController> _logger;

    public SaleController(ILogger<SaleController> logger)
    {
        _logger = logger;
    }

    [HttpPost]
    public IActionResult Create([FromBody] Sale sale)
    {
        throw new NotImplementedException();
    }

    [HttpGet("{id}")]
    public Sale Read(int id)
    {
        throw new NotImplementedException();
    }
}