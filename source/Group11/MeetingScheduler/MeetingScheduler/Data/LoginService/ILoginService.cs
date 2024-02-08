using MeetingScheduler.Entities;


namespace MeetingScheduler.Data
{
    public interface ILoginService
    {
        UserManagerResponse? GetLoginDetails(UserLogin userLogin);

        UserManagerResponse? ForgotPassword(ForgetPassword forgetPassword);

        UserManagerResponse? ResetPassword(ResetPasswordRequest resetPasswordRequest);
        List<UserVo>? GetUsers();
    }
}
