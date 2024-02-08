using DXC.BlogConnect.WebAPI.Models.Domain;
using DXC.BlogConnect.DTO;
using System.Linq.Expressions;

namespace DXC.BlogConnect.WebAPI.Repositories.Interfaces
{
    public interface IUserRepository:IGenericRepository<User>
    {
        Task<UserGetDTO> GetUserByUserName(string userName);
        bool IsUserExistByUsername(string userName);

        Task<List<UserGetDTO>> GetAllUserWithUserName();

    }
}
