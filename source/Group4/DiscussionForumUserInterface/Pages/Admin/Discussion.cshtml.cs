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

namespace DiscussionForumUserInterface.Pages.Admin
{
    /// <summary author="Kirti Garg">
    /// This is discussion page for admin where admin can create discussion answer, delete answer and like dislike answer.
    /// </summary>
    public class DiscussionModel : PageModel
    {
        [BindProperty]
        public Models.QuestionAnswerDetails QuestionAnswerDetails { get; set; }
        private readonly ILogger<DiscussionModel> _logger;
        private readonly HttpClient _client;
        public DiscussionModel(ILogger<DiscussionModel> logger)
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
                return RedirectToPage("Discussions");
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
                    HttpResponseMessage response = await _client.GetAsync(_client.BaseAddress + "/Question/GetQuestionAnswerDetailsById/" + questionId);
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        string data = response.Content.ReadAsStringAsync().Result;
                        var result = JsonConvert.DeserializeObject<QuestionAnswerDetails>(data);
                        QuestionAnswerDetails = new QuestionAnswerDetails
                        {
                            QuestionId = result.QuestionId,
                            CategoryName = result.CategoryName,
                            Title = result.Title,
                            Content = result.Content,
                            LikeCount = result.LikeCount,
                            DislikeCount = result.DislikeCount,
                            Status = result.Status,
                            QuestionCreatedBy = result.QuestionCreatedBy,
                            QuestionCreatedByName = result.QuestionCreatedByName,
                            QuestionDateCreation = result.QuestionDateCreation,
                            QuestionIsDelete = result.QuestionIsDelete,
                            Like = result.Like,
                            Dislike = result.Dislike,
                            AnswerDetails = result.AnswerDetails
                        };
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
                        return RedirectToPage("Discussions");
                    }
                }
            }
        }

        public async Task<IActionResult> OnPostAnswer()
        {

                if (!System.String.IsNullOrEmpty(QuestionAnswerDetails.Answer))
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
                            HttpContent discussionAnswerContent = new StringContent(JsonConvert.SerializeObject(QuestionAnswerDetails), Encoding.UTF8, "application/json");
                            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(CommonConstants.authScheme, Request.Cookies["Token"].ToString());
                            HttpResponseMessage response = await _client.PostAsync(_client.BaseAddress + "/Answer", discussionAnswerContent);
                            if (response.StatusCode == HttpStatusCode.Created)
                            {
                                string data = response.Content.ReadAsStringAsync().Result;
                                var result = JsonConvert.DeserializeObject<QuestionAnswerDetails>(data);
                                TempData["Success"] = "Discussion Post Answer has been created successfully";
                                return RedirectToPage("Discussion", new { questionId = result.QuestionId.ToString() });

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
                                return RedirectToPage("Discussion", new { questionId = QuestionAnswerDetails.QuestionId.ToString() });
                            }
                        }
                    }
                }
                else
                {
                    TempData["Error"] = "Answer is required!";
                    return RedirectToPage("Discussion", new { questionId = QuestionAnswerDetails.QuestionId.ToString() });
                }
        }

        public async Task<IActionResult> OnPostDeleteAnswerDiscussion(string questionId, string answerId)
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
                    HttpResponseMessage response = await _client.DeleteAsync(_client.BaseAddress + "/Answer/" + answerId);
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        string data = response.Content.ReadAsStringAsync().Result;
                        var result = JsonConvert.DeserializeObject<CreateUpdateDiscussionModel>(data);
                        TempData["Success"] = "Discussion Post Answer has been deleted successfully";
                        return RedirectToPage("Discussion", new { questionId = questionId.ToString() });
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
                        return RedirectToPage("Discussion", new { questionId = questionId.ToString() });
                    }
                }
            }

        }

        public async Task<IActionResult> OnPostLikeDislikeDiscussion(string questionId, bool like, bool dislike)
        {
            LikeDislikeModel model = new LikeDislikeModel { QuestionId = questionId, Like = like, Dislike = dislike };
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
                    HttpContent discussionLikeDislikeContent = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
                    _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(CommonConstants.authScheme, Request.Cookies["Token"].ToString());
                    HttpResponseMessage response = await _client.PostAsync(_client.BaseAddress + "/LikeDislikeQuestion", discussionLikeDislikeContent);
                    if (response.StatusCode == HttpStatusCode.Created)
                    {
                        string data = response.Content.ReadAsStringAsync().Result;
                        var result = JsonConvert.DeserializeObject<LikeDislikeModel>(data);
                        return RedirectToPage("Discussion", new { questionId = result.QuestionId.ToString() });

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
                        return RedirectToPage("Discussion", new { questionId = questionId.ToString() });
                    }
                }
            }
        }

    }
}
