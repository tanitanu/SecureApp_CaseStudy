using AutoMapper;
using BlazorCARS.HealthShield.DAL.UnitOfWork;
using BlazorCARS.HealthShield.DataObject.DTO;
using BlazorCARS.HealthShield.Utility.RequestValidator;
using BlazorCARS.HealthShield.Utility.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using BlazorCARS.HealthShield.Utility.Extensions;
using BlazorCARS.HealthShield.Utility;
using BlazorCARS.HealthShield.DAL.Entity;
using Microsoft.AspNetCore.Authorization;

/*
  Created by JAYaseelan
 */
namespace BlazorCARS.HealthShield.WebAPI.Controllers
{
    [Route("api/userroles")]
    [ApiController]
    [Authorize]
    public class UserRoleController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<UserRoleController> _logger;
        private readonly APIResponse _response;
        private const string _EntityName = "User Role";
        public UserRoleController(IMapper mapper, IUnitOfWork unitOfWork, ILogger<UserRoleController> logger)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _logger = logger;
            _response = new APIResponse();
        }


        #region Ready data
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<APIResponse>> GetAllAsync(int pageSize, int pageNumber)
        {
            try
            {
                if (CommonFunctions.ValidatePaginationParameters(_response, pageSize, pageNumber) == false)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    _logger.LogWarning(string.Join(", ", _response.ErrorMessages));
                    return BadRequest(_response);
                }

                IEnumerable<UserRole> model = await _unitOfWork.UserRole.GetAllAsync(x => x.IsDeleted == false,
                    orderBy: o => o.OrderBy(n => n.Name), pageSize: pageSize, pageNumber: pageNumber);

                _response.Result = _mapper.Map<List<UserRoleDTO>>(model);
                _response.StatusCode = HttpStatusCode.OK;
                _response.IsSuccess = true;
                _response.Count = await _unitOfWork.UserRole.CountAsync(x => x.IsDeleted == false);

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

        [HttpGet]
        [Route("{id:int}")]
        [ActionName("GetById")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> GetByIdAsync(int id)
        {
            try
            {
                string result = id.GreaterThan(0, $"{_EntityName}");
                if (result != string.Empty)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    _response.ErrorMessages.Add(result);
                    _logger.LogWarning(string.Join(", ", _response.ErrorMessages));
                    return BadRequest(_response);
                }
                UserRole model = await _unitOfWork.UserRole.GetByIdAsync(x => x.UserRoleId == id);
                if (model == null)
                {
                    _response.StatusCode = HttpStatusCode.NoContent;
                    _response.IsSuccess = false;
                    _response.ErrorMessages.Add($"Selected '{_EntityName}' doesn't exist.");
                    _logger.LogWarning(string.Join(", ", _response.ErrorMessages));
                    return NotFound(_response);
                }
                else if (model != null && model.IsDeleted)
                {
                    _response.StatusCode = HttpStatusCode.NoContent;
                    _response.IsSuccess = false;
                    _response.ErrorMessages.Add($"Selected '{_EntityName}' is in deactivated state.");
                    _logger.LogWarning(string.Join(", ", _response.ErrorMessages));
                    return NotFound(_response);
                }
                _response.Result = _mapper.Map<UserRoleDTO>(model);
                _response.StatusCode = HttpStatusCode.OK;
                _response.IsSuccess = true;
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
        #endregion

        #region Create/Update/Delete operations
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> CreateAsync([FromBody] UserRoleCreateDTO requestDTO)
        {
            try
            {
                if (requestDTO == null)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    _response.ErrorMessages.Add($"{_EntityName} request object must not be empty.");
                    _logger.LogWarning(string.Join(", ", _response.ErrorMessages));
                    return BadRequest(_response);
                }
                if (CreateRequestValidator(requestDTO) == false)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    _logger.LogWarning(string.Join(", ", _response.ErrorMessages));
                    return BadRequest(_response);
                }
                if (await _unitOfWork.UserRole.GetByIdAsync(x => x.Name.ToLower() == requestDTO.Name.ToLower()) != null)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    _response.ErrorMessages.Add($"{_EntityName} {requestDTO.Name} already exist and either Acive or Inacive.");
                    _logger.LogWarning(string.Join(", ", _response.ErrorMessages));
                    return BadRequest(_response);
                }

                UserRole model = _mapper.Map<UserRole>(requestDTO);
                model.CreatedDateTime = DateTime.UtcNow;
                model.UpdatedDateTime = DateTime.UtcNow;
                model.UpdatedUser = requestDTO.CreatedUser;
                await _unitOfWork.UserRole.AddAsync(model);
                await _unitOfWork.CommitAsync();

                _response.StatusCode = HttpStatusCode.OK;
                _response.IsSuccess = true;
                _response.Result = _mapper.Map<UserRoleDTO>(model);
                return CreatedAtAction("GetById", new { id = model.UserRoleId }, _response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }
            return _response;
        }

        [HttpPut]
        [Route("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> UpdateAsync([FromRoute] int id, [FromBody] UserRoleUpdateDTO requestDTO)
        {
            try
            {
                string result = id.GreaterThan(0, $"{_EntityName}");
                if (result != string.Empty)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    _response.ErrorMessages.Add(result);
                    _logger.LogWarning(string.Join(", ", _response.ErrorMessages));
                    return BadRequest(_response);
                }
                if (UpdateRequestValidator(requestDTO) == false)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    _logger.LogWarning(string.Join(", ", _response.ErrorMessages));
                    return BadRequest(_response);
                }
                var existingModel = await _unitOfWork.UserRole.GetByIdAsync(j => j.UserRoleId == id, tracked: false);
                if (existingModel == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.IsSuccess = false;
                    _response.ErrorMessages.Add($"Selected '{_EntityName}' doesn't exist.");
                    _logger.LogWarning(string.Join(", ", _response.ErrorMessages));
                    return NotFound(_response);
                }
                else if (existingModel != null && existingModel.IsDeleted)
                {
                    _response.StatusCode = HttpStatusCode.NoContent;
                    _response.IsSuccess = false;
                    _response.ErrorMessages.Add($"Selected '{_EntityName}' is in deactivated state.");
                    _logger.LogWarning(string.Join(", ", _response.ErrorMessages));
                    return NotFound(_response);
                }
                if (await _unitOfWork.UserRole.GetByIdAsync(x => x.UserRoleId != id
                    && x.Name.ToLower() == requestDTO.Name.ToLower(), tracked: false) != null)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    _response.ErrorMessages.Add($"Selected '{_EntityName}' {requestDTO.Name} already exist and either Acive or Inacive.");
                    return BadRequest(_response);
                }
                var model = _mapper.Map<UserRole>(requestDTO);
                model.UserRoleId = id;
                model.CreatedDateTime = existingModel.CreatedDateTime;
                model.UpdatedDateTime = DateTime.UtcNow;
                model.CreatedUser = existingModel.CreatedUser;
                _unitOfWork.UserRole.Update(model);
                await _unitOfWork.CommitAsync();

                _response.StatusCode = HttpStatusCode.OK;
                _response.IsSuccess = true;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }
            return _response;
        }
        [HttpDelete]
        [Route("{id:int}/{DeletedUser}")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> DeleteAync(int id, string DeletedUser)
        {
            try
            {
                string result = id.GreaterThan(0, $"{_EntityName}");
                if (result != string.Empty)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    _response.ErrorMessages.Add(result);
                    _logger.LogWarning(string.Join(", ", _response.ErrorMessages));
                    return BadRequest(_response);
                }
                result = DeletedUser.NullOrWhiteSpace("Deleted User");
                if (result != string.Empty)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    _response.ErrorMessages.Add(result);
                    _logger.LogWarning(string.Join(", ", _response.ErrorMessages));
                    return BadRequest(_response);
                }
                var existingModel = await _unitOfWork.UserRole.GetByIdAsync(j => j.UserRoleId == id);
                if (existingModel == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.IsSuccess = false;
                    _response.ErrorMessages.Add($"Selected '{_EntityName}' doesn't exist.");
                    _logger.LogWarning(string.Join(", ", _response.ErrorMessages));
                    return NotFound(_response);
                }
                else if (existingModel != null && existingModel.IsDeleted)
                {
                    _response.StatusCode = HttpStatusCode.NoContent;
                    _response.IsSuccess = false;
                    _response.ErrorMessages.Add($"Selected '{_EntityName}' is already in deactivated state.");
                    _logger.LogWarning(string.Join(", ", _response.ErrorMessages));
                    return NotFound(_response);
                }
                existingModel.IsDeleted = true;
                existingModel.UpdatedDateTime = DateTime.UtcNow;
                existingModel.UpdatedUser = DeletedUser;
                _unitOfWork.UserRole.Remove(existingModel);
                await _unitOfWork.CommitAsync();
                _response.StatusCode = HttpStatusCode.OK;
                _response.IsSuccess = true;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }
            return _response;
        }
        #endregion

        #region All Private methods go here
        private bool CreateRequestValidator(UserRoleCreateDTO model)
        {
            string result;
            result = model.CreatedUser.NullOrWhiteSpace("Create User");
            if (result != string.Empty)
            {
                _response.ErrorMessages.Add(result);
            }
            var requestModel = _mapper.Map<UserRoleRequest>(model);
            RequestValidator(requestModel);
            if (_response.ErrorMessages?.Count > 0)
            {
                return false;
            }
            return true;
        }

        private bool UpdateRequestValidator(UserRoleUpdateDTO model)
        {
            string result;
            result = model.UpdatedUser.NullOrWhiteSpace("Update User");
            if (result != string.Empty)
            {
                _response.ErrorMessages.Add(result);
            }
            var requestModel = _mapper.Map<UserRoleRequest>(model);
            RequestValidator(requestModel);
            if (_response.ErrorMessages?.Count > 0)
            {
                return false;
            }
            return true;
        }

        private void RequestValidator(UserRoleRequest model)
        {
            string result;
            result = model.Name.NullOrWhiteSpace("Name");
            if (result != string.Empty)
            {
                _response.ErrorMessages.Add(result);
            }
            result = model.Name.MaximumLength(50, "Name");
            if (result != string.Empty)
            {
                _response.ErrorMessages.Add(result);
            }
        }
        #endregion
    }
}
