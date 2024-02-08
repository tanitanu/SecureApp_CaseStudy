using TechCo.Data;
using TechCo.Models;
using TechCo.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace TechCo.Repository
{
    public class OrderRepository : IOrder
    {
        private readonly ApplicationDbContext _dbContext;

        public OrderRepository(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }
        public async Task AddAsync(Order order)
        {
            await _dbContext.Order.AddAsync(order);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Order order)
        {
            _dbContext.Order.Remove(order);
            await _dbContext.SaveChangesAsync();
        }

        public bool ExistsAsync(Guid? id)
        {
            return _dbContext.Order.Any(e => e.ID == id);
        }

        public async Task<List<Order>> GetAllAsync()
        {
            return await _dbContext.Order.Include(o => o.Client).ToListAsync();
        }

        public async Task<Order> GetAsync(Guid? id)
        {
            return await _dbContext.Order.Include(o => o.Client).FirstOrDefaultAsync(p => p.ID == id);
        }

        public async Task UpdateAsync(Order order)
        {
            _dbContext.Attach(order).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }
    }
}
