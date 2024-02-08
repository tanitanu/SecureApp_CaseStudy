using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using TechCo.Models;

namespace TechCo.Repository.IRepository
{
    public interface IProduct
    {
        bool ExistsAsync(Guid? id);
        Task<Product> GetAsync(Guid? id);
        Task<List<Product>> GetAllAsync();
        Task AddAsync(Product product);
        Task UpdateAsync(Product product);
        Task DeleteAsync(Product product);
    }
}
