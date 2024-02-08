using AutoMapper;
using DiscussionForumAPI.Auth;
using DiscussionForumAPI.Contracts;
using DiscussionForumAPI.Models;
using DiscussionForumAPI.Models.DTO;
using DiscussionForumAPI.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Mail;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.WebUtilities;
using DiscussionForumAPI.Helper;
using static System.Net.Mime.MediaTypeNames;
using System.Drawing;

namespace DiscussionForumAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly DiscussionForumContext _context;
        private readonly ILogger<AuthenticateController> _logger;
        private readonly IToken _token;
        private readonly IMapper _mapper;
        private readonly IMail _mail;


        public AuthenticateController(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IConfiguration configuration, DiscussionForumContext context, ILogger<AuthenticateController> logger,
            IToken token, IMapper mapper, IMail mail)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _context = context;
            _logger = logger;
            _token = token;
            _mapper = mapper;
            _mail = mail;
        }
        /// <summary Author = Kirti Garg>
        /// This method generates a random 6-digit number which used as a code to authenticate.
        /// </summary>
        /// <returns></returns>
        private static string GenerateNewRandom()
        {
            Random generator = new Random();
            String r = generator.Next(0, 1000000).ToString("D6");
            if (r.Distinct().Count() == 1)
            {
                r = GenerateNewRandom();
            }
            return r;
        }

        /// <summary Author = Kirti Garg>
        /// This method use to validate login credentials and if it is successful it
        /// generate code and mail to user email.It is a single layer of authentication.
        /// </summary>
        /// <param name="model">LoginModel</param>
        /// <returns>It will return details on successful validation</returns>

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            bool? activeUser = _context.AspNetUsers.Where(x => x.Id == user.Id).Select(f => f.IsActive).FirstOrDefault();
            if (activeUser == false)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new Response { Status = "Error", Message = "Account is In-active. Please contact admin!" });
            }
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                String code = GenerateNewRandom();
                var twoFactorCodeObj = await _token.GetByIdAsync(user.Id);
                DisussionForumUserTwoFactorCode userTwoFactorCode = new DisussionForumUserTwoFactorCode();
                userTwoFactorCode.Code = code;
                userTwoFactorCode.UserTwoFactorCodeId = Guid.NewGuid().ToString();
                userTwoFactorCode.UserId = user.Id;
                twoFactorCodeObj = await _token.CreateUpdateAsync(user.Id, userTwoFactorCode);
                var twoFactorCode = _mapper.Map<DisussionForumUserTwoFactorCodeDTO>(twoFactorCodeObj);
                if (twoFactorCode != null)
                {
                    string subject = "Verification Code";
                    string body = "<div style=\"padding: 20px; background-color: rgb(255, 255, 255);\"><div style=\"color: rgb(0, 0, 0); text-align: left;\"><h1 style=\"margin: 1rem 0\">Verification code</h1><p style=\"padding-bottom: 16px\">Please use the verification code below to sign in.</p><p style=\"padding-bottom: 16px\"><strong style=\"font-size: 130%\">" + twoFactorCode.Code + "</strong></p><p style=\"padding-bottom: 16px\">If you didn’t request this, you can ignore this email.</p><p style=\"padding-bottom: 16px\">Thanks,<br>The ConvoVerse Team</p></div></div>";
                    MailData mailData = new MailData();
                    mailData.EmailToId = user.Email;
                    mailData.EmailToName = user.UserName;
                    mailData.EmailSubject = subject;
                    mailData.EmailBody = body;
                    bool isSuccessful = _mail.SendMailAsync(mailData);
                    return Ok(new
                    {
                        Email = model.Email,
                    });
                }
                return StatusCode(StatusCodes.Status400BadRequest, new Response { Status = "Error", Message = "Something went wrong! We are looking into resolving this." });
            }
            return StatusCode(StatusCodes.Status400BadRequest, new Response { Status = "Error", Message = "Either Email Or Password Is Incorrect." });
        }

        /// <summary Author = Kirti Garg>
        /// This method use to validate login credentials along with the code that makes a two-factor authentication security.
        /// On successful authentication, it geneartes access-token along with refresh token.
        /// </summary>
        /// <param name="model">LoginModel</param>
        /// <returns>It return token details on successful validation</returns>
        [HttpPost]
        [Route("SecureLogin")]
        public async Task<IActionResult> SecureLogin([FromBody] LoginModel model)
        {
            if (model.Code != null && model.Code != string.Empty)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
                {
                    var twoFactorCodeObj = await _token.GetByIdAsync(user.Id);
                    if (twoFactorCodeObj != null)
                    {
                        var twoFactorCode = _mapper.Map<DisussionForumUserTwoFactorCodeDTO>(twoFactorCodeObj);
                        if (twoFactorCode.Code == model.Code)
                        {
                            twoFactorCodeObj = await _token.UpdateAsync(user.Id);
                            if (twoFactorCodeObj != null)
                            {
                                var roles = await _userManager.GetRolesAsync(user);


                                var token = _token.CreateJWTToken(user, roles.ToList());
                                var refreshToken = _token.GenerateRefreshToken();

                                _ = int.TryParse(_configuration["JWT:RefreshTokenValidityInDays"], out int refreshTokenValidityInDays);

                                user.RefreshToken = refreshToken;
                                user.RefreshTokenExpiryTime = DateTime.Now.AddDays(refreshTokenValidityInDays);

                                await _userManager.UpdateAsync(user);

                                return Ok(new
                                {
                                    Token = token,
                                    RefreshToken = refreshToken,
                                    Expiration = DateTime.UtcNow.AddMinutes(60)
                                });
                            }
                            else
                            {
                                return StatusCode(StatusCodes.Status400BadRequest, new Response { Status = "Error", Message = "Something went wrong! We are looking into resolving this." });
                            }
                        }
                        else
                        {
                            return StatusCode(StatusCodes.Status400BadRequest, new Response { Status = "Error", Message = "OTP is not matching." });
                        }
                    }
                    return StatusCode(StatusCodes.Status400BadRequest, new Response { Status = "Error", Message = "Something went wrong! We are looking into resolving this." });
                }
                return StatusCode(StatusCodes.Status400BadRequest, new Response { Status = "Error", Message = "Either Email Or Password Is Incorrect." });

            }
            else
            {
                return StatusCode(StatusCodes.Status400BadRequest, new Response { Status = "Error", Message = "Parameter Code is Required." });
            }
        }

        /// <summary Author = Kirti Garg>
        /// This method use to register user and add Roles As User in UserRoles.
        /// </summary>
        /// <param name="model">RegisterModel</param>
        /// <returns>It will return user successfully created on successful creation of user</returns>
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            var userExists = await _userManager.FindByEmailAsync(model.Email);

            if (userExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User already exists!" });
            string username = string.Empty;
            ApplicationUser user = new()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Firstname
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });
            if (await _roleManager.RoleExistsAsync(UserRoles.User))
            {
                await _userManager.AddToRoleAsync(user, UserRoles.User);
            }
            return Ok(new Response { Status = "Success", Message = "User created successfully!" });
        }

        /// <summary Author = Kirti Garg>
        /// This method use to register admin and add Roles As Admin in UserRoles.
        /// </summary>
        /// <param name="model">RegisterModel</param>
        /// <returns>It will return admin successfully created on successful creation of admin</returns>
        [HttpPost]
        [Route("RegisterAdmin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] RegisterModel model)
        {
            var userExists = await _userManager.FindByEmailAsync(model.Email);
            if (userExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "Admin already exists!" });
            ApplicationUser user = new()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Firstname
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "Admin creation failed! Please check user details and try again." });

            if (!await _roleManager.RoleExistsAsync(UserRoles.Admin))
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
            if (!await _roleManager.RoleExistsAsync(UserRoles.User))
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.User));

            if (await _roleManager.RoleExistsAsync(UserRoles.Admin))
            {
                await _userManager.AddToRoleAsync(user, UserRoles.Admin);
            }
            return Ok(new Response { Status = "Success", Message = "Admin created successfully!" });
        }

        /// <summary Author = Kirti Garg>
        /// This method use to create new access token and refresh token when access token gets expired, so user don't need to re-login.
        /// </summary>
        /// <param name="tokenModel">TokenModel</param>
        /// <returns>It will return token details on successful execution</returns>
        [HttpPost]
        [Route("RefreshToken")]
        public async Task<IActionResult> RefreshToken(TokenModel tokenModel)
        {
            if (tokenModel is null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new Response { Status = "Error", Message = "Invalid client request." });
            }

            string? accessToken = tokenModel.AccessToken;
            string? refreshToken = tokenModel.RefreshToken;

            var principal = _token.GetPrincipalFromExpiredToken(accessToken);
            if (principal == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new Response { Status = "Error", Message = "Invalid access token or refresh token." });
            }
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            string username = principal.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email).Value;
#pragma warning restore CS8602 // Dereference of a possibly null reference.
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.

#pragma warning disable CS8604 // Possible null reference argument.
            var user = await _userManager.FindByEmailAsync(username);
#pragma warning restore CS8604 // Possible null reference argument.

            if (user == null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new Response { Status = "Error", Message = "Invalid access token or refresh token." });
            }
            var roles = await _userManager.GetRolesAsync(user);
            var newAccessToken = _token.CreateJWTToken(user, roles.ToList());
            var newRefreshToken = _token.GenerateRefreshToken();

            user.RefreshToken = newRefreshToken;
            await _userManager.UpdateAsync(user);

            return new ObjectResult(new
            {
                accessToken = newAccessToken,
                refreshToken = newRefreshToken
            });
        }

        /// <summary Author = Kirti Garg>
        /// It will revoke refresh token if refresh token gets tampered.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        [Route("Revoke/{email}")]
        public async Task<IActionResult> Revoke(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null) return BadRequest("Invalid user name");

            user.RefreshToken = null;
            await _userManager.UpdateAsync(user);

            return NoContent();
        }

        /// <summary Author = Kirti Garg>
        /// It will resend otp, if otp didn't deliver the user.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("ResendOTP/{email}")]
        public async Task<IActionResult> ResendOTP(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user != null)
            {
                String code = GenerateNewRandom();
                var twoFactorCodeObj = await _token.GetByIdAsync(user.Id);
                DisussionForumUserTwoFactorCode userTwoFactorCode = new DisussionForumUserTwoFactorCode();
                userTwoFactorCode.Code = code;
                userTwoFactorCode.UserTwoFactorCodeId = Guid.NewGuid().ToString();
                userTwoFactorCode.UserId = user.Id;
                twoFactorCodeObj = await _token.CreateUpdateAsync(user.Id, userTwoFactorCode);
                var twoFactorCode = _mapper.Map<DisussionForumUserTwoFactorCodeDTO>(twoFactorCodeObj);
                if (twoFactorCode != null)
                {
                    string subject = "Verification Code";
                    string body = "<div style=\"padding: 20px; background-color: rgb(255, 255, 255);\"><div style=\"color: rgb(0, 0, 0); text-align: left;\"><h1 style=\"margin: 1rem 0\">Verification code</h1><p style=\"padding-bottom: 16px\">Please use the verification code below to sign in.</p><p style=\"padding-bottom: 16px\"><strong style=\"font-size: 130%\">" + twoFactorCode.Code + "</strong></p><p style=\"padding-bottom: 16px\">If you didn’t request this, you can ignore this email.</p><p style=\"padding-bottom: 16px\">Thanks,<br>The ConvoVerse Team</p></div></div>";
                    MailData mailData = new MailData();
                    mailData.EmailToId = user.Email;
                    mailData.EmailToName = user.UserName;
                    mailData.EmailSubject = subject;
                    mailData.EmailBody = body;
                    bool isSuccessful = _mail.SendMailAsync(mailData);
                    return StatusCode(StatusCodes.Status200OK, new Response { Status = "Success", Message = "OTP has been successfully emailed to you." });
                }
                return StatusCode(StatusCodes.Status400BadRequest, new Response { Status = "Error", Message = "Something went wrong! We are looking into resolving this." });

            }
            return StatusCode(StatusCodes.Status400BadRequest, new Response { Status = "Error", Message = "Either Email Or Password Is Incorrect." });

        }

        /// <summary Author = Kirti Garg>
        /// It will email the link of forgot password to the user.
        /// </summary>
        /// <param name="email"></param>
        /// <param name="clientURI"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("ForgotPassword/{email}")]
        public async Task<IActionResult> ForgotPassword(string email, string clientURI)
        {
            if (clientURI != null || clientURI != string.Empty)
            {
                var user = await _userManager.FindByEmailAsync(email);
                if (user == null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new Response { Status = "Error", Message = "User account don't exists!" });
                }
                else
                {
                    var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                    var param = new Dictionary<string, string>
    {
        {"token", token },
        {"email", email }
    };

                    var callback = QueryHelpers.AddQueryString(clientURI, param);
                    string subject = "Reset Password";
                    string body = "<div style=\"padding:20px; background - color: rgb(255, 255, 255);\"><div style=\"color: rgb(0, 0, 0); text - align: left;\"><h1 style=\"margin: 1rem 0\">Trouble signing in?</h1><p style=\"padding - bottom: 16px\">We've received a request to reset the password for this user account.</p><p style=\"padding - bottom: 16px\"><a href="+callback+" style=\"padding: 12px 24px; border-radius: 4px; color: #FFF; background: #2B52F5;display: inline-block;margin: 0.5rem 0;\">Reset your password</a></p><p style=\"padding-bottom: 16px\">If you didn't ask to reset your password, you can ignore this email.</p><p style=\"padding-bottom: 16px\">Thanks,<br>The ConvoVerse Team</p></div></div>";
                    MailData mailData = new MailData();
                    mailData.EmailToId = email;
                    mailData.EmailToName = user.UserName;
                    mailData.EmailSubject = subject;
                    mailData.EmailBody = body;
                    bool isSuccessful = _mail.SendMailAsync(mailData);
                    if (isSuccessful)
                    {
                        return StatusCode(StatusCodes.Status200OK, new Response { Status = "Success", Message = "Reset Password link has been successfully emailed to you." });
                    }
                    else
                    {
                        return StatusCode(StatusCodes.Status400BadRequest, new Response { Status = "Error", Message = "Please try after some time!" });
                    }
                }
            }
            else
            {
                return StatusCode(StatusCodes.Status400BadRequest, new Response { Status = "Error", Message = "Client URI is required" });
            }
        }

        /// <summary Author = Kirti Garg>
        /// This method uses to change the password of the user.
        /// </summary>
        /// <param name="forgotPassword"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("ResetPassword")]
        public async Task<IActionResult> ResetPassword([FromBody] ForgotPassword forgotPassword)
        {
            var user = await _userManager.FindByEmailAsync(forgotPassword.Email);
            if (user == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new Response { Status = "Error", Message = "User account don't exists!" });
            }

            var resetPassResult = await _userManager.ResetPasswordAsync(user, forgotPassword.Token, forgotPassword.Password);
            if (!resetPassResult.Succeeded)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new Response { Status = "Error", Message = resetPassResult.Errors.FirstOrDefault().Description });
            }

            return StatusCode(StatusCodes.Status200OK, new Response { Status = "Success", Message = "Password Changed Successfully!" });
        }


    }
}
