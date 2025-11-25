namespace BespokeBikesApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using BespokeBikesApi.Data.Models;

[ApiController]
[Route("[controller]")]
public class ProductController : ControllerBase
{
    private readonly ILogger<ProductController> _logger;

    public ProductController(ILogger<ProductController> logger)
    {
        _logger = logger;
    }

    [HttpPost]
    public IActionResult Create()
    {
        throw new NotImplementedException();
    }

    [HttpGet]
    public Product Read()
    {
        throw new NotImplementedException();
    }

    [HttpPatch]
    public IActionResult Update()
    {
        throw new NotImplementedException();
    }
}