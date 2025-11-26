namespace BespokeBikesApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using BespokeBikesApi.Data.Models;
using BespokeBikesApi.Logic;
using BespokeBikesApi.Logic.DTO;

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
    public IActionResult Create([FromBody] ProductNewInventory product)
    {
        if(!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try {
            var productCurrentInventory = _productService.AddProduct(product);
            return productCurrentInventory.Id > 0 ? 
                CreatedAtRoute("GetProductById", new { id = productCurrentInventory.Id }, productCurrentInventory) 
                : BadRequest();
        }
        catch(ArgumentException ex)
        {
            ModelState.AddModelError(ex.ParamName ?? nameof(Product), ex.Message);
            return ValidationProblem(ModelState);
        }
    }

    [HttpPatch]
    public ActionResult<ProductCurrentInventory> CreateInventory([FromBody] ProductNewInventory product)
    {
        if(!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        try {
            return _productService.AddInventory(product);
        }
        catch(ArgumentException ex)
        {
            ModelState.AddModelError(ex.ParamName ?? nameof(Product), ex.Message);
            return ValidationProblem(ModelState);
        }
    }

    [HttpGet("{id}", Name = "GetProductById")]
    public ActionResult<ProductCurrentInventory> Read(int id)
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