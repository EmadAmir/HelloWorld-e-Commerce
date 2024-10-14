using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HelloWorldWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController(IProductRepository _repository) : ControllerBase
    {

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Product>>> GetProducts(string? brand, string? type, string? sort)
        {
            return Ok(await _repository.GetAllProductsAsync(brand,type, sort));
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<Product>> GetProductById(int id)
        {
            var product = await _repository.GetProductByIdAsync(id);

            if (product == null)
            {
                return NotFound();
            }
            return product;
        }

        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<Product>>> GetProductBrands() 
        {
            return Ok(await _repository.GetBrandAsync());
        }

        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<Product>>> GetProductTypes()
        {
            return Ok(await _repository.GetTypesAsync());
        }

        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct(Product product)
        {
            _repository.AddProduct(product);

            if (await _repository.SaveChangesAsync())
            {
                return CreatedAtAction("GetProduct", new { id = product.Id }, product);
            }

            return BadRequest("Problem Creating product");
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<Product>> UpdateProduct(int id, Product product)
        {

            if (product.Id != id || !_repository.CheckIfProductExists(id))
            {
                return BadRequest("Product not found or does not exist");
            }

            _repository.UpdateProduct(product);

            if (await _repository.SaveChangesAsync())
            {
                return NoContent();
            }

            return BadRequest("Problem updating the Products");



        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            var product = await _repository.GetProductByIdAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            _repository.DeleteProduct(product);

            if (await _repository.SaveChangesAsync())
            {
                return NoContent();
            }

            return BadRequest("Problem deleting the Product");
        }


    }
}
