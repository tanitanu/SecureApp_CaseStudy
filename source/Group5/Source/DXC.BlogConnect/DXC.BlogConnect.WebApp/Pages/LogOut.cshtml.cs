using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DXC.BlogConnect.WebApp.Pages
{/*
 * Created By: Kishore
 */
    public class LogOutModel : PageModel
    {
        public async Task<IActionResult> OnGet()
        {
             await HttpContext.SignOutAsync();

            return RedirectToPage("./login");
        }

    }
}
