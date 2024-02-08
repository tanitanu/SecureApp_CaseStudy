using DXC.BlogConnect.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DXC.BlogConnect.WebApp.Pages.Users
{
    public class ChangePasswordModel : PageModel
    {
        public UserGetDTO PasswordUser;
        public void OnGet()
        {
        }
    }
}
