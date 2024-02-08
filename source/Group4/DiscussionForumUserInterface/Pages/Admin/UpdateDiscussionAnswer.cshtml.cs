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
    /// This is update discussion answer page for admin where admin can update answer on a certain discussion.
    /// </summary>
    public class UpdateDiscussionAnswerModel : PageModel
    {
        [BindProperty]
        public Models.UpdateDiscussionAnswer UpdateDiscussionAnswer { get; set; }
        private readonly ILogger<UpdateDiscussionAnswerModel> _logger;
        private readonly HttpClient _client;
        public UpdateDiscussionAnswerModel(ILogger<UpdateDiscussionAnswerModel> logger)
        {
            _logger = logger;
            _client = new HttpClient();
            _client.BaseAddress = CommonConstants.baseAddress;
        }
        public async Task<IActionResult> OnGet(string questionId, string answerId)
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
                    HttpResponseMessage response = await _client.GetAsync(_client.BaseAddress + "/Answer/GetQuestionAnswerDetailsById/" + questionId + "/" + answerId);
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        string data = response.Content.ReadAsStringAsync().Result;
                        var result = JsonConvert.DeserializeObject<QuestionAnswerDetails>(data);
                        UpdateDiscussionAnswer = new UpdateDiscussionAnswer
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
                            Answer = result.AnswerDetails.FirstOrDefault().Answer,
                            AnswerID = result.AnswerDetails.FirstOrDefault().AnswerID,
                            AnswerCreatedBy = result.AnswerDetails.FirstOrDefault().AnswerCreatedBy,
                            AnswerCreatedByName = result.AnswerDetails.FirstOrDefault().AnswerCreatedByName,
                            AnswerDateCreation = result.AnswerDetails.FirstOrDefault().AnswerDateCreation,
                            AnswerIsDelete = result.AnswerDetails.FirstOrDefault().AnswerIsDelete
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
                        return RedirectToPage("Discussion", new { questionId = questionId.ToString() });
                    }
                }
            }
        }

        public async Task<IActionResult> OnPost()
        {
            if (!String.IsNullOrEmpty(UpdateDiscussionAnswer.Answer))
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
                        HttpContent discussionAnswerContent = new StringContent(JsonConvert.SerializeObject(UpdateDiscussionAnswer), Encoding.UTF8, "application/json");
                        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(CommonConstants.authScheme, Request.Cookies["Token"].ToString());
                        response = await _client.PutAsync(_client.BaseAddress + "/Answer/" + UpdateDiscussionAnswer.AnswerID, discussionAnswerContent);
                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            string data = response.Content.ReadAsStringAsync().Result;
                            var result = JsonConvert.DeserializeObject<UpdateDiscussionAnswer>(data);
                            TempData["Success"] = "Discussion Post Answer has been updated successfully";
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
                            return RedirectToPage("UpdateDiscussionAnswer", new { questionId = UpdateDiscussionAnswer.QuestionId.ToString(), answerId = UpdateDiscussionAnswer.AnswerID.ToString() });
                        }
                    }
                }
            }
            else
            {
                TempData["Error"] = "Answer is required!";
                return RedirectToPage("UpdateDiscussionAnswer", new { questionId = UpdateDiscussionAnswer.QuestionId.ToString(), answerId = UpdateDiscussionAnswer.AnswerID.ToString() });

            }
        }
    }
}
