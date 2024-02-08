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
    public class RegisterModel : PageModel
    {
        [BindProperty]
        public Models.RegisterViewModel RegisterViewModel { get; set; }
        private readonly ILogger<RegisterModel> _logger;
        private readonly HttpClient _client;
        public RegisterModel(ILogger<RegisterModel> logger)
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
                HttpContent registerContent = new StringContent(JsonConvert.SerializeObject(RegisterViewModel), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _client.PostAsync(_client.BaseAddress + "/Authenticate/Register", registerContent);
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
            else
            {
                return Page();
            }
        }

    }
}
