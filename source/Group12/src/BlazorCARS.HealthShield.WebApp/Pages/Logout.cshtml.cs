using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BlazorCARS.HealthShield.WebApp.Pages
{
    public class LogoutModel : PageModel
    {
        public IActionResult OnGet()
        {
            // Clear user-related session data
            HttpContext.Session.Remove("UserName");
            HttpContext.Session.Remove("UserRole");
            HttpContext.Session.Remove("DiscriminationId");
            HttpContext.Session.Remove("Token");

            // Sign out the user
            HttpContext.SignOutAsync();
            // Redirect to the home page or any other page after logout
            return RedirectToPage("/Index");
        }

    }
}
