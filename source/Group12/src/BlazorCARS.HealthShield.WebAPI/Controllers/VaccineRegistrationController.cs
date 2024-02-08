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
using BlazorCARS.HealthShield.DAL.Appointment;
using BlazorCARS.HealthShield.DataObject.DTO.Appointment;
using Microsoft.AspNetCore.Authorization;

namespace BlazorCARS.HealthShield.WebAPI.Controllers
{
    [Route("api/vaccineregistrations")]
    [ApiController]
    [Authorize]

    public class VaccineRegistrationController: ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<VaccineRegistrationController> _logger;
        private readonly APIResponse _response;
        private const string _EntityName = "Vaccine Registration";


        #region Read data
        public VaccineRegistrationController(IMapper mapper, IUnitOfWork unitOfWork, ILogger<VaccineRegistrationController> logger)
        {
            this._mapper = mapper;
            this._unitOfWork = unitOfWork;
            this._logger = logger;
            this._response = new APIResponse();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<APIResponse>> GetAllSync(int pageSize, int pageNumber)
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

                IEnumerable<VaccineRegistration> model = await _unitOfWork.VaccineRegistration.GetAllAsync(x => x.IsDeleted == false,
                    orderBy: o => o.OrderBy(n => n.VaccineRegistrationId), pageSize: pageSize, pageNumber: pageNumber);

                _response.Result = _mapper.Map<List<VaccineRegistrationDTO>>(model);
                _response.StatusCode = HttpStatusCode.OK;
                _response.IsSuccess = true;
                _response.Count = await _unitOfWork.VaccineRegistration.CountAsync(x => x.IsDeleted == false);

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
                VaccineRegistration model = await _unitOfWork.VaccineRegistration.GetByIdAsync(x => x.VaccineRegistrationId == id);
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
                _response.Result = _mapper.Map<VaccineRegistrationDTO>(model);
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

        [HttpGet]
        [Route("active/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<APIResponse>> GetActiveappoinmentAsync(int id)
        {
            try
            {
                IEnumerable<ActiveAppointment> model = await _unitOfWork.VaccineRegistration.GetActiveappoinmentAsync(id);

                _response.Result = _mapper.Map<List<ActiveAppointmentDTO>>(model);
                _response.StatusCode = HttpStatusCode.OK;
                _response.IsSuccess = true;
                _response.Count = 0;

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
        [Route("hospitalactiveappointment/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<APIResponse>> GetActiveappoinmentByHospitalAsync(int id)
        {
            try
            {
                IEnumerable<ActiveAppointment> model = await _unitOfWork.VaccineRegistration.GetActiveappoinmentByHospitalAsync(id);

                _response.Result = _mapper.Map<List<ActiveAppointmentDTO>>(model);
                _response.StatusCode = HttpStatusCode.OK;
                _response.IsSuccess = true;
                _response.Count = 0;

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
        public async Task<ActionResult<APIResponse>> CreateAsync([FromBody] VaccineRegistrationCreateDTO requestDTO)
        {
            try
            {
                if (requestDTO == null)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    _response.ErrorMessages.Add($"{_EntityName} request object must not be empty.");
                    _logger.LogWarning(string.Join(", ", this._response.ErrorMessages));
                    return BadRequest(this._response);
                }
                if (await CreateRequestValidator(requestDTO) == false)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    _logger.LogWarning(string.Join(", ", _response.ErrorMessages));
                    return BadRequest(_response);
                }
                if (await _unitOfWork.VaccineRegistration.GetByIdAsync(x => x.RecipientId == requestDTO.RecipientId
                && x.Dose.ToLower() == requestDTO.Dose.ToLower()) != null)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    _response.ErrorMessages.Add($"{_EntityName} already exist and either Acive or Inacive.");
                    _logger.LogWarning(string.Join(", ", _response.ErrorMessages));
                    return BadRequest(_response);
                }
                //int count = await _unitOfWork.VaccineRegistration.MaxAsync();
                VaccineRegistration model = this._mapper.Map<VaccineRegistration>(requestDTO);
                //model.VaccineRegistrationId = count + 1;
                model.CreatedDateTime = DateTime.UtcNow;
                model.UpdatedDateTime = DateTime.UtcNow;
                model.UpdatedUser = requestDTO.CreatedUser;
                await _unitOfWork.VaccineRegistration.AddAsync(model);
                await _unitOfWork.CommitAsync();

                _response.StatusCode = HttpStatusCode.OK;
                _response.IsSuccess = true;
                _response.Result = _mapper.Map<VaccineRegistrationDTO>(model);
                return CreatedAtAction("GetById", new { id = model.VaccineRegistrationId }, _response);
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
        public async Task<ActionResult<APIResponse>> UpdateAsync([FromRoute] int id, [FromBody] VaccineRegistrationUpdateDTO requestDTO)
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

                var existingModel = await _unitOfWork.VaccineRegistration.GetByIdAsync(j => j.VaccineRegistrationId == id, tracked: false);
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
                if (await _unitOfWork.VaccineRegistration.GetByIdAsync(x => x.VaccineRegistrationId != id, tracked: false) != null)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    _response.ErrorMessages.Add($"Selected '{_EntityName}' already exist and either Acive or Inacive.");
                    return BadRequest(_response);
                }
                var model = _mapper.Map<VaccineRegistration>(requestDTO);
                model.VaccineRegistrationId = id;
                model.CreatedDateTime = existingModel.CreatedDateTime;
                model.UpdatedDateTime = DateTime.UtcNow;
                model.CreatedUser = existingModel.CreatedUser;
                _unitOfWork.VaccineRegistration.Update(model);
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
        [Route("{id:int}")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> DeleteAync(int id)
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

                var existingModel = await _unitOfWork.VaccineRegistration.GetByIdAsync(j => j.VaccineRegistrationId == id);
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
                existingModel.UpdatedUser = "current user>"; //DeletedUser; TODO
                _unitOfWork.VaccineRegistration.Remove(existingModel);
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private async Task<bool> CreateRequestValidator(VaccineRegistrationCreateDTO model)
        {
            string result;
            result = model.CreatedUser.NullOrWhiteSpace("Create User");
            if (result != string.Empty)
            {
                _response.ErrorMessages.Add(result);
            }
            var requestModel = _mapper.Map<VaccineRegistrationRequest>(model);
            RequestValidator(requestModel);
            if (_response.ErrorMessages?.Count > 0)
            {
                return false;
            }
            return await RequestDependentValidator(requestModel);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private async Task<bool> UpdateRequestValidator(VaccineRegistrationUpdateDTO model)
        {
            string result;
            result = model.UpdatedUser.NullOrWhiteSpace("Update User");
            if (result != string.Empty)
            {
                _response.ErrorMessages.Add(result);
            }
            var requestModel = _mapper.Map<VaccineRegistrationRequest>(model);
            RequestValidator(requestModel);
            if (_response.ErrorMessages?.Count > 0)
            {
                return false;
            }
            return await RequestDependentValidator(requestModel);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        private void RequestValidator(VaccineRegistrationRequest model)
        {
            string result;
            result = model.RecipientId.GreaterThan(0, "Recipient");
            if (result != string.Empty)
            {
                _response.ErrorMessages.Add(result);
            }
            result = model.VaccineScheduleId.GreaterThan(0, "Vaccine Schedule");
            if (result != string.Empty)
            {
                _response.ErrorMessages.Add(result);
            }
            result = model.TimeSlot.NullOrWhiteSpace("Time Slot");
            if (result != string.Empty)
            {
                _response.ErrorMessages.Add(result);
            }
            result = model.Dose.NullOrWhiteSpace("Dose");
            if (result != string.Empty)
            {
                _response.ErrorMessages.Add(result);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private async Task<bool> RequestDependentValidator(VaccineRegistrationRequest model)
        {
            var RecipientModel = await _unitOfWork.Recipient.GetByIdAsync(x => x.RecipientId == model.RecipientId);
            if (RecipientModel == null)
            {
                _response.ErrorMessages.Add("Selected Recipient is invalid.");
            }
            else if (RecipientModel != null && RecipientModel.IsDeleted)
            {
                _response.ErrorMessages.Add("Selected Recipient is in deactivated state.");
            }
            var VaccineScheduleModel = await _unitOfWork.VaccineSchedule.GetByIdAsync(x => x.VaccineScheduleId == model.VaccineScheduleId);
            if (VaccineScheduleModel == null)
            {
                _response.ErrorMessages.Add("Selected Vaccine Schedule is invalid.");
            }
            else if (VaccineScheduleModel != null && VaccineScheduleModel.IsDeleted)
            {
                _response.ErrorMessages.Add("Selected Vaccine Schedule is in deactivated state.");
            }
            return !(_response.ErrorMessages?.Count > 0);
        }
        #endregion
    }
}
