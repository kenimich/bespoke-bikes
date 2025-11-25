namespace BespokeBikesApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using BespokeBikesApi.Logic;
using BespokeBikesApi.Logic.DTO;

[ApiController]
[Route("[controller]")]
public class ReportController : ControllerBase
{
    private readonly ILogger<ReportController> _logger;

    public ReportController(ILogger<ReportController> logger)
    {
        _logger = logger;
    }

    [HttpGet("quarterly")]
    public QuarterlyReport Read()
    {
        throw new NotImplementedException();
    }
}