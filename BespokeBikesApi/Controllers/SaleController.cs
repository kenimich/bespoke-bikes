namespace BespokeBikesApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using BespokeBikesApi.Data.Models;

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
    public IActionResult Create()
    {
        throw new NotImplementedException();
    }

    [HttpGet]
    public Sale Read()
    {
        throw new NotImplementedException();
    }
}