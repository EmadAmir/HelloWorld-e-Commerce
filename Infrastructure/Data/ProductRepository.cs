using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class ProductRepository(StoreContext _context) : IProductRepository
    {
        public void AddProduct(Product product)
        {
            _context.Products.Add(product);
        }

        public bool CheckIfProductExists(int id)
        {
           return _context.Products.Any(p => p.Id == id);
        }

        public void DeleteProduct(Product product)
        {
            _context.Products.Remove(product);
        }

        public async Task<IReadOnlyList<Product>> GetAllProductsAsync(string? brand, string? type, string? sort)
        {
            var query = _context.Products.AsQueryable();

            if (!string.IsNullOrWhiteSpace(brand)) 
            { 
                query = query.Where(p => p.Brand == brand);
            }

            if (!string.IsNullOrWhiteSpace(type))
            {
                query = query.Where(p => p.Type == type);
            }

            query = sort switch
            {
                "priceAsc" => query.OrderBy(p => p.Price),
                "priceDesc" => query.OrderByDescending(p => p.Price),
                _ => query.OrderBy(p => p.Name)
            };
            return await query.ToListAsync();
            //return await query.Skip(5).Take(5).ToListAsync();
        }

        public async Task<IReadOnlyList<string>> GetBrandAsync()
        {
            return await _context.Products.Select(x => x.Brand)
                .Distinct()
                .ToListAsync();
        }

        public async Task<Product?> GetProductByIdAsync(int id)
        {
            return await _context.Products.FindAsync(id);
        }

        public async Task<IReadOnlyList<string>> GetTypesAsync()
        {
            return await _context.Products.Select(x => x.Type)
                .Distinct()
                .ToListAsync();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public void UpdateProduct(Product product)
        {
            _context.Entry(product).State = EntityState.Modified;
        }
    }
}
