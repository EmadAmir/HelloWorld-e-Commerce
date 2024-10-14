using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class StoreContextSeed
    {
        public static async Task SeedAsync(StoreContext _storeContext)
        {
            if (!_storeContext.Products.Any())
            {
                var productData = await File.ReadAllTextAsync("../Infrastructure/Data/SeedData/products.json");

                var products = JsonSerializer.Deserialize<List<Product>>(productData);

                if (products == null) return;

                _storeContext.Products.AddRange(products);

                await _storeContext.SaveChangesAsync();

            }
        }
    }
}
