using AutoMapper;
using DXC.BlogConnect.WebAPI.Models;
using DXC.BlogConnect.WebAPI.Models.Domain;
using DXC.BlogConnect.DTO;
using DXC.BlogConnect.WebAPI.Services.Interfaces;
using DXC.BlogConnect.WebAPI.Utilities;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;
using static DXC.BlogConnect.DTO.ErrorCode;
/*
* Created By: Kishore
*/
namespace DXC.BlogConnect.WebAPI.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly IValidator<UserDTO> _userValidator;
        private readonly ILogger<UserController> _logger;
        private readonly List<Error> _errorMessages;
        public UserController(IUserService userService, IMapper mapper, IValidator<UserDTO> userValidator, ILogger<UserController> logger)
        {
            _userService = userService;
            _mapper = mapper;
            _userValidator = userValidator;
            _errorMessages = new List<Error>();
            _logger = logger;
        }

        #region Get Data
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetAllAsync()
        {
            var _response = new APIResponse<UserGetDTO>();
            try
            {

                var model = await _userService.GetAllUserWithUserName();

                _response.Result = _mapper.Map<List<UserGetDTO>>(model);
                _response.StatusCode = HttpStatusCode.OK;
                _response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.IsSuccess = false;
                _errorMessages.Add(new Error("ex", ex.ToString()));
                _response.ErrorMessages = _errorMessages;
            }
            return Ok(_response);
        }
        #endregion

        #region Create/Update/Delete operations
        [HttpPost]
        [Route("Add")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> CreateAsync([FromBody] UserDTO createDTO)
        {
            var _response = new APIResponse<UserDTO>();
            try
            {
                var validationResult = _userValidator.Validate(createDTO);
                if (!validationResult.IsValid)
                {
                    var errors = validationResult.Errors.Select(e => new Error(e.ErrorCode, e.ErrorMessage));
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    _response.ErrorMessages = errors;
                    return BadRequest(_response);
                }

                var isUserExist = _userService.IsUserExistByUsername(createDTO.UserName);
                if (isUserExist)
                {
                    _response.StatusCode = HttpStatusCode.OK;
                    _response.IsSuccess = true;
                    _errorMessages.Add(new Error("user_name", "UserName already exist."));
                    _response.ErrorMessages = _errorMessages;
                    return BadRequest(_response);
                }
                var model = _mapper.Map<User>(createDTO);
                model.CreatedDate = DateTime.Now;
                model.UpdatedDate = DateTime.Now;
                model.CreatedBy = createDTO.UserName;
                model.UpdatedBy = createDTO.UserName;

                var encryptedPassword = CommonFunctions.EncryptPassword(createDTO.Password);
                model.Password = encryptedPassword;
                model.RoleId = (int)Utilities.Enums.UserRole.User;
                await _userService.AddUserAsync(model);


                _response.StatusCode = HttpStatusCode.OK;
                _response.IsSuccess = true;
                _errorMessages.Add(new Error("Success", "Saved Successfully"));
                _response.ErrorMessages = _errorMessages;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.IsSuccess = false;
                _errorMessages.Add(new Error("ex", ex.ToString()));
                _response.ErrorMessages = _errorMessages;
            }
            return Ok(_response);

        }

        /// <summary>
        /// Get user by id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet()]
        [Route("{id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetAsync(int id)
        {
            var _response = new APIResponse<UserDTO>();
            try
            {
                var userDetails = await _userService.GetUserListById(id);
                
                if (userDetails != null)
                {
                    _response.StatusCode = HttpStatusCode.OK;
                    _response.IsSuccess = true;
                    _response.Result = _mapper.Map<List<UserDTO>>(userDetails);
                    return Ok(_response);


                }
                else
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    _errorMessages.Add(new Error("user_name", "User doesn't exist."));
                    _response.ErrorMessages = _errorMessages;
                    return BadRequest(_response);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.IsSuccess = false;
                _errorMessages.Add(new Error("ex", ex.ToString()));
                _response.ErrorMessages = _errorMessages;
            }
            return Ok(_response);
        }
        [HttpPut]
        [Route("edit/{id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> UpdateAsync([FromRoute] int id, [FromBody] UserDTO updateDTO)
        {
            var _response = new APIResponse<UserDTO>();
            try
            {
                var validationResult = _userValidator.Validate(updateDTO);
                if (!validationResult.IsValid)
                {
                    var errors = validationResult.Errors.Select(e => new Error(e.ErrorCode, e.ErrorMessage));
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    _response.ErrorMessages = errors;
                    return BadRequest(_response);
                }

                var existingUserModel = await _userService.GetUserById(id);

                if (existingUserModel == null)
                {

                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.IsSuccess = false;
                    _errorMessages.Add(new Error("user_name", "User doesn't exist."));
                    _response.ErrorMessages = _errorMessages;
                    return NotFound(_response);

                }

                existingUserModel.UpdatedDate = DateTime.Now;
                existingUserModel.UpdatedBy = updateDTO.UserName;
                existingUserModel.FirstName = updateDTO.FirstName;
                existingUserModel.LastName = updateDTO.LastName;
                existingUserModel.EmailId = updateDTO.EmailId;

                _userService.UpdateUser(existingUserModel);
                _response.StatusCode = HttpStatusCode.OK;
                _errorMessages.Add(new Error("user_name", "User details updated successfully"));
                _response.ErrorMessages = _errorMessages;
                _response.IsSuccess = true;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.IsSuccess = false;
                _errorMessages.Add(new Error("ex", ex.ToString()));
                _response.ErrorMessages = _errorMessages;
            }
            return Ok(_response);
        }

        [HttpDelete]
        [Route("{id:int}")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteAync(int id)
        {
            var _response = new APIResponse<UserDTO>();
            try
            {
                var isUserDeleted = await _userService.DeleteUser(id);

                if (isUserDeleted)
                {
                    _response.StatusCode = HttpStatusCode.OK;
                    _errorMessages.Add(new Error("user_name", "User deleted successfully"));
                    _response.ErrorMessages = _errorMessages;
                    _response.IsSuccess = true;
                    return Ok(_response);

                }
                else
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.IsSuccess = false;
                    _errorMessages.Add(new Error("user_name", "User doesn't exist."));
                    _response.ErrorMessages = _errorMessages;
                    return NotFound(_response);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.IsSuccess = false;
                _errorMessages.Add(new Error("ex", ex.ToString()));
                _response.ErrorMessages = _errorMessages;
            }
            return Ok(_response);
        }
        #endregion
    }
}
