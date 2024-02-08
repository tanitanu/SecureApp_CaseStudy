using DXC.BlogConnect.WebAPI.Models.Domain;
using DXC.BlogConnect.DTO;
using System.Linq.Expressions;
using System.Threading.Tasks;
/*
* Created By: Kishore
*/
namespace DXC.BlogConnect.WebAPI.Services.Interfaces
{
    //user service - which we inject and consume inside the user controller

    public interface IUserService
    {
        Task<IEnumerable<User>>GetAllUserAsync();
        Task AddUserAsync(User entity);
        void UpdateUser(User entity);
        Task<bool> DeleteUser(int userId);
        Task<UserGetDTO> GetUserByUserName(string userName);
        Task<User> GetUserById(int userId);
        bool IsUserExistByUsername(string userName);
        Task<List<User>> GetUserListById(int userId);
        Task<List<UserGetDTO>> GetAllUserWithUserName();

    }
}
