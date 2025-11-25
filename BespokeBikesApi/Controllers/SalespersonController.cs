namespace BespokeBikesApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using BespokeBikesApi.Data.Models;

[ApiController]
[Route("[controller]")]
public class SalespersonController : ControllerBase
{
    private readonly ILogger<SalespersonController> _logger;

    public SalespersonController(ILogger<SalespersonController> logger)
    {
        _logger = logger;
    }

    [HttpPost]
    public IActionResult Create()
    {
        throw new NotImplementedException();
    }

    [HttpGet]
    public Salesperson Read()
    {
        throw new NotImplementedException();
    }

    [HttpPatch]
    public IActionResult Update()
    {
        throw new NotImplementedException();
    }
}