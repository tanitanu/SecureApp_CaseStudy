using DiscussionForumUserInterface.Common;
using DiscussionForumUserInterface.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Net;

namespace DiscussionForumUserInterface.Pages.Admin
{
    /// <summary author="Kirti Garg">
    /// This is create discussion page for admin where all discussion are list where admin can edit,delete and change status of the discussion.
    /// </summary>
    public class DiscussionsModel : PageModel
    {
        public List<Models.QuestionDetails> QuestionDetails { get; set; }
        private readonly ILogger<DiscussionsModel> _logger;
        private readonly HttpClient _client;
        public DiscussionsModel(ILogger<DiscussionsModel> logger)
        {
            _logger = logger;
            _client = new HttpClient();
            _client.BaseAddress = CommonConstants.baseAddress;
        }
        public async Task<IActionResult> OnGet()
        {
            if (Request.Cookies["Token"] == null)
            {
                foreach (var cookie in Request.Cookies.Keys)
                {
                    Response.Cookies.Delete(cookie);
                }
                return RedirectToPage("/Login");
            }
            else
            {

                Helper helper = new Helper(HttpContext);
                TokenViewModel tokenViewModel = await helper.ValidateAndRefreshAccessToken(Request.Cookies["Token"].ToString(), Request.Cookies["RefreshToken"].ToString());
                if (tokenViewModel == null)
                {
                    foreach (var cookie in Request.Cookies.Keys)
                    {
                        Response.Cookies.Delete(cookie);
                    }
                    return RedirectToPage("/Login");
                }
                else
                {
                    _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(CommonConstants.authScheme, Request.Cookies["Token"].ToString());
                    HttpResponseMessage response = await _client.GetAsync(_client.BaseAddress + "/Question/GetQuestionsList");
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        string data = response.Content.ReadAsStringAsync().Result;
                        var result = JsonConvert.DeserializeObject<List<QuestionDetails>>(data);
                        QuestionDetails = new List<QuestionDetails>(result);
                        return Page();

                    }
                    else if (response.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        foreach (var cookie in Request.Cookies.Keys)
                        {
                            Response.Cookies.Delete(cookie);
                        }
                        return RedirectToPage("/Login");
                    }
                    else if (response.StatusCode == HttpStatusCode.InternalServerError)
                    {
                        return RedirectToPage("/Error");
                    }
                    else
                    {
                        string data = response.Content.ReadAsStringAsync().Result;
                        TempData["Error"] = data;
                        return RedirectToPage();
                    }
                }
            }
        }

        public async Task<IActionResult> OnPostDeleteDiscussion(string questionId)
        {
            if (Request.Cookies["Token"] == null)
            {
                foreach (var cookie in Request.Cookies.Keys)
                {
                    Response.Cookies.Delete(cookie);
                }
                return RedirectToPage("/Login");
            }
            else
            {

                Helper helper = new Helper(HttpContext);
                TokenViewModel tokenViewModel = await helper.ValidateAndRefreshAccessToken(Request.Cookies["Token"].ToString(), Request.Cookies["RefreshToken"].ToString());
                if (tokenViewModel == null)
                {
                    foreach (var cookie in Request.Cookies.Keys)
                    {
                        Response.Cookies.Delete(cookie);
                    }
                    return RedirectToPage("/Login");
                }
                else
                {
                    _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(CommonConstants.authScheme, Request.Cookies["Token"].ToString());
                    HttpResponseMessage response = await _client.DeleteAsync(_client.BaseAddress + "/Question/" + questionId);
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        string data = response.Content.ReadAsStringAsync().Result;
                        var result = JsonConvert.DeserializeObject<CreateUpdateDiscussionModel>(data);
                        TempData["Success"] = "Discussion Post has been deleted successfully";
                        return RedirectToPage("Discussions");
                    }
                    else if (response.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        foreach (var cookie in Request.Cookies.Keys)
                        {
                            Response.Cookies.Delete(cookie);
                        }
                        return RedirectToPage("/Login");
                    }
                    else if (response.StatusCode == HttpStatusCode.InternalServerError)
                    {
                        return RedirectToPage("/Error");
                    }
                    else
                    {
                        string data = response.Content.ReadAsStringAsync().Result;
                        TempData["Error"] = data;
                        return RedirectToPage();
                    }
                }
            }

        }

        public async Task<IActionResult> OnPostStatusDiscussion(string questionId)
        {
            if (Request.Cookies["Token"] == null)
            {
                foreach (var cookie in Request.Cookies.Keys)
                {
                    Response.Cookies.Delete(cookie);
                }
                return RedirectToPage("/Login");
            }
            else
            {

                Helper helper = new Helper(HttpContext);
                TokenViewModel tokenViewModel = await helper.ValidateAndRefreshAccessToken(Request.Cookies["Token"].ToString(), Request.Cookies["RefreshToken"].ToString());
                if (tokenViewModel == null)
                {
                    foreach (var cookie in Request.Cookies.Keys)
                    {
                        Response.Cookies.Delete(cookie);
                    }
                    return RedirectToPage("/Login");
                }
                else
                {
                    _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(CommonConstants.authScheme, Request.Cookies["Token"].ToString());
                    HttpResponseMessage response = await _client.PostAsync(_client.BaseAddress + "/Question/Status/" + questionId, null);
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        string data = response.Content.ReadAsStringAsync().Result;
                        var result = JsonConvert.DeserializeObject<CreateUpdateDiscussionModel>(data);
                        TempData["Success"] = "Discussion Post Status has been updated successfully";
                        return RedirectToPage("Discussions");
                    }
                    else if (response.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        foreach (var cookie in Request.Cookies.Keys)
                        {
                            Response.Cookies.Delete(cookie);
                        }
                        return RedirectToPage("/Login");
                    }
                    else if (response.StatusCode == HttpStatusCode.InternalServerError)
                    {
                        return RedirectToPage("/Error");
                    }
                    else
                    {
                        string data = response.Content.ReadAsStringAsync().Result;
                        TempData["Error"] = data;
                        return RedirectToPage();
                    }
                }
            }

        }

        public async Task<IActionResult> OnGetLogout()
        {
            foreach (var cookie in Request.Cookies.Keys)
            {
                Response.Cookies.Delete(cookie);
            }
            return RedirectToPage("/Login");
        }
    }
}
