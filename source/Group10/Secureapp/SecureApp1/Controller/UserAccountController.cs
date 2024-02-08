using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SecureApp.SecureAppModel;

namespace SecureApp.Controllers
{
    public class UserAccountController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        public UserAccountController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }
        [HttpPost("Registration")]
        public async Task<IActionResult> RegisterUser([FromBody] UserForRegistrationDto userForRegistration)
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
    }
}
