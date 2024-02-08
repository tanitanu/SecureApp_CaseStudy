using AutoMapper;
using DXC.BlogConnect.WebAPI.Models;
using DXC.BlogConnect.WebAPI.Models.Domain;
using DXC.BlogConnect.DTO;
using DXC.BlogConnect.WebAPI.Services;
using DXC.BlogConnect.WebAPI.Services.Interfaces;
using DXC.BlogConnect.WebAPI.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using static DXC.BlogConnect.DTO.ErrorCode;
using DXC.BlogConnect.WebAPI.Data.FluentValidation;
using FluentValidation;
/*
* Created By: Kishore
*/
namespace DXC.BlogConnect.WebAPI.Controllers
{
    [Route("api/userroles")]
    [ApiController]
    [Authorize]
    public class UserRoleController : Controller
    {
        public readonly IUserRoleService _userRoleService;
        private readonly IMapper _mapper;
        private readonly ILogger<UserRoleController> _logger;
        private readonly List<Error> _errorMessages;
        private readonly IValidator<RoleAddDTO> _roleValidator;
        public UserRoleController(IUserRoleService userRoleService, IMapper mapper, ILogger<UserRoleController> logger, IValidator<RoleAddDTO> roleValidator)
        {
            _userRoleService = userRoleService;
            _mapper = mapper;
            _errorMessages = new List<Error>();
            _logger = logger;
            _roleValidator = roleValidator;
        }

        [HttpGet]

        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetAllAsync()
        {
            var _response = new APIResponse<UserRoleDTO>();
            try
            {
                IEnumerable<Role> model = await _userRoleService.GetAllRoleAsync();
                var result = _mapper.Map<List<UserRoleDTO>>(model);
                _response.Result = result;
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

        [HttpPost]
        [Route("Add")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> CreateAsync([FromBody] RoleAddDTO createDTO)
        {
            var _response = new APIResponse<UserDTO>();
            try
            {
                var validationResult = _roleValidator.Validate(createDTO);
                if (!validationResult.IsValid)
                {
                    var errors = validationResult.Errors.Select(e => new Error(e.ErrorCode, e.ErrorMessage));
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    _response.ErrorMessages = errors;
                    return BadRequest(_response);
                }

                var isUserExist = _userRoleService.IsRoleExistByRolename(createDTO.RoleName);
                if (isUserExist)
                {
                    _response.StatusCode = HttpStatusCode.OK;
                    _response.IsSuccess = true;
                    _errorMessages.Add(new Error("user_name", "UserName already exist."));
                    _response.ErrorMessages = _errorMessages;
                    return BadRequest(_response);
                }
                var model = _mapper.Map<Role>(createDTO);
                model.CreatedDate = DateTime.Now;
                model.UpdatedDate = DateTime.Now;
                model.CreatedBy = createDTO.UserName;
                model.UpdatedBy = createDTO.UserName;

                await _userRoleService.AddRoleAsync(model);


                _response.StatusCode = HttpStatusCode.OK;
                _response.IsSuccess = true;
                _errorMessages.Add(new Error("Success", "Successfully Saved"));
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

        [HttpPut]
        [Route("edit/{id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> UpdateAsync([FromRoute] int id, [FromBody] RoleAddDTO updateDTO)
        {
            var _response = new APIResponse<RoleAddDTO>();
            try
            {
                var validationResult = _roleValidator.Validate(updateDTO);
                if (!validationResult.IsValid)
                {
                    var errors = validationResult.Errors.Select(e => new Error(e.ErrorCode, e.ErrorMessage));
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    _response.ErrorMessages = errors;
                    return BadRequest(_response);
                }

                var existingUserModel =  _userRoleService.GetRoleById(id);

                if (existingUserModel == null)
                {

                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.IsSuccess = false;
                    _errorMessages.Add(new Error("role_name", "Role Name doesn't exist."));
                    _response.ErrorMessages = _errorMessages;
                    return NotFound(_response);

                }

                existingUserModel.UpdatedDate = DateTime.Now;
                existingUserModel.UpdatedBy = updateDTO.UserName;

                _userRoleService.UpdateRole(existingUserModel);
                _response.StatusCode = HttpStatusCode.OK;
                _errorMessages.Add(new Error("role_name", "Role name updated successfully"));
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

    }
}
