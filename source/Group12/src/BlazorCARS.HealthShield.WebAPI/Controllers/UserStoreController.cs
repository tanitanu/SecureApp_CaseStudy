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
    [Route("api/userstories")]
    [ApiController]
    [Authorize]
    public class UserStoreController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<UserStoreController> _logger;
        private readonly APIResponse _response;
        private const string _EntityName = "User Store";
        public UserStoreController(IMapper mapper, IUnitOfWork unitOfWork, ILogger<UserStoreController> logger)
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

                IEnumerable<UserStore> model = await _unitOfWork.UserStore.GetAllAsync(x => x.IsDeleted == false,
                    orderBy: o => o.OrderBy(n => n.UserName), pageSize: pageSize, pageNumber: pageNumber);

                _response.Result = _mapper.Map<List<UserStoreDTO>>(model);
                _response.StatusCode = HttpStatusCode.OK;
                _response.IsSuccess = true;
                _response.Count = await _unitOfWork.UserStore.CountAsync(x => x.IsDeleted == false);

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
                UserStore model = await _unitOfWork.UserStore.GetByIdAsync(x => x.UserStoreId == id);
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
                _response.Result = _mapper.Map<UserStoreDTO>(model);
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
        public async Task<ActionResult<APIResponse>> CreateAsync([FromBody] UserStoreCreateDTO requestDTO)
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
                if (await CreateRequestValidator(requestDTO) == false)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    _logger.LogWarning(string.Join(", ", _response.ErrorMessages));
                    return BadRequest(_response);
                }
                var dupcheck = await _unitOfWork.UserStore.GetByIdAsync(x => x.UserName.ToLower() == requestDTO.UserName.ToLower());
                if (dupcheck != null && dupcheck.IsDeleted == false)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    _response.ErrorMessages.Add($"{_EntityName} {requestDTO.UserName} already exist.");
                    _logger.LogWarning(string.Join(", ", _response.ErrorMessages));
                    return BadRequest(_response);
                }
                else if (dupcheck != null && dupcheck.IsDeleted == true)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    _response.ErrorMessages.Add($"{requestDTO.UserName} already exist but it is in deactivated state. Please contact Admin.");
                    _logger.LogWarning(string.Join(", ", _response.ErrorMessages));
                    return BadRequest(_response);
                }
                //Encrypt the password
                string encryptedPassword = CommonFunctions.EncryptPassword(requestDTO.Password);
                if (encryptedPassword == string.Empty)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    _response.ErrorMessages.Add($"Password must not be empty.");
                    _logger.LogWarning(string.Join(", ", _response.ErrorMessages));
                    return BadRequest(_response);
                }
                UserStore model = _mapper.Map<UserStore>(requestDTO);
                model.CreatedDateTime = DateTime.UtcNow;
                model.UpdatedDateTime = DateTime.UtcNow;
                model.UpdatedUser = requestDTO.CreatedUser;
                model.Password = encryptedPassword;
                await _unitOfWork.UserStore.AddAsync(model);
                await _unitOfWork.CommitAsync();

                _response.StatusCode = HttpStatusCode.OK;
                _response.IsSuccess = true;
                _response.Result = _mapper.Map<UserStoreDTO>(model);
                return CreatedAtAction("GetById", new { id = model.UserStoreId }, _response);
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
        public async Task<ActionResult<APIResponse>> UpdateAsync([FromRoute] int id, [FromBody] UserStoreUpdateDTO requestDTO)
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
                if (await UpdateRequestValidator(requestDTO) == false)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    _logger.LogWarning(string.Join(", ", _response.ErrorMessages));
                    return BadRequest(_response);
                }
                var existingModel = await _unitOfWork.UserStore.GetByIdAsync(j => j.UserStoreId == id, tracked: false);
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
                if (await _unitOfWork.UserStore.GetByIdAsync(x => x.UserStoreId != id
                    && x.UserName.ToLower() == requestDTO.UserName.ToLower(), tracked: false) != null)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    _response.ErrorMessages.Add($"Selected '{_EntityName}' {requestDTO.UserName} already exist and either Acive or Inacive.");
                    return BadRequest(_response);
                }
                var model = _mapper.Map<UserStore>(requestDTO);
                model.UserStoreId = id;
                model.CreatedDateTime = existingModel.CreatedDateTime;
                model.UpdatedDateTime = DateTime.UtcNow;
                model.CreatedUser = existingModel.CreatedUser;
                _unitOfWork.UserStore.Update(model);
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
                var existingModel = await _unitOfWork.UserStore.GetByIdAsync(j => j.UserStoreId == id);
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
                _unitOfWork.UserStore.Remove(existingModel);
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
        private async Task<bool> CreateRequestValidator(UserStoreCreateDTO model)
        {
            string result;
            result = model.CreatedUser.NullOrWhiteSpace("Create User");
            if (result != string.Empty)
            {
                _response.ErrorMessages.Add(result);
            }
            var requestModel = _mapper.Map<UserStoreRequest>(model);
            RequestValidator(requestModel);
            if (_response.ErrorMessages?.Count > 0)
            {
                return false;
            }
            return await RequestDependentValidator(requestModel);
        }

        private async Task<bool> UpdateRequestValidator(UserStoreUpdateDTO model)
        {
            string result;
            result = model.UpdatedUser.NullOrWhiteSpace("Update User");
            if (result != string.Empty)
            {
                _response.ErrorMessages.Add(result);
            }
            var requestModel = _mapper.Map<UserStoreRequest>(model);
            RequestValidator(requestModel);
            if (_response.ErrorMessages?.Count > 0)
            {
                return false;
            }
            return await RequestDependentValidator(requestModel);
        }

        private void RequestValidator(UserStoreRequest model)
        {
            string result;
            result = model.UserName.NullOrWhiteSpace("User Name");
            if (result != string.Empty)
            {
                _response.ErrorMessages.Add(result);
            }
            result = model.UserName.MaximumLength(50, "User Name");
            if (result != string.Empty)
            {
                _response.ErrorMessages.Add(result);
            }
            result = model.Password.NullOrWhiteSpace("password");
            if (result != string.Empty)
            {
                _response.ErrorMessages.Add(result);
            }
            result = model.UserRoleId.GreaterThan(0, "User Role");
            if (result != string.Empty)
            {
                _response.ErrorMessages.Add(result);
            }
            result = model.DiscriminationId.GreaterThan(0, "Discrimination");
            if (result != string.Empty)
            {
                _response.ErrorMessages.Add(result);
            }
        }

        private async Task<bool> RequestDependentValidator(UserStoreRequest model)
        {
            var RoleModel = await _unitOfWork.UserRole.GetByIdAsync(x => x.UserRoleId == model.UserRoleId);
            if (RoleModel == null)
            {
                _response.ErrorMessages.Add($"Selected {_EntityName} is invalid.");
            }
            else if (RoleModel != null && RoleModel.IsDeleted)
            {
                _response.ErrorMessages.Add($"Selected {_EntityName} is in deactivated state.");
            }
            return !(_response.ErrorMessages?.Count > 0);
        }
        #endregion

        #region Change/Reset Password
        [HttpPut]
        [Route("changepassword")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> ChangePasswordAsync(ChangePasswordDTO changePasswordDTO)
        {
            try
            {
                if (changePasswordDTO == null)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    _response.ErrorMessages.Add($"User request object must not be empty.");
                    _logger.LogWarning(string.Join(", ", _response.ErrorMessages));
                    return BadRequest(_response);
                }
                string result = changePasswordDTO.UserName.NullOrWhiteSpace("User Name");
                if (result != string.Empty)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    _response.ErrorMessages.Add(result);
                    _logger.LogWarning(string.Join(", ", _response.ErrorMessages));
                    return BadRequest(_response);
                }
                result = changePasswordDTO.CurrentPassword.NullOrWhiteSpace("Current Password");
                if (result != string.Empty)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    _response.ErrorMessages.Add(result);
                    _logger.LogWarning(string.Join(", ", _response.ErrorMessages));
                    return BadRequest(_response);
                }
                result = changePasswordDTO.CurrentPassword.NullOrWhiteSpace("New Password");
                if (result != string.Empty)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    _response.ErrorMessages.Add(result);
                    _logger.LogWarning(string.Join(", ", _response.ErrorMessages));
                    return BadRequest(_response);
                }
                var userModel = await _unitOfWork.UserStore.GetByUserNameAsync(changePasswordDTO.UserName);
                if (userModel == null)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    _response.ErrorMessages.Add("User doesn't Exist.");
                    _logger.LogWarning(string.Join(", ", _response.ErrorMessages));
                    return BadRequest(_response);
                }
                else if (userModel != null && userModel.IsDeleted)
                {
                    _response.StatusCode = HttpStatusCode.NoContent;
                    _response.IsSuccess = false;
                    _response.ErrorMessages.Add($"User is in deactivated state.");
                    _logger.LogWarning(string.Join(", ", _response.ErrorMessages));
                    return NotFound(_response);
                }
                //Encrypt the current password
                string encryptedCurrentPassword = CommonFunctions.EncryptPassword(changePasswordDTO.CurrentPassword);
                if (encryptedCurrentPassword == string.Empty)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    _response.ErrorMessages.Add($"Password must not be empty.");
                    _logger.LogWarning(string.Join(", ", _response.ErrorMessages));
                    return BadRequest(_response);
                }
                if (encryptedCurrentPassword != userModel.Password)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    _response.ErrorMessages.Add($"Current Password is incorrect.");
                    _logger.LogWarning(string.Join(", ", _response.ErrorMessages));
                    return BadRequest(_response);
                }
                //Encrypt the new password
                string encryptedPassword = CommonFunctions.EncryptPassword(changePasswordDTO.NewPassword);
                if (encryptedPassword == string.Empty)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    _response.ErrorMessages.Add($"Password must not be empty.");
                    _logger.LogWarning(string.Join(", ", _response.ErrorMessages));
                    return BadRequest(_response);
                }
                var existingModel = await _unitOfWork.UserStore.GetByIdAsync(j => j.UserStoreId == userModel.UserStoreId);
                existingModel.UpdatedDateTime = DateTime.UtcNow;
                existingModel.UpdatedUser = changePasswordDTO.UserName;
                existingModel.Password = encryptedPassword;
                _unitOfWork.UserStore.Update(existingModel);
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
                _response.ErrorMessages.Add(ex.ToString());
            }
            return _response;
        }

        [HttpPut]
        [Route("resetpassword")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> ResetPasswordAsync(ResetPasswordDTO resetPasswordDTO)
        {
            try
            {
                if (resetPasswordDTO == null)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    _response.ErrorMessages.Add($"User request object must not be empty.");
                    _logger.LogWarning(string.Join(", ", _response.ErrorMessages));
                    return BadRequest(_response);
                }
                string result = resetPasswordDTO.UserName.NullOrWhiteSpace("User Name");
                if (result != string.Empty)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    _response.ErrorMessages.Add(result);
                    _logger.LogWarning(string.Join(", ", _response.ErrorMessages));
                    return BadRequest(_response);
                }
                result = resetPasswordDTO.NewPassword.NullOrWhiteSpace("Current Password");
                if (result != string.Empty)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    _response.ErrorMessages.Add(result);
                    _logger.LogWarning(string.Join(", ", _response.ErrorMessages));
                    return BadRequest(_response);
                }
                result = resetPasswordDTO.UserRoleId.GreaterThan(0, "Role Id");
                if (result != string.Empty)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    _response.ErrorMessages.Add(result);
                    _logger.LogWarning(string.Join(", ", _response.ErrorMessages));
                    return BadRequest(_response);
                }
                if (resetPasswordDTO.UserRoleId != (int)DataObject.Enums.UserRole.SAdmin)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    _response.ErrorMessages.Add("You don't have privilliage to reset password. Please contact Administrator");
                    _logger.LogWarning(string.Join(", ", _response.ErrorMessages));
                    return BadRequest(_response);
                }
                var userModel = await _unitOfWork.UserStore.GetByUserNameAsync(resetPasswordDTO.UserName);
                if (userModel == null)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    _response.ErrorMessages.Add("User doesn't Exist.");
                    _logger.LogWarning(string.Join(", ", _response.ErrorMessages));
                    return BadRequest(_response);
                }
                else if (userModel != null && userModel.IsDeleted)
                {
                    _response.StatusCode = HttpStatusCode.NoContent;
                    _response.IsSuccess = false;
                    _response.ErrorMessages.Add($"User is in deactivated state.");
                    _logger.LogWarning(string.Join(", ", _response.ErrorMessages));
                    return NotFound(_response);
                }
                //Encrypt the new password
                string encryptedPassword = CommonFunctions.EncryptPassword(resetPasswordDTO.NewPassword);
                if (encryptedPassword == string.Empty)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    _response.ErrorMessages.Add($"Password must not be empty.");
                    _logger.LogWarning(string.Join(", ", _response.ErrorMessages));
                    return BadRequest(_response);
                }
                var existingModel = await _unitOfWork.UserStore.GetByIdAsync(j => j.UserStoreId == userModel.UserStoreId);
                existingModel.UpdatedDateTime = DateTime.UtcNow;
                existingModel.UpdatedUser = resetPasswordDTO.ChangedBy;
                existingModel.Password = encryptedPassword;
                _unitOfWork.UserStore.Update(existingModel);
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
                _response.ErrorMessages.Add(ex.ToString());
            }
            return _response;
        }
        #endregion
    }
}
