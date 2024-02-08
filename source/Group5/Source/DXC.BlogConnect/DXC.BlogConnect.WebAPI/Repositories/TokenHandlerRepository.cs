using DXC.BlogConnect.WebAPI.Models.Domain;
using DXC.BlogConnect.DTO;
using DXC.BlogConnect.WebAPI.Repositories.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DXC.BlogConnect.WebAPI.Repositories
{
    /*
* Created By: Kishore
*/
    public class TokenHandlerRepository : ITokenHandlerRepository
    {
        private readonly IConfiguration _configuration;

        public TokenHandlerRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Task<string> GenerateTokenAsync(UserGetDTO userGetDTO)
        {
            var roles = string.Format("{0}", userGetDTO.RoleName);

            var claims = new[]
            {
                new Claim("UserId", Convert.ToString(userGetDTO.Id)),
                new Claim("UserName", userGetDTO.UserName),
                new Claim("FirstName", userGetDTO.FirstName),
                new Claim("LastName", userGetDTO.LastName),
                new Claim("Roles", roles)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credential = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            int tokenExpirationTimeInMin = Convert.ToInt32(_configuration["Jwt:TokenExpirationInMin"]);
            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.UtcNow.AddMinutes(tokenExpirationTimeInMin),
                signingCredentials: credential
                );
            return Task.FromResult(new JwtSecurityTokenHandler().WriteToken(token));
        }
    }
}
