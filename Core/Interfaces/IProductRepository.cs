using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IProductRepository
    {
        Task<IReadOnlyList<Product>> GetAllProductsAsync( string? brand, string? type, string? sort);
        Task<Product?> GetProductByIdAsync(int id);
        Task<IReadOnlyList<string>> GetBrandAsync();
        Task<IReadOnlyList<string>> GetTypesAsync();
        void AddProduct(Product product);
        void UpdateProduct(Product product);
        void DeleteProduct(Product product); //why is this not int id
        bool CheckIfProductExists(int id);

        Task<bool> SaveChangesAsync();

    }
}
