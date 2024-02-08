using DXC.BlogConnect.DTO;

namespace DXC.BlogConnect.WebAPI.Repositories.Interfaces
{
    /*
* Created By: Kishore
*/
    public interface ITokenHandlerRepository
    {
        Task<string> GenerateTokenAsync(UserGetDTO userGetDTO);
    }
}
