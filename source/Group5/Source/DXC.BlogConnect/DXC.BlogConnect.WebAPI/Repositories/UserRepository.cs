using DXC.BlogConnect.WebAPI.Data;
using DXC.BlogConnect.WebAPI.Models.Domain;
using DXC.BlogConnect.DTO;
using DXC.BlogConnect.WebAPI.Repositories;
using DXC.BlogConnect.WebAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DXC.BlogConnect.WebAPI.Repositories
{
    /*
* Created By: Kishore
*/
    public class UserRepository:GenericRepository<User>,IUserRepository
    {
        private readonly BlogConnectDbcontext _dbContext;
        public UserRepository(BlogConnectDbcontext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

       public async Task<UserGetDTO> GetUserByUserName(string userName)
        {
            return await (from u in _dbContext.Users
                          join r in _dbContext.Roles on u.RoleId equals r.Id
                          where u.UserName == userName
                          select new UserGetDTO
                          {
                              Id = u.Id,
                              UserName = u.UserName,
                              FirstName = u.FirstName,
                              LastName = u.LastName,
                              EmailId = u.EmailId,
                              RoleId = u.RoleId,
                              RoleName = r.Name,
                              Password=u.Password
                          }).FirstOrDefaultAsync();

        }
        public bool IsUserExistByUsername(string userName)
        { 
            var result=_dbContext.Users.FirstOrDefault(u=>u.UserName == userName);
            return result != null;
        }
        public async Task<List<UserGetDTO>> GetAllUserWithUserName()
        {
            return await (from u in _dbContext.Users
                          join r in _dbContext.Roles on u.RoleId equals r.Id
                          select new UserGetDTO
                          {
                              Id = u.Id,
                              UserName = u.UserName,
                              FirstName = u.FirstName,
                              LastName = u.LastName,
                              EmailId = u.EmailId,
                              RoleId = u.RoleId,
                              RoleName = r.Name,
                              Password = u.Password
                          }).ToListAsync();

        }

    }
}
