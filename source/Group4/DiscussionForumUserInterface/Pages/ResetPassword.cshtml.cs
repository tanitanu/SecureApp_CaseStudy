using DiscussionForumUserInterface.Common;
using DiscussionForumUserInterface.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Net;
using System.Reflection;
using System.Text;

namespace DiscussionForumUserInterface.Pages
{
    public class ResetPasswordModel : PageModel
    {
        [BindProperty]
        public Models.ResetPasswordViewModel ResetPasswordViewModel { get; set; }
        private readonly ILogger<ResetPasswordModel> _logger;
        private readonly HttpClient _client;
        public ResetPasswordModel(ILogger<ResetPasswordModel> logger)
        {
            _logger = logger;
            _client = new HttpClient();
            _client.BaseAddress = CommonConstants.baseAddress;
        }
        public void OnGet([FromQuery(Name = "token")] string token, [FromQuery(Name = "email")] string email)
        {
            ResetPasswordViewModel = new ResetPasswordViewModel { Email = email };
            TempData["Code"] = token;
        }
        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                ResetPasswordViewModel.Token = TempData.Peek("Code").ToString();
                TempData.Keep("Code");
                HttpContent resetPasswordContent = new StringContent(JsonConvert.SerializeObject(ResetPasswordViewModel), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _client.PostAsync(_client.BaseAddress + "/Authenticate/ResetPassword", resetPasswordContent);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    TempData["Code"] = null;
                    string data = response.Content.ReadAsStringAsync().Result;
                    var result = JsonConvert.DeserializeObject<ResultModel>(data);
                    TempData["Success"] = result?.Message;
                    return Page();
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
                    return Page();
                }
            }
            return Page();
        }

    }
}
