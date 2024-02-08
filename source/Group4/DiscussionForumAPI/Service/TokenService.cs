using DiscussionForumAPI.Auth;
using DiscussionForumAPI.Contracts;
using DiscussionForumAPI.Controllers;
using DiscussionForumAPI.Helper;
using DiscussionForumAPI.Models;
using Microsoft.AspNetCore.DataProtection.XmlEncryption;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace DiscussionForumAPI.Service
{
    public class TokenService : IToken
    {
        private readonly DiscussionForumContext _dbContext;
        private readonly IConfiguration _configuration;
        public TokenService(DiscussionForumContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _configuration = configuration;
        }

        public string CreateJWTToken(IdentityUser user, List<string> roles)
        {
            var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                     new Claim(ClaimTypes.GivenName, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

            foreach (var userRole in roles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
            _ = int.TryParse(_configuration["JWT:TokenValidityInMinutes"], out int tokenValidityInMinutes);

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddMinutes(tokenValidityInMinutes),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<DisussionForumUserTwoFactorCode?> GetByIdAsync(string id)
        {
          return await _dbContext.DisussionForumUserTwoFactorCodes.FirstOrDefaultAsync(x => x.UserId == id);
        }

        public async Task<DisussionForumUserTwoFactorCode> CreateUpdateAsync(string id , DisussionForumUserTwoFactorCode code)
        {
            var existingCode = await _dbContext.DisussionForumUserTwoFactorCodes.FirstOrDefaultAsync(x => x.UserId == id);

            if (existingCode == null)
            {
               await _dbContext.DisussionForumUserTwoFactorCodes.AddAsync(code);
            }
            else
            {
                existingCode.Code = code.Code;
            }
            await _dbContext.SaveChangesAsync();
            return code;
        }

        public async Task<DisussionForumUserTwoFactorCode?> UpdateAsync(string id)
        {
            var existingCode = await _dbContext.DisussionForumUserTwoFactorCodes.FirstOrDefaultAsync(x => x.UserId == id);

            if (existingCode == null)
            {
                return null;
            }

            existingCode.Code = null;
            await _dbContext.SaveChangesAsync();
            return existingCode;
        }

        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        public ClaimsPrincipal? GetPrincipalFromExpiredToken(string? token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"])),
                ValidateLifetime = false
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
            if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");
            return principal;
        }
    }
}
