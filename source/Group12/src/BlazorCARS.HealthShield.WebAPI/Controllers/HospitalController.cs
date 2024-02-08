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
  Created by SaiSreeja Yellampalli
 */
namespace BlazorCARS.HealthShield.WebAPI.Controllers
{
    [Route("api/hospitals")]
    [ApiController]
    [Authorize]
    public class HospitalController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<HospitalController> _logger;
        private readonly APIResponse _response;
        private const string _EntityName = "Hospital";
        public HospitalController(IMapper mapper, IUnitOfWork unitOfWrok, ILogger<HospitalController> logger)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWrok;
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

                IEnumerable<Hospital> model = (IEnumerable<Hospital>)await _unitOfWork.Hospital.GetAllAsync(x => x.IsDeleted == false,
                    orderBy: o => o.OrderBy(n => n.Name), pageSize: pageSize, pageNumber: pageNumber);

                _response.Result = _mapper.Map<List<HospitalDTO>>(model);
                _response.StatusCode = HttpStatusCode.OK;
                _response.IsSuccess = true;
                _response.Count = await _unitOfWork.Hospital.CountAsync(x => x.IsDeleted == false);

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
                Hospital model = await _unitOfWork.Hospital.GetByIdAsync(x => x.HospitalId == id);
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
                _response.Result = _mapper.Map<HospitalDTO>(model);
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
        public async Task<ActionResult<APIResponse>> CreateAsync([FromBody] HospitalCreateDTO requestDTO)
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
                if ( await CreateRequestValidator(requestDTO) == false)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    _logger.LogWarning(string.Join(", ", _response.ErrorMessages));
                    return BadRequest(_response);
                }
                //if (await _unitOfWork.Hospital.GetByIdAsync(x => x.Name.ToLower() == requestDTO.Name.ToLower()) != null)
                //{
                //    _response.StatusCode = HttpStatusCode.BadRequest;
                //    _response.IsSuccess = false;
                //    _response.ErrorMessages.Add($"{_EntityName} {requestDTO.Name} already exist and either Acive or Inacive.");
                //    _logger.LogWarning(string.Join(", ", _response.ErrorMessages));
                //    return BadRequest(_response);
                //}

                Hospital model = _mapper.Map<Hospital>(requestDTO);
                model.CreatedDateTime = DateTime.UtcNow;
                model.UpdatedDateTime = DateTime.UtcNow;
                model.UpdatedUser = requestDTO.CreatedUser;
                await _unitOfWork.Hospital.AddAsync(model);
                await _unitOfWork.CommitAsync();

                _response.StatusCode = HttpStatusCode.OK;
                _response.IsSuccess = true;
                _response.Result = _mapper.Map<HospitalDTO>(model);
                return CreatedAtAction("GetById", new { id = model.HospitalId }, _response);
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
        public async Task<ActionResult<APIResponse>> UpdateAsync([FromRoute] int id, [FromBody] HospitalUpdateDTO requestDTO)
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
                var existingModel = await _unitOfWork.Hospital.GetByIdAsync(j => j.HospitalId == id, tracked: false);
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
                //if (await _unitOfWork.Hospital.GetByIdAsync(x => x.HospitalId != id
                //    && x.Name.ToLower() == requestDTO.Name.ToLower(), tracked: false) != null)
                //{
                //    _response.StatusCode = HttpStatusCode.BadRequest;
                //    _response.IsSuccess = false;
                //    _response.ErrorMessages.Add($"Selected '{_EntityName}' {requestDTO.Name} already exist and either Acive or Inacive.");
                //    return BadRequest(_response);
                //}
                var model = _mapper.Map<Hospital>(requestDTO);
                model.HospitalId = id;
                model.CreatedDateTime = existingModel.CreatedDateTime;
                model.UpdatedDateTime = DateTime.UtcNow;
                model.CreatedUser = existingModel.CreatedUser;
                model.RegistrationStatus = existingModel.RegistrationStatus;
                _unitOfWork.Hospital.Update(model);
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
        [Route("{id:int}/{DeletedHospital}")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> DeleteAync(int id, string DeletedHospital)
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
                result = DeletedHospital.NullOrWhiteSpace("Deleted Hospital");
                if (result != string.Empty)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    _response.ErrorMessages.Add(result);
                    _logger.LogWarning(string.Join(", ", _response.ErrorMessages));
                    return BadRequest(_response);
                }
                var existingModel = await _unitOfWork.Hospital.GetByIdAsync(j => j.HospitalId == id);
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
                existingModel.UpdatedUser = DeletedHospital;
                _unitOfWork.Hospital.Remove(existingModel);
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
        private async Task<bool> CreateRequestValidator(HospitalCreateDTO model)
        {
            string result;
            result = model.CreatedUser.NullOrWhiteSpace("Create User");
            if (result != string.Empty)
            {
                _response.ErrorMessages.Add(result);
            }
            var requestModel = _mapper.Map<HospitalRequest>(model);
            RequestValidator(requestModel);
            if (_response.ErrorMessages?.Count > 0)
            {
                return false;
            }
            return await RequestDependentValidator(requestModel);
        }

        private async Task<bool> UpdateRequestValidator(HospitalUpdateDTO model)
        {
            string result;
            result = model.UpdatedUser.NullOrWhiteSpace("Update User");
            if (result != string.Empty)
            {
                _response.ErrorMessages.Add(result);
            }
            var requestModel = _mapper.Map<HospitalRequest>(model);
            RequestValidator(requestModel);
            if (_response.ErrorMessages?.Count > 0)
            {
                return false;
            }
            return await RequestDependentValidator(requestModel);
        }

        private void RequestValidator(HospitalRequest model)
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
            result = model.Address1.NullOrWhiteSpace("Address1");
            if (result != string.Empty)
            {
                _response.ErrorMessages.Add(result);
            }
            result = model.LicenseNo.NullOrWhiteSpace("LicenseNo");
            if (result != string.Empty)
            {
                _response.ErrorMessages.Add(result);
            }
            //result = model.Landmark.NullOrWhiteSpace("Landmark");
            //if (result != string.Empty)
            //{
            //    _response.ErrorMessages.Add(result);
            //}
            result = model.City.NullOrWhiteSpace("City");
            if (result != string.Empty)
            {
                _response.ErrorMessages.Add(result);
            }
            result = model.PostalCode.NullOrWhiteSpace("PostalCode");
            if (result != string.Empty)
            {
                _response.ErrorMessages.Add(result);
            }
            result = model.StateId.GreaterThan(0,"StateId");
            if (result != string.Empty)
            {
                _response.ErrorMessages.Add(result);
            }
            result = model.CountryId.GreaterThan(0,"CountryId");
            if (result != string.Empty)
            {
                _response.ErrorMessages.Add(result);
            }
            result = model.PrimaryContact.NullOrWhiteSpace("PrimaryContact");
            if (result != string.Empty)
            {
                _response.ErrorMessages.Add(result);
            }
            result = model.EmergencyContact.NullOrWhiteSpace("EmergencyContact");
            if (result != string.Empty)
            {
                _response.ErrorMessages.Add(result);
            }
            result = model.EmailId.EmailAddress("EmailId");
            if (result != string.Empty)
            {
                _response.ErrorMessages.Add(result);
            }
        }
        private async Task<bool> RequestDependentValidator(HospitalRequest model)
        {
            var HospitalModel = await _unitOfWork.Country.GetByIdAsync(x => x.CountryId == model.CountryId);
            if (HospitalModel == null)
            {
                _response.ErrorMessages.Add("Selected Country is invalid.");
            }
            else if (HospitalModel != null && HospitalModel.IsDeleted)
            {
                _response.ErrorMessages.Add("Selected Country is in deactivated state.");
            }
            var StateModel = await _unitOfWork.State.GetByIdAsync(x => x.StateId == model.StateId);
            if (StateModel == null)
            {
                _response.ErrorMessages.Add("Selected State is invalid.");
            }
            else if (StateModel != null && StateModel.IsDeleted)
            {
                _response.ErrorMessages.Add("Selected State is in deactivated state.");
            }
            return !(_response.ErrorMessages?.Count > 0);
        }
        #endregion
    }
}
