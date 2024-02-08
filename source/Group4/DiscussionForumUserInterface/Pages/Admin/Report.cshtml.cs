using DiscussionForumUserInterface.Common;
using DiscussionForumUserInterface.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Net;
using System.Text;
using static DiscussionForumUserInterface.Models.AdminReportModel;

namespace DiscussionForumUserInterface.Pages.Admin
{
    /// <summary author="Kirti Garg">
    /// This is report page for admin where admin can see report based on closed,open and top contributor.
    /// </summary>
    public class ReportModel : PageModel
    {
        [BindProperty]
        public Models.AdminReportModel ReportAdminModel { get; set; }
        public List<QuestionReportDetails> ReportDetails { get; set; }
        public List<TopContributor> TopContributors { get; set; }
        private readonly ILogger<ReportModel> _logger;
        private readonly HttpClient _client;

        public ReportModel(ILogger<ReportModel> logger)
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
            if (ReportAdminModel.Status != "Top")
            {
                if ((ReportAdminModel.Type == "Year" && !String.IsNullOrWhiteSpace(ReportAdminModel.FromYearDate)) || (ReportAdminModel.Type == "Month" && !String.IsNullOrWhiteSpace(ReportAdminModel.FromMonthDate))
                    || (ReportAdminModel.Type == "Week" && !String.IsNullOrWhiteSpace(ReportAdminModel.FromWeekDate)))
                {
                    if (ReportAdminModel.Type == "Year")
                    {
                        ReportAdminModel.FromDate = ReportAdminModel.FromYearDate;
                    }
                    else if (ReportAdminModel.Type == "Month")
                    {
                        ReportAdminModel.FromDate = ReportAdminModel.FromMonthDate;
                    }
                    else
                    {
                        ReportAdminModel.FromDate = ReportAdminModel.FromWeekDate;
                    }
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
                            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(CommonConstants.authScheme, Request.Cookies["Token"].ToString());
                            response = await _client.GetAsync(_client.BaseAddress + "/Report/" + ReportAdminModel.Status + "/" + ReportAdminModel.Type + "/" + ReportAdminModel.FromDate + "/" + ReportAdminModel.ToDate);
                            if (response.StatusCode == HttpStatusCode.OK)
                            {
                                string data = response.Content.ReadAsStringAsync().Result;
                                var result = JsonConvert.DeserializeObject<AdminReportModel>(data);
                                ReportDetails = result.QuestionReportDetails;
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
                                return Page();
                            }
                        }
                    }
                }
                else
                {
                    TempData["Error"] = "From Date is required!";
                    return Page();
                }
            }
            else
            {
                if ((ReportAdminModel.Type == "Year" && !String.IsNullOrWhiteSpace(ReportAdminModel.FromYearDate)) || (ReportAdminModel.Type == "Month" && !String.IsNullOrWhiteSpace(ReportAdminModel.FromMonthDate))
                    || (ReportAdminModel.Type == "Week" && !String.IsNullOrWhiteSpace(ReportAdminModel.FromWeekDate)))
                {
                    if (ReportAdminModel.Type == "Year")
                    {
                        ReportAdminModel.FromDate = ReportAdminModel.FromYearDate;
                    }
                    else if (ReportAdminModel.Type == "Month")
                    {
                        ReportAdminModel.FromDate = ReportAdminModel.FromMonthDate;
                    }
                    else
                    {
                        ReportAdminModel.FromDate = ReportAdminModel.FromWeekDate;
                    }
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
                            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(CommonConstants.authScheme, Request.Cookies["Token"].ToString());
                            response = await _client.GetAsync(_client.BaseAddress + "/Report/" + ReportAdminModel.Status + "/" + ReportAdminModel.Type + "/" + ReportAdminModel.FromDate + "/" + ReportAdminModel.ToDate);
                            if (response.StatusCode == HttpStatusCode.OK)
                            {
                                string data = response.Content.ReadAsStringAsync().Result;
                                var result = JsonConvert.DeserializeObject<AdminReportModel>(data);
                                TopContributors = result.topContributor;
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
                                return Page();
                            }
                        }
                    }
                }
                else
                {
                    TempData["Error"] = "From Date is required!";
                    return Page();
                }
            }
        }
    }
}
