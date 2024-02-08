using AutoMapper;
using BlazorCARS.HealthShield.DAL.Entity;
using BlazorCARS.HealthShield.DAL.UnitOfWork;
using BlazorCARS.HealthShield.DataObject.DTO;
using BlazorCARS.HealthShield.Utility.Extensions;
using BlazorCARS.HealthShield.Utility.RequestValidator;
using BlazorCARS.HealthShield.Utility.Response;
using BlazorCARS.HealthShield.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Microsoft.AspNetCore.Authorization;
/*
Created by JAYaseelan
*/
namespace BlazorCARS.HealthShield.WebAPI.Controllers
{
    [Route("api/recipients")]
    [ApiController]
    [Authorize]
    public class RecipientController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<RecipientController> _logger;
        private readonly APIResponse _response;
        private const string _EntityName = "Recipient";
        public RecipientController(IMapper mapper, IUnitOfWork unitOfWork, ILogger<RecipientController> logger)
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

                IEnumerable<Recipient> model = await _unitOfWork.Recipient.GetAllAsync(x => x.IsDeleted == false,
                    orderBy: o => o.OrderBy(n => n.FirstName), pageSize: pageSize, pageNumber: pageNumber);

                _response.Result = _mapper.Map<List<RecipientDTO>>(model);
                _response.StatusCode = HttpStatusCode.OK;
                _response.IsSuccess = true;
                _response.Count = await _unitOfWork.Recipient.CountAsync(x => x.IsDeleted == false);

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
        [Route("Depandant/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<APIResponse>> GetAllDependantsAsync(int id)
        {
            try
            {
                IEnumerable<Recipient> model = await _unitOfWork.Recipient.GetAllAsync(x => x.IsDeleted == false && (x.RecipientId == id || x.DependentRecipientId == id),
                    orderBy: o => o.OrderBy(n => n.FirstName), pageSize: 20, pageNumber: 1);

                _response.Result = _mapper.Map<List<RecipientDTO>>(model);
                _response.StatusCode = HttpStatusCode.OK;
                _response.IsSuccess = true;
                _response.Count = await _unitOfWork.Recipient.CountAsync(x => x.IsDeleted == false);

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
                Recipient model = await _unitOfWork.Recipient.GetByIdAsync(x => x.RecipientId == id);
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
                _response.Result = _mapper.Map<RecipientDTO>(model);
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
        public async Task<ActionResult<APIResponse>> CreateAsync([FromBody] RecipientCreateDTO requestDTO)
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

                requestDTO.CountryId = 1;
                requestDTO.StateId = 23;

                if (await CreateRequestValidator(requestDTO) == false)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    _logger.LogWarning(string.Join(", ", _response.ErrorMessages));
                    return BadRequest(_response);
                }
                //if (await _unitOfWork.Recipient.GetByIdAsync(x => x.FirstName.ToLower() == requestDTO.FirstName.ToLower()) != null)
                //{
                //    _response.StatusCode = HttpStatusCode.BadRequest;
                //    _response.IsSuccess = false;
                //    _response.ErrorMessages.Add($"{_EntityName} already exist and either Acive or Inacive.");
                //    _logger.LogWarning(string.Join(", ", _response.ErrorMessages));
                //    return BadRequest(_response);
                //}

                if (await _unitOfWork.Recipient.GetByIdAsync(x => x.AadhaarNo == requestDTO.AadhaarNo) != null)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    _response.ErrorMessages.Add("Aadhaar number already exist.");
                    _logger.LogWarning(string.Join(", ", _response.ErrorMessages));
                    return BadRequest(_response);
                }

                Recipient model = _mapper.Map<Recipient>(requestDTO);
                model.CreatedDateTime = DateTime.UtcNow;
                model.UpdatedDateTime = DateTime.UtcNow;
                model.UpdatedUser = requestDTO.CreatedUser;
                await _unitOfWork.Recipient.AddAsync(model);
                await _unitOfWork.CommitAsync();

                _response.StatusCode = HttpStatusCode.OK;
                _response.IsSuccess = true;
                _response.Result = _mapper.Map<RecipientDTO>(model);
                return CreatedAtAction("GetById", new { id = model.RecipientId }, _response);
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
        public async Task<ActionResult<APIResponse>> UpdateAsync([FromRoute] int id, [FromBody] RecipientUpdateDTO requestDTO)
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
                var existingModel = await _unitOfWork.Recipient.GetByIdAsync(j => j.RecipientId == id, tracked: false);
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
                //if (await _unitOfWork.Recipient.GetByIdAsync(x => x.RecipientId != id
                //    && x.FirstName.ToLower() == requestDTO.FirstName.ToLower(), tracked: false) != null)
                //{
                //    _response.StatusCode = HttpStatusCode.BadRequest;
                //    _response.IsSuccess = false;
                //    _response.ErrorMessages.Add($"Selected '{_EntityName}' already exist and either Acive or Inacive.");
                //    return BadRequest(_response);
                //}

                requestDTO.CountryId = 1;
                requestDTO.StateId = 23;

                var model = _mapper.Map<Recipient>(requestDTO);
                model.RecipientId = id;
                model.CreatedDateTime = existingModel.CreatedDateTime;
                model.UpdatedDateTime = DateTime.UtcNow;
                model.CreatedUser = existingModel.CreatedUser;
                _unitOfWork.Recipient.Update(model);
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
        [Route("{id:int}/{DeletedRecipient}")]
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
                result = DeletedUser.NullOrWhiteSpace("Deleted Recipient");
                if (result != string.Empty)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    _response.ErrorMessages.Add(result);
                    _logger.LogWarning(string.Join(", ", _response.ErrorMessages));
                    return BadRequest(_response);
                }
                var existingModel = await _unitOfWork.Recipient.GetByIdAsync(j => j.RecipientId == id);
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
                _unitOfWork.Recipient.Remove(existingModel);
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
        private async Task<bool> CreateRequestValidator(RecipientCreateDTO model)
        {
            string result;
            result = model.CreatedUser.NullOrWhiteSpace("Create User");
            if (result != string.Empty)
            {
                _response.ErrorMessages.Add(result);
            }
            var requestModel = _mapper.Map<RecipientRequest>(model);
            RequestValidator(requestModel);
            if (_response.ErrorMessages?.Count > 0)
            {
                return false;
            }
            return await RequestDependentValidator(requestModel);
        }

        private async Task<bool> UpdateRequestValidator(RecipientUpdateDTO model)
        {
            string result;
            result = model.UpdatedUser.NullOrWhiteSpace("Update User");
            if (result != string.Empty)
            {
                _response.ErrorMessages.Add(result);
            }
            var requestModel = _mapper.Map<RecipientRequest>(model);
            RequestValidator(requestModel);
            if (_response.ErrorMessages?.Count > 0)
            {
                return false;
            }
            return await RequestDependentValidator(requestModel);
        }

        private void RequestValidator(RecipientRequest model)
        {
            string result;
            result = model.FirstName.NullOrWhiteSpace("First Name");
            if (result != string.Empty)
            {
                _response.ErrorMessages.Add(result);
            }
            result = model.AadhaarNo.NullOrWhiteSpace("Aadhaar Number");
            if (result != string.Empty)
            {
                _response.ErrorMessages.Add(result);
            }
            result = model.PrimaryContact.NullOrWhiteSpace("Primary Contact");
            if (result != string.Empty)
            {
                _response.ErrorMessages.Add(result);
            }
            result = model.EmergencyContact.NullOrWhiteSpace("Emergency Contact");
            if (result != string.Empty)
            {
                _response.ErrorMessages.Add(result);
            }
            result = model.EmergencyContact.NullOrWhiteSpace("Relationship Type");
            if (result != string.Empty)
            {
                _response.ErrorMessages.Add(result);
            }
        }

        private async Task<bool> RequestDependentValidator(RecipientRequest model)
        {
            return true;
            //var CountryModel = await _unitOfWork.Country.GetByIdAsync(x => x.CountryId == model.CountryId);
            //if (CountryModel == null)
            //{
            //    _response.ErrorMessages.Add("Selected Country is invalid.");
            //}
            //else if (CountryModel != null && CountryModel.IsDeleted)
            //{
            //    _response.ErrorMessages.Add("Selected Country is in deactivated state.");
            //}
            //var StateModel = await _unitOfWork.State.GetByIdAsync(x => x.StateId == model.StateId);
            //if (StateModel == null)
            //{
            //    _response.ErrorMessages.Add("Selected State is invalid.");
            //}
            //else if (StateModel != null && StateModel.IsDeleted)
            //{
            //    _response.ErrorMessages.Add("Selected State is in deactivated state.");
            //}
            //return !(_response.ErrorMessages?.Count > 0);
        }
        #endregion
    }
}
