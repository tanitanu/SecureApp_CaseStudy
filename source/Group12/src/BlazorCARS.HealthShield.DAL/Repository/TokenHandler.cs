using BlazorCARS.HealthShield.DAL.Entity;
using BlazorCARS.HealthShield.DAL.IRepository;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
/*
  Created by JAYaseelan
 */
namespace BlazorCARS.HealthShield.DAL.Repository
{
    public class TokenHandler : ITokenHandler
    {
        private readonly IConfiguration _configuration;

        public TokenHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Task<string> GenerateTokenAsync(UserStore userStore, string userRole)
        {
            //Create Claims
            var claims = new[]
            {
                new Claim("UserName", userStore.UserName),
                new Claim("RoleId", userStore.UserRoleId.ToString()),
                new Claim("UserRole", userRole),
                new Claim("Discrimination", userStore.DiscriminationId != null ? userStore.DiscriminationId.ToString()
                                    : string.Empty)
            };

            //Create Taoken
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
