namespace BespokeBikesApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using BespokeBikesApi.Data.Models;
using BespokeBikesApi.Logic;
using BespokeBikesApi.Logic.DTO;

/// <summary>
/// Controller used to Create, Read, and Update the Product and Inventory. 
/// </summary>
[ApiController]
[Route("[controller]")]
public class ProductController : ControllerBase
{
    private readonly ILogger<ProductController> _logger;
    private readonly ProductService _productService;

    /// <summary>
    /// Default constructor for the Product Controller class.
    /// </summary>
    /// <param name="logger">A logger implementation for creating error logs.</param>
    /// <param name="productService">Connection to the business logic for the Product and Inventory classes.</param>
    public ProductController(ILogger<ProductController> logger, ProductService productService)
    {
        _logger = logger;
        _productService = productService;
    }

    /// <summary>
    /// Creates a new product and initial inventory entry.
    /// </summary>
    /// <param name="product">Product + initial inventory to create.</param>
    /// <response code="201">Product created — returns the created ProductCurrentInventory with Location header.</response>
    /// <response code="400">Bad request — validation errors or creation failed. Validation responses use ValidationProblemDetails.</response>
    [HttpPost]
    [Produces("application/json")]
    [ProducesResponseType(typeof(ProductCurrentInventory), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public ActionResult<ProductCurrentInventory> Create([FromBody] ProductNewInventory product)
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

    /// <summary>
    /// Adds new inventory to an existing product.
    /// </summary>
    /// <param name="product">The product inventory data to add. This includes the ProductId and the Quantity, PurchasePrice, and PurchaseDate to add.</param>
    /// <response code="200">Returns the updated current inventory state of the product.</response>
    /// <response code="400">Bad request — model state validation errors (e.g., required fields missing).</response>
    [HttpPatch]
    [Produces("application/json")]
    [ProducesResponseType(typeof(ProductCurrentInventory), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
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

    /// <summary>
    /// Retrieves the current product + inventory details for a product by its unique ID.
    /// </summary>
    /// <param name="id">The unique integer ID of the product to retrieve.</param>
    /// <response code="200">Product found — returns the current inventory state.</response>
    /// <response code="404">Product not found with the given ID.</response>
    [HttpGet("{id}", Name = "GetProductById")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(ProductCurrentInventory), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public ActionResult<ProductCurrentInventory> Read(int id)
    {
        var product = _productService.GetProductById(id);
        if(product == null)
        {
            return NotFound();
        }
        return product;
    }

    /// <summary>
    /// Updates an existing product resource entirely, leaving inventory unchanged.
    /// </summary>
    /// <param name="product">The complete product object to update (Id required).</param>
    /// <response code="200">Product updated successfully.</response>
    /// <response code="400">Bad request — model state validation errors</response>
    [HttpPut]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
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