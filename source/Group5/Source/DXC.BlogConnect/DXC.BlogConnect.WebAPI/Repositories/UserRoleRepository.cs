using DXC.BlogConnect.WebAPI.Data;
using DXC.BlogConnect.WebAPI.Models.Domain;
using DXC.BlogConnect.WebAPI.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DXC.BlogConnect.WebAPI.Repositories
{
    /*
* Created By: Kishore
*/
    public class UserRoleRepository:GenericRepository<Role>,IUserRoleRepository
    {
        private readonly BlogConnectDbcontext _dbContext;
        public UserRoleRepository(BlogConnectDbcontext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public bool IsRoleExistByRolename(string roleName)
        {
            var result = _dbContext.Roles.FirstOrDefault(u => u.Name == roleName);
            return result != null;
        }

        public Role GetRoleByID(int id)
        {

            var result = _dbContext.Roles.FirstOrDefault(u => u.Id == id);
            return result;
        }
    }
}
