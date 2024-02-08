using BlazorCARS.HealthShield.DAL.UnitOfWork;
using BlazorCARS.HealthShield.DataObject.DTO;
using BlazorCARS.HealthShield.Utility;
using BlazorCARS.HealthShield.Utility.Extensions;
using BlazorCARS.HealthShield.Utility.Response;
using Microsoft.AspNetCore.Mvc;
using System.Net;
/*
  Created by JAYaseelan
 */
namespace BlazorCARS.HealthShield.WebAPI.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        protected APIResponse _response;

        public AuthController(IUnitOfWork unitOfWork, ILogger<AuthController> logger)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _response = new();
        }

        [HttpPost]
        [Route("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> LoginAsync(UserDTO userStoreDTO)
        {
            try
            {
                if (userStoreDTO == null)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    _response.ErrorMessages.Add("User request object must not be empty.");
                    _logger.LogWarning(string.Join(", ", _response.ErrorMessages));
                    return BadRequest(_response);
                }
                string result = userStoreDTO.UserName.NullOrWhiteSpace("User Name");
                if (result != string.Empty)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    _response.ErrorMessages.Add(result);
                    _logger.LogWarning(string.Join(", ", _response.ErrorMessages));
                    return BadRequest(_response);
                }
                result = userStoreDTO.Password.NullOrWhiteSpace("Password");
                if (result != string.Empty)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    _response.ErrorMessages.Add(result);
                    _logger.LogWarning(string.Join(", ", _response.ErrorMessages));
                    return BadRequest(_response);
                }

                var userModel = await _unitOfWork.UserStore.GetByUserNameAsync(userStoreDTO.UserName);
                if (userModel == null)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    _response.ErrorMessages.Add("User doesn't exist");
                    _logger.LogWarning(string.Join(", ", _response.ErrorMessages));
                    return BadRequest(_response);
                }
                else if (userModel != null && userModel.IsDeleted)
                {
                    _response.StatusCode = HttpStatusCode.NoContent;
                    _response.IsSuccess = false;
                    _response.ErrorMessages.Add("User login is in deactivated state.");
                    _logger.LogWarning(string.Join(", ", _response.ErrorMessages));
                    return NotFound(_response);
                }
                var userRole = await _unitOfWork.UserRole.GetByIdAsync(x => x.UserRoleId == userModel.UserRoleId, tracked: false);
                if (userRole == null)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    _response.ErrorMessages.Add("User role doesn't exist");
                    _logger.LogWarning(string.Join(", ", _response.ErrorMessages));
                    return BadRequest(_response);
                }
                else if (userRole != null && userRole.IsDeleted)
                {
                    _response.StatusCode = HttpStatusCode.NoContent;
                    _response.IsSuccess = false;
                    _response.ErrorMessages.Add("User role is in deactivated state.");
                    _logger.LogWarning(string.Join(", ", _response.ErrorMessages));
                    return NotFound(_response);
                }
                //Encrypt the password
                string encryptedPassword = CommonFunctions.EncryptPassword(userStoreDTO.Password);
                if (encryptedPassword == string.Empty)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    _response.ErrorMessages.Add("Password must not be empty.");
                    _logger.LogWarning(string.Join(", ", _response.ErrorMessages));
                    return BadRequest(_response);
                }
                if (encryptedPassword != userModel.Password)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    _response.ErrorMessages.Add("Password is incorrect.");
                    _logger.LogWarning(string.Join(", ", _response.ErrorMessages));
                    return BadRequest(_response);
                }

                string userToken = await _unitOfWork.TokenHandler.GenerateTokenAsync(userModel, userRole.Name);
                LoginResponseDTO loginResponseDTO = new()
                {
                    //FullName = //TODO : Fetch Fullname
                    UserName = userModel.UserName,
                    UserRole = userRole.Name,
                    DiscriminationId = userModel.DiscriminationId,
                    Token = userToken
                };
                _response.StatusCode = HttpStatusCode.OK;
                _response.IsSuccess = true;
                _response.Result = loginResponseDTO;//userToken;
                return Ok(_response);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.IsSuccess = false;
                _response.ErrorMessages.Add(ex.ToString());
            }
            return _response;
        }
    }
}
