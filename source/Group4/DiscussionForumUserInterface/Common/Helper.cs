using DiscussionForumUserInterface.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Web;

namespace DiscussionForumUserInterface.Common
{
    /// <summary author="Kirti Garg">
    /// This is helper class where we validate and refresh access token.
    /// </summary>
    public class Helper
    {
        Uri baseAddress = new Uri("https://localhost:7225/api");
        private readonly HttpClient _client;
        private readonly HttpContext _context ;
        public Helper(HttpContext context)
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
            _context = context;
        }

        public async Task<TokenViewModel> ValidateAndRefreshAccessToken(string token, string refreshToken)
        {
            TokenViewModel tokenViewModel = new TokenViewModel();
            var tokenExpTime = _context.Request.Cookies["Expiration"].ToString();
            DateTime localTokenExpTime = DateTime.SpecifyKind(Convert.ToDateTime(tokenExpTime), DateTimeKind.Utc).ToLocalTime();
            if (localTokenExpTime <= DateTime.Now.AddMinutes(1))
            {
                tokenViewModel.RefreshToken = refreshToken;
                tokenViewModel.AccessToken = token;
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(CommonConstants.authScheme, token);
                HttpContent tokenContent = new StringContent(JsonConvert.SerializeObject(tokenViewModel), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _client.PostAsync(_client.BaseAddress + "/Authenticate/RefreshToken", tokenContent);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    var result = JsonConvert.DeserializeObject<TokenViewModel>(data);
                    tokenViewModel = result;
                    if (tokenViewModel != null)
                    {
                        var tokenString = CommonConstants.authScheme + " " + tokenViewModel.AccessToken;
                        var jwtEncodedString = tokenString.Substring(7);
                        var tokens = new JwtSecurityToken(jwtEncodedString);
                        _context.Response.Cookies.Delete("Token");
                        _context.Response.Cookies.Delete("RefreshToken");
                        _context.Response.Cookies.Delete("Expiration");
                        _context.Response.Cookies.Append("Token", tokenViewModel.AccessToken);
                        _context.Response.Cookies.Append("RefreshToken", tokenViewModel.RefreshToken);
                        _context.Response.Cookies.Append("Expiration", tokens.ValidTo.ToString());

                    }
                }
                else
                {
                    tokenViewModel = new TokenViewModel();
                }
            }
            else
            {
                tokenViewModel.AccessToken = _context.Request.Cookies["Token"].ToString();
            }
            return tokenViewModel;
        }
    }
}
