using DXC.BlogConnect.DTO;
using DXC.BlogConnect.WebApp.ServiceExtension;
using DXC.BlogConnect.WebApp.Services.Interfaces;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static DXC.BlogConnect.DTO.ErrorCode;
/*
* Created By: Kishore
*/
namespace DXC.BlogConnect.WebApp.Pages.Users
{
    public class IndexModel : PageModel
    {
        private readonly IUserService _userService;
        public IList<UserGetDTO> UserList;
        public readonly IAuthService _authService;
        private readonly ILogger<AddModel> _logger;
        public List<Error> ErrorList;
        public UserGetDTO FindUser;
        public IndexModel(IUserService userService, IAuthService authService, ILogger<AddModel> logger)
        {
            _userService = userService;
            UserList = new List<UserGetDTO>();
            _authService = authService;
            _logger = logger;
            ErrorList = new List<Error>();
        }
        public async Task<IActionResult> OnGetAsync()
        {
            try
            {
                if (!User.Identity.IsAuthenticated)
                {
                    RedirectToPage("/login");
                }

                var _userList = await _userService.GetAllUserAsync();

                if (_userList != null)
                {
                    UserList = _userList.Result;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                ErrorList.Add(new Error("ex", "Unhandled error"));
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string submit,UserGetDTO FindUser)
        {
            try
            {
                if(submit=="Find")
                {
                   
                    if (UserList != null)
                    {
                        var user = UserList.FirstOrDefault(x=>x.UserName==FindUser.UserName);
                        if (user != null)
                        {
                            FindUser = user;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                ErrorList.Add(new Error("ex", "Unhandled error"));
            }

            return Page();
        }

        }
}
