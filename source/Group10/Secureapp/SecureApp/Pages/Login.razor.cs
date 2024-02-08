using Microsoft.AspNetCore.Components;
using SecureApp.Model.SecureAppModel;
using SecureApp.SecureAppModel;
using SecureApp.Service;

namespace SecureApp.Pages
{
    public partial class Login
    {
        private UserLoginDto _userLogin = new UserLoginDto();

        [Inject]
        public IAuthenticateService AuthenticationService { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        public bool ShowLoginErrors { get; set; }
        public string Errors { get; set; }

        public async Task Login_User()
        {
            ShowLoginErrors = false;

            var result = await AuthenticationService.LoginUser(_userLogin);
            if (!result.IsSuccessfulLogin)
            {
                Errors = result.Errors;
                ShowLoginErrors = true;
            }
            else
            {
                NavigationManager.NavigateTo("/UserLog");
            }
        }
    }
}
