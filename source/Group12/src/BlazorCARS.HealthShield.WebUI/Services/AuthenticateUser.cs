using BlazorCARS.HealthShield.WebUI.Data;

namespace BlazorCARS.HealthShield.WebUI.Services
{
    public class AuthenticateUser
    {
        public int ValidateUser(string UserID)
        {
            List<UserInfo> userInfo = new List<UserInfo>();

            userInfo.Add(new UserInfo { UserId = "Mohan1", UserLevel = 1 });
            userInfo.Add(new UserInfo { UserId = "Mohan2", UserLevel = 2 });
            userInfo.Add(new UserInfo { UserId = "Mohan3", UserLevel = 3 });

            if(userInfo.Exists(u => u.UserId == UserID))
            {
                return userInfo[userInfo.FindIndex(u => u.UserId == UserID)].UserLevel;
            }
            else
            {
                return 0;
            }            
        }
    }
}
