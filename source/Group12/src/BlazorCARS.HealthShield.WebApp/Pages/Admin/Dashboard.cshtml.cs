using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BlazorCARS.HealthShield.WebApp.Pages.Admin
{
    public class DashboardModel : PageModel
    {
        public void OnGet()
        {
            string userName = TempData["UserName"]?.ToString();
        }
    }
}
