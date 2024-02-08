using TechCo.Data;
using TechCo.Models;
using TechCo.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TechCo.Repository
{
    public class CategoryRepository : ICategory
    {
        private readonly ApplicationDbContext _dbContext;

        public CategoryRepository(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }
        public async Task<List<Category>> GetAllAsync()
        {
            return await _dbContext.Category.ToListAsync();
        }
        public async Task AddAsync(Category category)
        {
            await _dbContext.Category.AddAsync(category);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Category category)
        {
            _dbContext.Category.Remove(category);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Category category)
        {
            _dbContext.Attach(category).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Category> GetAsync(Guid? id)
        {
            return await _dbContext.Category.FirstOrDefaultAsync(c => c.ID == id);
        }

        public bool ExistsAsync(Guid? id)
        {
            return _dbContext.Category.Any(e => e.ID == id);
        }
    }
}
