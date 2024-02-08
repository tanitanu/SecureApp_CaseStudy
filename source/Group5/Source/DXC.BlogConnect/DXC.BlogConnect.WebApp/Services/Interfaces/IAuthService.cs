using DXC.BlogConnect.DTO;
using Microsoft.AspNetCore.Mvc;

namespace DXC.BlogConnect.WebApp.Services.Interfaces
{/*
 * Created By: Kishore
 */
    public interface IAuthService
    {
        Task<APIResponse<string>> LoginAsync(UserLoginDTO userLoginDTO);
        bool IsCurrentUserInRole(string role);
        bool IsLogin(string userName);

    }
}
