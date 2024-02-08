using MeetingScheduler.Entities;

namespace MeetingScheduler.Data
{
    public interface IRoleService
    {
        Role? GetRole(UserLogin user);

        List<RoleVo>? GetRoles();

        UserManagerResponse? UpdateRole(int id, Role role);
    }
}
