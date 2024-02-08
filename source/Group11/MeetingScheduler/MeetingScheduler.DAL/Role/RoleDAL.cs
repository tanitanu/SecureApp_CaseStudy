using MeetingScheduler.DAL.Models;
using MeetingScheduler.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingScheduler.DAL
{
    public class RoleDAL : BaseDAL, IRoleDAL
    {
        #region
        private const string GET_ROLE_ERR = "An error occurred while retrieving role for the user.";
        private const string GET_ROLES_ERR = "An error occurred while retrieving roles.";
        #endregion
        public RoleDAL()
        {
        }
        /// <summary>
        /// Get user role
        /// </summary>
        /// <param name="userLogin"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="Exception"></exception>
        public async Task<Role> GetRole(UserLogin userLogin)
        {
            if (userLogin == null)
            {
                throw new ArgumentNullException(nameof(userLogin), "UserLogin cannot be null.");
            }

            Role role = new Role();
            try
            {
                using (var context = new MeetingSchedulerContext())
                {
                    var user = await context.Users.FirstOrDefaultAsync(u => u.Email == userLogin.EmailId);

                    if (user != null)
                    {
                        role = await context.Roles.FirstOrDefaultAsync(r => r.RoleId == user.RoleId);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(GET_ROLE_ERR, ex);
            }

            return role;
        }

        /// <summary>
        /// Get all roles
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<IEnumerable<RoleVo>> GetRoles()
        {
            try
            {
                using (var context = new MeetingSchedulerContext())
                {
                    var roles = await context.Roles.ToListAsync();

                    var _lstRoles = from _role in roles
                                    select new RoleVo
                                    {
                                        RoleId = _role.RoleId,
                                        RoleName = _role.RoleName
                                    };

                    return _lstRoles;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(GET_ROLES_ERR, ex);
            }
        }


    }
}
