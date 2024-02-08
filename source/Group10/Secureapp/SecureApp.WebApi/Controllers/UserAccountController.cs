using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SecureApp.Model.SecureAppModel;
using SecureApp.SecureAppModel;

namespace SecureApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAccountController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        public UserAccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        [HttpPost("Registration")]
        public async Task<IActionResult> RegisterUser(UserForRegistrationDto userForRegistration)
        {
            if (userForRegistration == null || !ModelState.IsValid)
                return BadRequest();
            var user = new IdentityUser { UserName = userForRegistration.Email, Email = userForRegistration.Email };

            var result = await _userManager.CreateAsync(user, userForRegistration.Password);
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description);
                return BadRequest(new RegistrationResponseDto { Errors = errors });
            }

            return StatusCode(201);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(UserLoginDto userLogin)
        {
            if (userLogin == null || !ModelState.IsValid)
                return BadRequest();

            var result = await _signInManager.PasswordSignInAsync(userLogin.Email, userLogin.Password, true, false);
            if (!result.Succeeded)
            {
                return BadRequest(new LoginResponseDto { Errors = "Invalid UserName or Password" });
            }

            return StatusCode(201);
        }
    }
}
