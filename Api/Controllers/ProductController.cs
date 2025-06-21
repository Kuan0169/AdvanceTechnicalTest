using Microsoft.AspNetCore.Mvc;
using MyCompany.Test.Infrastructure.Models;
using MyCompany.Test.Infrastructure.Services;

namespace MyCompany.Test.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController(IProductService productService) : ControllerBase
{
    private readonly IProductService _productService = productService;

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var products = await _productService.GetAllAsync();
        return Ok(products);
    }

    [HttpGet]
    [Route("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var product = await _productService.GetByIdAsync(id);
        return Ok(product);
    }

    [HttpPost]
    public async Task<IActionResult> Create(ProductModel productModel)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var created = await _productService.CreateAsync(productModel);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut]
    [Route("{id:guid}")]
    public async Task<IActionResult> UpdateAsync(Guid id, ProductModel productModel)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        await _productService.UpdateAsync(id, productModel);
        return NoContent();
    }

    [HttpDelete]
    [Route("{id:guid}")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        await _productService.DeleteAsync(id);
        return NoContent();
    }
}
