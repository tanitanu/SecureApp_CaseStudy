using DXC.BlogConnect.DTO;
using DXC.BlogConnect.WebAPI.Models.Domain;
using DXC.BlogConnect.WebAPI.Services.Interfaces;
using DXC.BlogConnect.WebApp.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Security.Claims;
using static DXC.BlogConnect.DTO.ErrorCode;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authentication;
using DXC.BlogConnect.WebApp.ServiceExtension;
/*
* Created By: Kishore
*/
namespace DXC.BlogConnect.WebApp.Pages
{
    public class LoginModel : PageModel
    {
        private readonly IAuthService _authService;
        private readonly ILogger<LoginModel> _logger;
        public List<Error> ErrorList;
        public LoginModel(IAuthService authService, ILogger<LoginModel> logger)
        {
            _authService = authService;
            _logger = logger;
            ErrorList = new List<Error>();
        }
        [BindProperty]
        public UserLoginDTO UserLoginDTO { get; set; } = default!;
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                if (!ModelState.IsValid || UserLoginDTO == null)
                {
                    return Page();
                }


                var res = await _authService.LoginAsync(UserLoginDTO);
                if (res.ErrorMessages.Any())
                {
                    ErrorList = res.ErrorMessages.ToList();

                    return Page();
                }
                

                if (res.IsSuccess)
                {
                    string token = res.Result[0];
                    var handler = new JwtSecurityTokenHandler();
                    var jwt = handler.ReadJwtToken(token);
                    var userPrincipal = new ClaimsPrincipal(
                        new ClaimsIdentity(jwt.Claims, "myClaims")
                    );

                    // Set current principal
                    Thread.CurrentPrincipal = userPrincipal;

                    await HttpContext.SignInAsync(userPrincipal);

                    Response.Cookies.Append("bearer", token);

;                    _logger.LogInformation("User Login a new account with username and password.");

                    return RedirectToPage("./Index");
                }
                else
                    return Page();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                ErrorList.Add(new Error("ex", "Unhandled error"));
            }
            return Page();
        }
    }
}
