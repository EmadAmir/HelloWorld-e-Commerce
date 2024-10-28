using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using HelloWorldWebAPI.RequestHelpers;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HelloWorldWebAPI.Controllers
{

    public class ProductsController(IGenericRepository<Product> _repo) : BaseApiController
    {

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Product>>> GetProducts(
            [FromQuery]ProductSpecParams productSpecParams)
        {
             var spec = new ProductSpecification(productSpecParams);

            return await CreatePagedResult<Product>(_repo, spec, productSpecParams.PageIndex,productSpecParams.PageSize);
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
            var spec = new BrandListSpecification();

            return Ok(await _repo.ListAsync(spec));
        }

        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<Product>>> GetProductTypes()
        {
            var spec = new TypeListSpecification();

            return Ok(await _repo.ListAsync(spec));
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

            if (product.Id != id || !ProductExists(id))
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

        private bool ProductExists(int id) 
        { 
            return _repo.Exists(id);
        }


    }
}
