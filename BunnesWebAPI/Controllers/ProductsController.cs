using Core.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BunnesWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly StoreContext _storeContext;
        public ProductsController(StoreContext context)
        {
            this._storeContext = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            return await _storeContext.Products.ToListAsync();
            //var products =  await _storeContext.Products.ToListAsync();

            //if (products == null) 
            //{ 
            //	return NotFound();
            //}
            //return Ok(products);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<Product>> GetProducts(int id)
        {
            var product = await _storeContext.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }
            return product;
        }


        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct(Product product)
        {
            _storeContext.Products.Add(product);

            await _storeContext.SaveChangesAsync();

            return product;
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<Product>> UpdateProduct(int id, Product product)
        {
            if (product.Id == id || !checkProductExists(id))
            {
                return BadRequest("Product not found or does not exist");
            }

            _storeContext.Entry(product).State = EntityState.Modified;

            await _storeContext.SaveChangesAsync();

            return NoContent();
                
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteProduct(int id)
        { 
            var product = _storeContext.Products.Find(id);

            if (product == null)
            {
                return NotFound();
            }
            _storeContext.Products.Remove(product);

            await _storeContext.SaveChangesAsync();

            return NoContent();
        }


        private bool checkProductExists(int id) 
        { 
            return _storeContext.Products.Any(x => x.Id == id);
        }
    }
}
