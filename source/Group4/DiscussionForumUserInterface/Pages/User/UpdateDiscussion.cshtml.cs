using DiscussionForumUserInterface.Common;
using DiscussionForumUserInterface.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Net;
using System.Text;

namespace DiscussionForumUserInterface.Pages.User
{
    /// <summary author="Kirti Garg">
    /// This is update discussion page for user where user can update discussion.
    /// </summary>
    public class UpdateDiscussionModel : PageModel
    {
        [BindProperty]
        public Models.CreateUpdateDiscussionModel CreateUpdateDiscussionModel { get; set; }
        private readonly ILogger<UpdateDiscussionModel> _logger;
        private readonly HttpClient _client;
        public SelectList Categories { get; set; }
        static SelectList CategoriesList { get; set; }
        public UpdateDiscussionModel(ILogger<UpdateDiscussionModel> logger)
        {
            _logger = logger;
            _client = new HttpClient();
            _client.BaseAddress = CommonConstants.baseAddress;
        }
        public async Task<IActionResult> OnGet(string questionId)
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
                    HttpResponseMessage response = await _client.GetAsync(_client.BaseAddress + "/Question/GetCategories");
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        string data = response.Content.ReadAsStringAsync().Result;
                        var result = JsonConvert.DeserializeObject<List<Catgory>>(data);
                        this.Categories = new SelectList(result, "CategoryId", "Category");
                        CategoriesList = Categories;
                        if (questionId != null)
                        {
                            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(CommonConstants.authScheme, Request.Cookies["Token"].ToString());
                            response = await _client.GetAsync(_client.BaseAddress + "/Question/" + questionId);
                            if (response.StatusCode == HttpStatusCode.OK)
                            {
                                data = response.Content.ReadAsStringAsync().Result;
                                var res = JsonConvert.DeserializeObject<CreateUpdateDiscussionModel>(data);
                                CreateUpdateDiscussionModel = new CreateUpdateDiscussionModel
                                {
                                    CategoryId = res.CategoryId,
                                    Title = res.Title,
                                    Status = res.Status,
                                    Content = res.Content,
                                    QuestionId = res.QuestionId
                                };
                            }
                        }
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
                        return RedirectToPage("UpdateDiscussion", new { questionId = questionId.ToString() });
                    }
                }
            }
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                HttpResponseMessage response;
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
                        HttpContent discussionContent = new StringContent(JsonConvert.SerializeObject(CreateUpdateDiscussionModel), Encoding.UTF8, "application/json");
                        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(CommonConstants.authScheme, Request.Cookies["Token"].ToString());
                        response = await _client.PutAsync(_client.BaseAddress + "/Question/" + CreateUpdateDiscussionModel.QuestionId, discussionContent);
                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            string data = response.Content.ReadAsStringAsync().Result;
                            var result = JsonConvert.DeserializeObject<CreateUpdateDiscussionModel>(data);
                            TempData["Success"] = "Discussion Post has been updated successfully";
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
                            return RedirectToPage("UpdateDiscussion", new { questionId = CreateUpdateDiscussionModel.QuestionId.ToString() });
                        }
                    }
                }
            }
            else
            {
                Categories = CategoriesList;
                return Page();
            }
        }
    }
}

