using DXC.BlogConnect.DTO;
using Microsoft.AspNetCore.Mvc;
/*
 * Created By: Kishore
 */
namespace DXC.BlogConnect.WebApp.Services.Interfaces
{
    public interface IUserService
    {
        Task<APIResponse<UserGetDTO>> GetAllUserAsync();
        Task<APIResponse<UserDTO>> AddUserAsync(UserDTO userDTO);
        Task<APIResponse<UserDTO>> GetUserById(int userId);
        Task<APIResponse<UserDTO>> UpdateUser(UserDTO userDTO);
        Task<APIResponse<UserEditDTO>> GetUserByUserId(int id);
    }
}
