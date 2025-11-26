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

    /// <summary>
/// Creates a new salesperson.
/// </summary>
/// <param name="salesperson">The salesperson to create.</param>
/// <returns>201 Created with the created Salesperson on success, 400 Bad Request on failure.</returns>
    [HttpPost]
    public IActionResult Create([FromBody] Salesperson salesperson)
    {
        return _salespersonService.AddSalesperson(salesperson) > 0 ? new JsonResult(salesperson) : BadRequest();
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