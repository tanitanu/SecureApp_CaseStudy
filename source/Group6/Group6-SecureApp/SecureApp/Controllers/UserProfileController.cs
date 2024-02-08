using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SecureApp.Controllers.Utility;

namespace SecureApp.Controllers
{
    public class UserProfileController : Controller
    {
        private readonly ILogger<UserProfileController> _logger;
        private readonly HttpClient _client;

        public UserProfileController(ILogger<UserProfileController> logger)
        {
            _logger = logger;
            _client = new HttpClient();
            _client.BaseAddress = new Uri(ApplicationSettings.WebApiUrl);
        }

        /// <summary>
        /// user details 
        /// </summary>
        /// <returns>user details</returns>
        public async Task<IActionResult> Index()
        {
            Models.UserProfile data = new Models.UserProfile();
            using (HttpClient httpClient = new HttpClient())
            {
                var response = httpClient.GetAsync(_client.BaseAddress + "User/FindUserProfilebyUserName?name=" + Convert.ToString(HttpContext.Session.GetString("UserName"))).GetAwaiter().GetResult();
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                    data = JsonConvert.DeserializeObject<Models.UserProfile>(result);

                    if (data != null)
                    {
                        ViewBag.Name = data.User.Name;
                        return View(new List<Models.UserProfile>() { data });
                    }
                    else
                        return RedirectToAction("UserLogin", "UserLogin");
                }
                else
                    return RedirectToAction("UserLogin", "UserLogin");
            }
        }

        //public async Task<IActionResult> Index()
        //{
        //    List<Models.UserProfile> data = new List<Models.UserProfile>();
        //    using (HttpClient httpClient = new HttpClient())
        //    {
        //        var response = httpClient.GetAsync(_client.BaseAddress + "User/GetAllUserProfiles").GetAwaiter().GetResult();
        //        if (response.IsSuccessStatusCode)
        //        {
        //            var result = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
        //            data = JsonConvert.DeserializeObject<List<Models.UserProfile>>(result);

        //            if (data != null && data.Count > 0)
        //            {
        //                return View(data);
        //            }
        //            else
        //                return RedirectToAction("UserLogin", "UserLogin");
        //        }
        //        else
        //            return RedirectToAction("UserLogin", "UserLogin");
        //    }
        //}

        [HttpGet]
        public async Task<IActionResult> Details(string id)
        {
            var data = getUserDetails(id);
            if (data != null)
                return View(data);
            else
                return NotFound();
        }

        public async Task<IActionResult> Edit(string id)
        {
            var data = getUserDetails(id);
            if (data != null)
            {
                ViewData["User"] = data.User.Name;
                ViewBag.User = data.User.Name;
                return View(data);
            }
            else
                return RedirectToAction("UserLogin", "UserLogin");

        }

        /// <summary>
        /// user details Edit method
        /// </summary>
        /// <param name="id"></param>
        /// <param name="UserProfileData"></param>
        /// <returns></returns>
        [HttpPost("UserProfile/Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Name,Dob,Adhar")] Models.UserProfile UserProfileData)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Models.UserProfile data = new Models.UserProfile();
                    if (UserProfileData != null)
                        UserProfileData.UserId = "0";
                    using (HttpClient httpClient = new HttpClient())
                    {
                        var postTask = httpClient.PutAsJsonAsync<Models.UserProfile>(_client.BaseAddress + "User/EditUserProfile", UserProfileData);
                        postTask.Wait();
                        var response = postTask.Result;
                        if (response.IsSuccessStatusCode)
                        {
                            var result = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                            data = JsonConvert.DeserializeObject<Models.UserProfile>(result);

                            if (data != null)
                            {
                                return RedirectToAction("Index", "UserProfile");
                            }
                            else
                                return RedirectToAction(nameof(Index));
                        }
                        else
                            return RedirectToAction(nameof(Index));

                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError("Error in Method - Edit", ex.Message + Environment.NewLine + ex.StackTrace);
                    return View("Error");
                }

            }
            else
                return View(UserProfileData);
        }

        /// <summary>
        /// Delete user method
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                Models.UserProfile userProfile = new Models.UserProfile();

                using (HttpClient httpClient = new HttpClient())
                {
                    var postTask = httpClient.DeleteAsync(_client.BaseAddress + "User/DeleteUser?id=" + id);
                    postTask.Wait();
                    var response = postTask.Result;
                    if (response.IsSuccessStatusCode)
                    {
                        var result = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                        var status = JsonConvert.DeserializeObject<bool>(result);

                        if (status)
                        {
                            //return RedirectToAction("Index", "UserProfile");
                            return RedirectToAction("UserLogin", "UserLogin");
                        }
                    }
                    else
                        return View(userProfile);

                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in Method - Delete", ex.Message + Environment.NewLine + ex.StackTrace);
                return View("Error");
            }
            return RedirectToAction("UserLogin", "UserLogin");
        }

        [HttpGet]
        public IActionResult ChangePassword(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                var profile = getUserDetails(id);
                ViewBag.Name = profile.User.Name;
                return View();
            }
            else
                return RedirectToAction("Index", "UserProfile");
        }

        [HttpPost("UserProfile/ChangePassword")]
        [ValidateAntiForgeryToken]
        /// <summary>
        /// xChange Password method
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> ChangePassword(string userName, [Bind("UserName,NewPassword,ConfirmPassword")] Models.ChangePassword pass)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (pass.NewPassword == pass.ConfirmPassword)
                    {
                        using (HttpClient httpClient = new HttpClient())
                        {
                            var postTask = httpClient.PostAsJsonAsync<Models.ChangePassword>(_client.BaseAddress + "User/ChangePassword", pass);
                            postTask.Wait();
                            var response = postTask.Result;
                            if (response.IsSuccessStatusCode)
                            {
                                var result = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                                var status = JsonConvert.DeserializeObject<bool>(result);
                                if (status)
                                {
                                    return Logout();
                                }
                            }
                        }
                    }

                }
                catch (Exception ex)
                {
                    _logger.LogError("Error in Method - ChangePassword", ex.Message + Environment.NewLine + ex.StackTrace);
                    return View("Error");
                }

            }

            return RedirectToAction("ChangePassword", "UserProfile");
        }


        public ActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("UserLogin", "UserLogin");
        }



        #region Helper methods
        /// <summary>
        /// Method to search user data
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>User details</returns>
        private Models.UserProfile getUserDetails(string Id)
        {
            Models.UserProfile data = new Models.UserProfile();
            using (HttpClient httpClient = new HttpClient())
            {
                var response = httpClient.GetAsync(_client.BaseAddress + "User/FindProfilebyProfileID?Id=" + Id).GetAwaiter().GetResult();
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                    data = JsonConvert.DeserializeObject<Models.UserProfile>(result);
                }
            }
            return data;
        }
        #endregion


    }
}
