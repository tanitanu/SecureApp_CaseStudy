using MeetingScheduler.DAL;
using MeetingScheduler.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingScheduler.Model
{
    public class RoleHandler : BaseHandler,IRoleHandler
    {
        private readonly IRoleDAL roleDAL;  //Role Dal object

        /// <summary>
        /// Role handler constructor
        /// </summary>
        public RoleHandler()
        {
            roleDAL = new RoleDAL();
        }

        /// <summary>
        /// Get role by user id
        /// </summary>
        /// <param name="userLogin"></param>
        /// <returns></returns>
        public  Task<Role> GetRole(UserLogin userLogin)
        {
            try
            {
                return roleDAL.GetRole(userLogin);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        /// <summary>
        /// Get All roles
        /// </summary>
        /// <returns></returns>
        public Task<IEnumerable<RoleVo>> GetRoles()
        {
            try
            {
                return roleDAL.GetRoles();
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
