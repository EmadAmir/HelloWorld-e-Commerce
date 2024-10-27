using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HelloWorldWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController(IGenericRepository<Product> _repo) : ControllerBase
    {

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Product>>> GetProducts(string? brand, string? type, string? sort)
        {
            return Ok(await _repo.ListAllAsync());
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<Product>> GetProductById(int id)
        {
            var product = await  _repo.GetByIdAsync(id);

            if (product == null)
            {
                return NotFound();
            }
            return product;
        }

        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<Product>>> GetProductBrands() 
        {
            // Todo: needs implementation

            return Ok();
        }

        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<Product>>> GetProductTypes()
        {
            // Todo: needs implementation

            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct(Product product)
        {
            _repo.Add(product);

            if (await _repo.SaveAllAsync())
            {
                return CreatedAtAction("GetProduct", new { id = product.Id }, product);
            }

            return BadRequest("Problem Creating product");
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<Product>> UpdateProduct(int id, Product product)
        {

            if (product.Id != id || !_repo.Exists(id))
            {
                return BadRequest("Product not found or does not exist");
            }

            _repo.Update(product);

            if (await _repo.SaveAllAsync())
            {
                return NoContent();
            }

            return BadRequest("Problem updating the Products");



        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            var product = await _repo.GetByIdAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            _repo.Remove(product);

            if (await _repo.SaveAllAsync())
            {
                return NoContent();
            }

            return BadRequest("Problem deleting the Product");
        }




    }
}
