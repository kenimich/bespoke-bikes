namespace BespokeBikesApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using BespokeBikesApi.Data.Models;
using BespokeBikesApi.Logic;

[ApiController]
[Route("[controller]")]
public class ProductController : ControllerBase
{
    private readonly ILogger<ProductController> _logger;
    private readonly ProductService _productService;

    public ProductController(ILogger<ProductController> logger, ProductService productService)
    {
        _logger = logger;
        _productService = productService;
    }

    [HttpPost]
    public IActionResult Create([FromBody] Product product)
    {
        if(!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try {
            return _productService.AddProduct(product) > 0 ? 
                CreatedAtRoute("GetProductById", new { id = product.Id }, product) 
                : BadRequest();
        }
        catch(ArgumentException ex)
        {
            ModelState.AddModelError(ex.ParamName ?? nameof(Product), ex.Message);
            return ValidationProblem(ModelState);
        }
    }

    [HttpGet("{id}", Name = "GetProductById")]
    public ActionResult<Product> Read(int id)
    {
        return _productService.GetProductById(id);
    }

    [HttpPut]
    public IActionResult Update([FromBody] Product product)
    {
        if(!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        try {
            return _productService.UpdateProduct(product) ? Ok() : BadRequest();
        }
        catch(ArgumentException ex)
        {
            ModelState.AddModelError(ex.ParamName ?? nameof(Product), ex.Message);
            return ValidationProblem(ModelState);
        }
    }
}