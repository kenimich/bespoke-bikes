namespace BespokeBikesApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using BespokeBikesApi.Logic.Reports;
using BespokeBikesApi.Logic.DTO;

[ApiController]
[Route("[controller]")]
public class ReportController : ControllerBase
{
    private readonly ILogger<ReportController> _logger;
    private readonly QuarterlyReportService _quarterlyReportService;

    public ReportController(ILogger<ReportController> logger, QuarterlyReportService quarterlyReportService)
    {
        _logger = logger;
        _quarterlyReportService = quarterlyReportService;
    }

    [HttpGet("quarterly/{salespersonId}/{year}/{quarter}")]
    public QuarterlyReport Read(int salespersonId, int year, int quarter)
    {
        return _quarterlyReportService.GetQuarterlyReport(salespersonId, year, quarter);
    }
}