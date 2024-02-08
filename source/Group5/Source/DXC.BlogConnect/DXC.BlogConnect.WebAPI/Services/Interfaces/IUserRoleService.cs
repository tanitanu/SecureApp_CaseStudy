using DXC.BlogConnect.WebAPI.Models.Domain;

namespace DXC.BlogConnect.WebAPI.Services.Interfaces
{
    public interface IUserRoleService
    {
        Task<IEnumerable<Role>> GetAllRoleAsync();
        bool IsRoleExistByRolename(string roleName);

        Task AddRoleAsync(Role entity);
        void UpdateRole(Role entity);

        Role GetRoleById(int roleId);
    }
}
