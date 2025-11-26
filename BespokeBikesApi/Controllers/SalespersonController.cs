namespace BespokeBikesApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using BespokeBikesApi.Data.Models;
using BespokeBikesApi.Logic;

[ApiController]
[Route("[controller]")]
public class SalespersonController : ControllerBase
{
    private readonly ILogger<SalespersonController> _logger;
    private readonly SalespersonService _salespersonService;

    public SalespersonController(ILogger<SalespersonController> logger, SalespersonService salespersonService)
    {
        _logger = logger;
        _salespersonService = salespersonService;
    }

    [HttpPost]
    public IActionResult Create([FromBody] Salesperson salesperson)
    {
        return _salespersonService.AddSalesperson(salesperson) > 0 ? Ok(salesperson.Id) : BadRequest();
    }

    [HttpGet("{id}")]
    public Salesperson Read(int id)
    {
        return _salespersonService.GetSalespersonById(id);
    }

    [HttpPut]
    public IActionResult Update([FromBody] Salesperson salesperson)
    {
        return _salespersonService.UpdateSalesperson(salesperson) ? Ok() : BadRequest();
    }
}