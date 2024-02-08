using AutoMapper;
using DXC.BlogConnect.DTO;
using DXC.BlogConnect.WebAPI.Models;
using DXC.BlogConnect.WebAPI.Services.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using static DXC.BlogConnect.DTO.ErrorCode;
using System.Net;
using DXC.BlogConnect.WebAPI.Utilities;

/*
* Created By: Kishore
*/
namespace DXC.BlogConnect.WebAPI.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ITokenHandlerService _tokenService;
        private readonly IUserService _userService;
        private readonly IValidator<UserLoginDTO> _userValidator;
        private readonly ILogger<UserController> _logger;
        private readonly List<Error> _errorMessages;
        public AuthController(ITokenHandlerService tokenService, IMapper mapper, IValidator<UserLoginDTO> userValidator, ILogger<UserController> logger, IUserService userService)
        {
            _tokenService = tokenService;
            _userValidator = userValidator;
            _errorMessages = new List<Error>();
            _logger = logger;
            _userService = userService;
        }

        [HttpPost]
        [Route("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> LoginAsync(UserLoginDTO userLoginDTO)
        {
            var _response = new APIResponse<string>();

            try
            {
                var validationResult = _userValidator.Validate(userLoginDTO);
                if (!validationResult.IsValid)
                {
                    var errors = validationResult.Errors.Select(e => new Error(e.ErrorCode, e.ErrorMessage));
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    _response.ErrorMessages = errors;
                    _logger.LogWarning(string.Join(", ", _response.ErrorMessages));
                    return BadRequest(_response);
                }
                var userModel = await _userService.GetUserByUserName(userLoginDTO.UserName);
                if (userModel == null)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    _errorMessages.Add(new Error("user_name", "User doesn't exist."));
                    _response.ErrorMessages = _errorMessages;
                    _logger.LogWarning(string.Join(", ", _response.ErrorMessages));
                    return BadRequest(_response);
                }
                string encryptedPassword = CommonFunctions.EncryptPassword(userLoginDTO.Password);
                if (encryptedPassword != userModel.Password)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    _errorMessages.Add(new Error("password", "Password is incorrect."));
                    _response.ErrorMessages = _errorMessages;
                    _logger.LogWarning(string.Join(", ", _response.ErrorMessages));
                    return BadRequest(_response);
                }

                string userToken = await _tokenService.GetTokenAsync(userModel);
                var lstString = new List<string>
                {
                    userToken
                };
                _response.StatusCode = HttpStatusCode.OK;
                _response.IsSuccess = true;
                _response.Result = lstString;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.IsSuccess = false;
                _errorMessages.Add(new Error("ex", ex.ToString()));
                _response.ErrorMessages = _errorMessages;
                _response.ErrorMessages = _errorMessages;
            }

            return Ok(_response);
        }
    }
}
