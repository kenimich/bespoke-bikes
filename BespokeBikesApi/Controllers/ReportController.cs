namespace BespokeBikesApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using BespokeBikesApi.Logic.Reports;
using BespokeBikesApi.Logic.DTO;

/// <summary>
/// Controller used to pull financial reports.
/// </summary>
[ApiController]
[Route("[controller]")]
public class ReportController : ControllerBase
{
    private readonly ILogger<ReportController> _logger;
    private readonly QuarterlyReportService _quarterlyReportService;

    /// <summary>
    /// Default constructer for the Report Controller class. 
    /// </summary>
    /// <param name="logger">A logger implementation for creating error logs.</param>
    /// <param name="quarterlyReportService">Connection to the business logic for reports.</param>
    public ReportController(ILogger<ReportController> logger, QuarterlyReportService quarterlyReportService)
    {
        _logger = logger;
        _quarterlyReportService = quarterlyReportService;
    }

    /// <summary>
    /// Retrieves a total commissions for a salesperson per quarter.
    /// </summary>
    /// <param name="salespersonId">The unique identifier of the salesperson.</param>
    /// <param name="year">The year for the report (e.g., 2025). Must be between 0 and the current year.</param>
    /// <param name="quarter">The quarter for the report (1, 2, 3, or 4).</param>
    /// <returns>An action result containing the quarterly report or a validation problem.</returns>
    /// <response code="200">Returns the requested quarterly report.</response>
    /// <response code="400">If any of the parameters are invalid (e.g., an invalid quarter or non-existent salesperson ID).</response>
    [HttpGet("quarterly/{salespersonId}/{year}/{quarter}")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(QuarterlyReport), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public ActionResult<QuarterlyReport> Read(int salespersonId, int year, int quarter)
    {
        try {
            return _quarterlyReportService.GetQuarterlyReport(salespersonId, year, quarter);
        }
        catch(ArgumentException ex)
        {
            ModelState.AddModelError(ex.ParamName ?? "ReportParameters", ex.Message);
            return ValidationProblem(ModelState);
        }
    }
}