using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Net;
using System;
using DiscussionForumUserInterface.Common;
using DiscussionForumUserInterface.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text;

namespace DiscussionForumUserInterface.Pages.Admin
{
    public class CreateDiscussionModel : PageModel
    {
        /// <summary author="Kirti Garg">
        /// This is create discussion page for admin where admin can create discussion.
        /// </summary>
        [BindProperty]
        public Models.CreateUpdateDiscussionModel CreateUpdateDiscussionModel { get; set; }
        private readonly ILogger<CreateDiscussionModel> _logger;
        private readonly HttpClient _client;
        public SelectList Categories { get; set; }
        static SelectList CategoriesList { get; set; }
        public CreateDiscussionModel(ILogger<CreateDiscussionModel> logger)
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
                    HttpResponseMessage response = await _client.GetAsync(_client.BaseAddress + "/Question/GetCategories");
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        string data = response.Content.ReadAsStringAsync().Result;
                        var result = JsonConvert.DeserializeObject<List<Catgory>>(data);
                        this.Categories = new SelectList(result, "CategoryId", "Category");
                        CategoriesList = Categories;
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
                        CreateUpdateDiscussionModel.Status = "Open";
                        HttpContent discussionContent = new StringContent(JsonConvert.SerializeObject(CreateUpdateDiscussionModel), Encoding.UTF8, "application/json");
                        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(CommonConstants.authScheme, Request.Cookies["Token"].ToString());
                        response = await _client.PostAsync(_client.BaseAddress + "/Question", discussionContent);
                        if (response.StatusCode == HttpStatusCode.Created)
                        {
                            string data = response.Content.ReadAsStringAsync().Result;
                            var result = JsonConvert.DeserializeObject<CreateUpdateDiscussionModel>(data);
                            TempData["Success"] = "Discussion Post has been created successfully";
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
                            Categories = CategoriesList;
                            return Page();
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
