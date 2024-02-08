using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SecureApp.Controllers.Utility;
using SecureApp.Models;
using System.Diagnostics;

namespace SecureApp.Controllers
{

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        [AllowAnonymous]
        public IActionResult Index()
        {
            if (Convert.ToString(HttpContext.Session.GetString("UserName")) != null)
                return RedirectToAction("Index", "UserProfile");
            else
                return RedirectToAction("UserLogin", "UserLogin");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}