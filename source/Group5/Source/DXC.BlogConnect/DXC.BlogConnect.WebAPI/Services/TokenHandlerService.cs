using DXC.BlogConnect.WebAPI.Models.Domain;
using DXC.BlogConnect.DTO;
using DXC.BlogConnect.WebAPI.Repositories;
using DXC.BlogConnect.WebAPI.Repositories.Interfaces;
using DXC.BlogConnect.WebAPI.Services.Interfaces;

namespace DXC.BlogConnect.WebAPI.Services
{
    /*
* Created By: Kishore
*/
    public class TokenHandlerService : ITokenHandlerService
    {
        public ITokenHandlerRepository _handlerRepository;
        public TokenHandlerService(ITokenHandlerRepository handlerRepository)
        {
            _handlerRepository = handlerRepository;
        }
        public async Task<string> GetTokenAsync(UserGetDTO userGetDTO)
        {
            var token = await _handlerRepository.GenerateTokenAsync(userGetDTO);
            return token;
        }
    }
}
