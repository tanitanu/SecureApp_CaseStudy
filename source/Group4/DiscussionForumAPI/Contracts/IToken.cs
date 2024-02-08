using Microsoft.AspNetCore.Identity;
using DiscussionForumAPI.Models;
using System.Security.Claims;

namespace DiscussionForumAPI.Contracts
{
    public interface IToken
    {
        string CreateJWTToken(IdentityUser user, List<string> roles);

        Task<DisussionForumUserTwoFactorCode?> GetByIdAsync(string id);
        Task<DisussionForumUserTwoFactorCode> CreateUpdateAsync(string id, DisussionForumUserTwoFactorCode code);

        Task<DisussionForumUserTwoFactorCode?> UpdateAsync(string id);

        string GenerateRefreshToken();

        ClaimsPrincipal? GetPrincipalFromExpiredToken(string? token);
    }
}
