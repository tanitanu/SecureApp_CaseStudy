using DXC.BlogConnect.WebAPI.Models.Domain;
using DXC.BlogConnect.DTO;

namespace DXC.BlogConnect.WebAPI.Services.Interfaces
{
    /*
* Created By: Kishore
*/
    public interface ITokenHandlerService
    {
        Task<string> GetTokenAsync(UserGetDTO userGetDTO);
    }
   
}
