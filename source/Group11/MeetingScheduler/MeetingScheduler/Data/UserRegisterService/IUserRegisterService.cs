using MeetingScheduler.Entities;

namespace MeetingScheduler.Data
{
    public interface IUserRegisterService
    {
        UserManagerResponse? UserRegisterDetails(UserRegister user);
    }
}
