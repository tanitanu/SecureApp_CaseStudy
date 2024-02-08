using ElectronicStoreAPI.Data;
using ElectronicStoreAPI.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace ElectronicStoreAPI.Repositories
{
    //by Srinivasan
    public class SQLCategoryRepository : ICategoryRepository

    {
        private readonly ElecStoreDBContext dbContext;

        public SQLCategoryRepository(ElecStoreDBContext dbContext)
        {
            this.dbContext = dbContext;
        }


        public async Task<List<Category>> GetAll()
        {
            return  await dbContext.CategoryMstr.ToListAsync();
            //throw new NotImplementedException();
        }
    }
}
