using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SecureApp.Controllers.Utility;
using SecureApp.Models;

namespace SecureApp.Controllers
{
    public class UserLoginController : Controller
    {
        private readonly HttpClient _client;

        public UserLoginController()
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri(ApplicationSettings.WebApiUrl);
        }

        public IActionResult UserLogin()
        {
            return View();
        }
        /// <summary>
        /// Method for Login
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult UserLogin(Login login)
        {
            if (ModelState.IsValid)
            {
                bool result = false;
                using (HttpClient httpClient = new HttpClient())
                {
                    //HTTP POST
                    var postTask = httpClient.PostAsJsonAsync<Login>(_client.BaseAddress + "User/Login", login);
                    postTask.Wait();
                    var response = postTask.Result;
                    if (response.IsSuccessStatusCode)
                    {
                        var data = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                        result = JsonConvert.DeserializeObject<bool>(data);
                    }

                    if (result)
                    {
                        HttpContext.Session.SetString("UserName", login.Name);
                        return RedirectToAction("Index", "UserProfile");
                    }
                }
            }

            // Authentication failed, display an error message
            ViewBag.ErrorMessage = "Invalid username or password.";
            return View();
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }


    }
}
