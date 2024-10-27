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
    public class GenericRepository<T>(StoreContext _storeContext) : IGenericRepository<T> where T : BaseEntity
    {
        public void Add(T entity)
        {
            _storeContext.Set<T>().Add(entity);
        }

        public bool Exists(int id)
        {
           return _storeContext.Set<T>().Any(x => x.Id == id);
        }

        public async Task<T?> GetByIdAsync(int id)
        {
           return await _storeContext.Set<T>().FindAsync(id);
        }

        public async Task<IReadOnlyList<T>> ListAllAsync()
        {
            return await _storeContext.Set<T>().ToListAsync();
        }

        public void Remove(T entity)
        {
            _storeContext.Set<T>().Remove(entity);
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _storeContext.SaveChangesAsync() > 0;
        }

        public void Update(T entity)
        {
            _storeContext.Set<T>().Attach(entity);
        }
    }
}
