using DiscussionForumUserInterface.Common;
using DiscussionForumUserInterface.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace DiscussionForumUserInterface.Pages
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public Models.LoginViewModel LoginViewModel { get; set; }
        private readonly ILogger<LoginModel> _logger;
        private readonly HttpClient _client;
        public LoginModel(ILogger<LoginModel> logger)
        {
            _logger = logger;
            _client = new HttpClient();
            _client.BaseAddress = CommonConstants.baseAddress;
        }
        public async Task<IActionResult> OnGet()
        {
            return Page();
        }
        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
              
                HttpContent loginContent = new StringContent(JsonConvert.SerializeObject(LoginViewModel), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _client.PostAsync(_client.BaseAddress + "/Authenticate/Login", loginContent);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    var result = JsonConvert.DeserializeObject<LoginViewModel>(data);
                    LoginViewModel.Code = result?.Code;
                    TempData["Email"] = LoginViewModel.Email;
                    TempData["Password"] = LoginViewModel.Password;
                    return RedirectToPage("SecureLogin");
                }
                else if (response.StatusCode == HttpStatusCode.InternalServerError)
                {
                    return RedirectToPage("Error");
                }
                else
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    var result = JsonConvert.DeserializeObject<ResultModel>(data);
                    TempData["Error"] = result?.Message;
                    LoginViewModel.Email = string.Empty;
                    LoginViewModel.Password = string.Empty;
                    return Page();
                }
            }
            else { return Page(); }
        }

    }
}
