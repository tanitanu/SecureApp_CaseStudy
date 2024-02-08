using Azure.Core;
using MeetingScheduler.Common;
using MeetingScheduler.Entities;
using MeetingScheduler.Model;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;

namespace MeetingScheduler.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger; //logger object
        private readonly IUserHandler _userHandler; //user hadnler object

        /// <summary>
        /// User Controller constructor
        /// </summary>
        /// <param name="logger"></param>
        public UserController(ILogger<UserController> logger)
        {
            _logger = logger;
            _userHandler = new UserHandler();
        }

        /// <summary>
        /// Register user
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("Register")]
        public async Task<IActionResult> Register(UserRegister request)
        {
            try
            {
                _logger.LogInformation(Constants.REGISTER_START);
                var response= await _userHandler.Register(request);
                _logger.LogInformation(Constants.REGISTER_END);
                return Ok(response);
                
                
            }
            catch (Exception ex)
            {
                _logger.LogError(Constants.REGISTER_EXCEPTION + " \n" + ex.ToString());
                return BadRequest(ex.Message);
            }

        }

        /// <summary>
        /// Get login details
        /// </summary>
        /// <param name="userLogin"></param>
        /// <returns></returns>
        [HttpPost("Login")]
        public async Task<IActionResult> Login(UserLogin userLogin)
        {
            try
            {
                _logger.LogInformation(Constants.LOGIN_START);
                var response = await _userHandler.Login(userLogin);
                _logger.LogInformation(Constants.LOGIN_START);
                return Ok(response);
            }
            catch (Exception ex) 
            {
                _logger.LogError(Constants.LOGIN_EXCEPTION + " \n" + ex.ToString());
                return BadRequest(ex.Message);
            }
            
        }

        /// <summary>
        /// Get forgot password
        /// </summary>
        /// <param name="forgetPassword"></param>
        /// <returns></returns>
        [HttpPost("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword(ForgetPassword forgetPassword)
        {
            try
            {
                _logger.LogInformation(Constants.FORGOT_START);
                var response = await _userHandler.ForgotPassword(forgetPassword);
                _logger.LogInformation(Constants.FORGOT_END);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(Constants.FORGOT_EXCEPTION + " \n" + ex.ToString());
                return BadRequest(ex.Message);
            }

        }
        /// <summary>
        /// Reset password
        /// </summary>
        /// <param name="resetPasswordRequest"></param>
        /// <returns></returns>
        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword(ResetPasswordRequest resetPasswordRequest)
        {
            try
            {
                _logger.LogInformation(Constants.RESET_START);
                var response = await _userHandler.resetPassword(resetPasswordRequest);
                _logger.LogInformation(Constants.REGISTER_END);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(Constants.RESET_EXCEPTION + " \n" + ex.ToString());
                return BadRequest(ex.Message);
            }

        }

        /// <summary>
        /// Get all users details
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetUsers")]
        public async Task<IActionResult> GetUsers()
        {
            try
            {
                _logger.LogInformation(Constants.GETUSERS_START);
                var response = await _userHandler.GetUsers();
                _logger.LogInformation(Constants.GETUSERS_END);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(Constants.GETUSERS_EXCEPTION + " \n" + ex.ToString());
                return BadRequest(ex.Message);
            }

        }

        /// <summary>
        /// Update Role 
        /// </summary>
        /// <param name="role"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateRole(Role role, int id)
        {
            try
            {
                _logger.LogInformation(Constants.UPDATEDROLE_START);
                var response = await _userHandler.UpdateRole(id, role);
                _logger.LogInformation(Constants.UPDATEDROLE_END);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(Constants.UPDATEDROLE_EXCEPTION + " \n" + ex.ToString());
                return BadRequest(ex.Message);
            }
        }



    }
}
