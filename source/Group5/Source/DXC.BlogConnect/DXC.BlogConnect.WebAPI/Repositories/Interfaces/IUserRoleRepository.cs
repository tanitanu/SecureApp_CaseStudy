using DXC.BlogConnect.WebAPI.Models.Domain;
using Microsoft.AspNetCore.Mvc;

namespace DXC.BlogConnect.WebAPI.Repositories.Interfaces
{
    /*
* Created By: Kishore
*/
    public interface IUserRoleRepository:IGenericRepository<Role>
    {
        bool IsRoleExistByRolename(string roleName);

        Role GetRoleByID(int id);

        //can implement based on business requirement.

    }
}
