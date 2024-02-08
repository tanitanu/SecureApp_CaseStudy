using DiscussionForumUserInterface.Common;
using DiscussionForumUserInterface.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Reflection;
using System.Security.Claims;
using System.Text;

namespace DiscussionForumUserInterface.Pages
{
    public class SecureLoginModel : PageModel
    {
        [BindProperty]
        public Models.LoginViewModel LoginViewModel { get; set; }
        private readonly ILogger<SecureLoginModel> _logger;
        private readonly HttpClient _client;
        public SecureLoginModel(ILogger<SecureLoginModel> logger)
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
            if (LoginViewModel.Code != null && LoginViewModel.Code != string.Empty)
            {
                LoginViewModel.Email = TempData.Peek("Email").ToString();
                LoginViewModel.Password = TempData.Peek("Password").ToString();
                TempData.Keep("Email");
                TempData.Keep("Password");
                HttpContent registerSecureContent = new StringContent(JsonConvert.SerializeObject(LoginViewModel), Encoding.UTF8, "application/json");
                HttpResponseMessage secureResponse = await _client.PostAsync(_client.BaseAddress + "/Authenticate/SecureLogin", registerSecureContent);
                if (secureResponse.StatusCode == HttpStatusCode.OK)
                {
                    var dataSecure = secureResponse.Content.ReadAsStringAsync().Result;
                    var secureLoginValues = JsonConvert.DeserializeObject<SecureLoginViewModel>(dataSecure);
                    var tokenString = CommonConstants.authScheme + " " + secureLoginValues?.Token;
                    var jwtEncodedString = tokenString.Substring(7);
                    var token = new JwtSecurityToken(jwtEncodedString);
                    Response.Cookies.Append("Email", token.Claims.First(c => c.Type == ClaimTypes.Email).Value);
                    Response.Cookies.Append("Id", token.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value);
                    Response.Cookies.Append("Role", token.Claims.First(c => c.Type == ClaimTypes.Role).Value);
                    Response.Cookies.Append("UserName", token.Claims.First(c => c.Type == ClaimTypes.GivenName).Value);
                    Response.Cookies.Append("Token", secureLoginValues.Token);
                    Response.Cookies.Append("RefreshToken", secureLoginValues.RefreshToken);
                    Response.Cookies.Append("Expiration", token.ValidTo.ToString());
                    SecureLoginViewModel secureDecodeValues = new SecureLoginViewModel()
                    {
                        Email = token.Claims.First(c => c.Type == ClaimTypes.Email).Value,
                        Id = token.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value,
                        Expiration = token.ValidTo,
                        Role = token.Claims.First(c => c.Type == ClaimTypes.Role).Value,
                        UserName = token.Claims.First(c => c.Type == ClaimTypes.GivenName).Value,
                        Token = secureLoginValues?.Token,
                        RefreshToken = secureLoginValues?.RefreshToken
                    };
                    if (secureDecodeValues.Role == "Admin")
                    {
                        TempData["Email"] = null;
                        TempData["Password"] = null;
                        return RedirectToPage("Admin/Discussions");
                    }
                    if (secureDecodeValues.Role == "User")
                    {
                        TempData["Email"] = null;
                        TempData["Password"] = null;
                        return RedirectToPage("User/Discussions");
                    }
                }
                else if (secureResponse.StatusCode == HttpStatusCode.InternalServerError)
                {
                    return RedirectToPage("Error");
                }
                else
                {
                    string data = secureResponse.Content.ReadAsStringAsync().Result;
                    var result = JsonConvert.DeserializeObject<ResultModel>(data);
                    TempData["Error"] = result?.Message;
                    return RedirectToPage();
                }
            }
            TempData["Error"] = "The field Code is required!";
            return Page();
        }

        public async Task<IActionResult> OnPostResend(LoginViewModel LoginViewModel)
        {
            LoginViewModel.Email = TempData.Peek("Email").ToString();
            LoginViewModel.Password = TempData.Peek("Password").ToString();
            TempData.Keep("Email");
            TempData.Keep("Password");
            HttpResponseMessage secureResponse = await _client.PostAsync(_client.BaseAddress + $"/Authenticate/ResendOTP/{LoginViewModel.Email}", null);
            if (secureResponse.StatusCode == HttpStatusCode.OK)
            {
                string data = secureResponse.Content.ReadAsStringAsync().Result;
                var result = JsonConvert.DeserializeObject<ResultModel>(data);
                TempData["Success"] = result?.Message;
                LoginViewModel.Code = string.Empty;
                return RedirectToPage();
            }
            else if (secureResponse.StatusCode == HttpStatusCode.InternalServerError)
            {
                return RedirectToPage("Error");
            }
            else
            {
                string data = secureResponse.Content.ReadAsStringAsync().Result;
                var result = JsonConvert.DeserializeObject<ResultModel>(data);
                TempData["Error"] = result?.Message;
                LoginViewModel.Code = string.Empty;
                return Page();
            }
        }
    }
}
