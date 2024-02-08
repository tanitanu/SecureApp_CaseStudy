using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using TechCo.Models;

namespace TechCo.Repository.IRepository
{
    public interface IOrder
    {
        bool ExistsAsync(Guid? id);
        Task<Order> GetAsync(Guid? id);
        Task<List<Order>> GetAllAsync();
        Task AddAsync(Order order);
        Task UpdateAsync(Order order);
        Task DeleteAsync(Order order);
    }
}
