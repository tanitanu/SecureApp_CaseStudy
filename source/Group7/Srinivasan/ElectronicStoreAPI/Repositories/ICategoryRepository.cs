using ElectronicStoreAPI.Models.Domain;

namespace ElectronicStoreAPI.Repositories
{
    //by Srinivasan
    public interface ICategoryRepository
    {
        Task<List<Category>> GetAll();
    }
}
