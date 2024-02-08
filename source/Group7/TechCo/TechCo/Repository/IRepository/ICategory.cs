using TechCo.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TechCo.Repository.IRepository
{
    public interface ICategory
    {
        bool ExistsAsync(Guid? id);
        Task<Category> GetAsync(Guid? id);
        Task<List<Category>> GetAllAsync();
        Task AddAsync(Category category);
        Task UpdateAsync(Category category);
        Task DeleteAsync(Category category);

    }
}
