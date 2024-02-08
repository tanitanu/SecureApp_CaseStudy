using TechCo.Data;
using TechCo.Models;
using TechCo.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TechCo.Repository
{
    public class ProductRepository : IProduct
    {
        private readonly ApplicationDbContext _dbContext;

        public ProductRepository(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }
        public async Task AddAsync(Product product)
        {
            await _dbContext.Product.AddAsync(product);
            await _dbContext.SaveChangesAsync();

        }

        public async Task DeleteAsync(Product product)
        {
            _dbContext.Product.Remove(product);
            await _dbContext.SaveChangesAsync();
        }

        public bool ExistsAsync(Guid? id)
        {
            return _dbContext.Product.Any(e => e.ID == id);
        }

        public async Task<List<Product>> GetAllAsync()
        {
            //return await _dbContext.Product.Include(p => p.Category).ToListAsync();
            return await _dbContext.Product.ToListAsync();
        }

        public async Task<Product> GetAsync(Guid? id)
        {
            return await _dbContext.Product.Include(p => p.Category).FirstOrDefaultAsync(p => p.ID == id);
        }

        public async Task UpdateAsync(Product product)
        {
            _dbContext.Attach(product).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }
    }
}
