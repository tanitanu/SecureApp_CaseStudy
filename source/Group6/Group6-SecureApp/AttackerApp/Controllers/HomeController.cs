﻿using AttackerApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AttackerApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
           // return View();
            return View("UnsecureAppAttackCP");
        }


        public IActionResult AttackSecureApp()
        {
            return View("SecureAppAttack");

        }
        public IActionResult AttackUnsecureAppCP()
        {
            return View("UnsecureAppAttackCP");

        }
        public IActionResult AttackUnsecureAppEdit()
        {
            return View("UnsecureAppAttackEdit");

        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}