using MeetingScheduler.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingScheduler.Model
{
    public interface IRoleHandler
    {
        Task<Role> GetRole(UserLogin userLogin);

        Task<IEnumerable<RoleVo>> GetRoles();
    }
}
