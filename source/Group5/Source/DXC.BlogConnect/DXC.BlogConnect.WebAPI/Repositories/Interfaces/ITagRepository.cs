using DXC.BlogConnect.WebAPI.Models.Domain;

namespace DXC.BlogConnect.WebAPI.Repositories.Interfaces
{
    /*Created by Prabu Elavarasan*/
    public interface ITagRepository
    {
        Task<IEnumerable<Tag>> GetAllAsync();
    }
}
