using DiscussionForumUserInterface.Common;
using DiscussionForumUserInterface.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Net;
using System.Reflection;

namespace DiscussionForumUserInterface.Pages
{
    public class ForgotPasswordModel : PageModel
    {
        [BindProperty]
        public Models.ForgotPasswordViewModel ForgotPasswordViewModel { get; set; }
        private readonly ILogger<ForgotPasswordModel> _logger;
        private readonly HttpClient _client;
        public ForgotPasswordModel(ILogger<ForgotPasswordModel> logger)
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
            string baseUrl = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";
            string clientURI = baseUrl + "/ResetPassword";
            HttpResponseMessage response = await _client.PostAsync(_client.BaseAddress + $"/Authenticate/ForgotPassword/{ForgotPasswordViewModel.Email}?clientURI={clientURI}", null);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                var result = JsonConvert.DeserializeObject<ResultModel>(data);
                TempData["Success"] = result?.Message;
                return RedirectToPage();
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
    }
}
