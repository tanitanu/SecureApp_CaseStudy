using AutoMapper;
using BlazorCARS.HealthShield.DAL.Entity;
using BlazorCARS.HealthShield.DAL.UnitOfWork;
using BlazorCARS.HealthShield.DataObject.DTO;
using BlazorCARS.HealthShield.Utility;
using BlazorCARS.HealthShield.Utility.Extensions;
using BlazorCARS.HealthShield.Utility.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BlazorCARS.HealthShield.WebAPI.Controllers
{
    [Route("api/vaccineschedules")]
    [ApiController]
    [Authorize]
    public class VaccineScheduleController: ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<VaccineScheduleController> _logger;
        private readonly APIResponse _response;
        private const string _EntityName = "Vaccine Schedule";

        #region Read data
        public VaccineScheduleController(IMapper mapper, IUnitOfWork unitOfWork, ILogger<VaccineScheduleController> logger)
        {
            this._mapper = mapper;
            this._unitOfWork = unitOfWork;
            this._logger = logger;
            this._response = new APIResponse();
        }

        [HttpGet]
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

                IEnumerable<VaccineSchedule> model = await _unitOfWork.VaccineSchedule.GetAllAsync(x => x.IsDeleted == false,
                    orderBy: o => o.OrderBy(n => n.ScheduleDate), pageSize: pageSize, pageNumber: pageNumber);

                _response.Result = _mapper.Map<List<VaccineScheduleDTO>>(model);
                _response.StatusCode = HttpStatusCode.OK;
                _response.IsSuccess = true;
                _response.Count = await _unitOfWork.VaccineSchedule.CountAsync(x => x.IsDeleted == false);

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
                VaccineSchedule model = await _unitOfWork.VaccineSchedule.GetByIdAsync(x => x.VaccineScheduleId == id);
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
                _response.Result = _mapper.Map<VaccineScheduleDTO>(model);
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
        public async Task<ActionResult<APIResponse>> CreateAsync([FromBody] VaccineScheduleCreateDTO requestDTO)
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
                //if (await CreateRequestValidator(requestDTO) == false)
                //{
                //    _response.StatusCode = HttpStatusCode.BadRequest;
                //    _response.IsSuccess = false;
                //    _logger.LogWarning(string.Join(", ", _response.ErrorMessages));
                //    return BadRequest(_response);
                //}
                //if (await _unitOfWork.VaccineSchedule.GetByIdAsync(x => x.FirstName.ToLower() == requestDTO.FirstName.ToLower()) != null)
                //{
                //    _response.StatusCode = HttpStatusCode.BadRequest;
                //    _response.IsSuccess = false;
                //    _response.ErrorMessages.Add($"{_EntityName} already exist and either Acive or Inacive.");
                //    _logger.LogWarning(string.Join(", ", _response.ErrorMessages));
                //    return BadRequest(_response);
                //}

                VaccineSchedule model = this._mapper.Map<VaccineSchedule>(requestDTO);
                model.CreatedDateTime = DateTime.UtcNow;
                model.UpdatedDateTime = DateTime.UtcNow;
                model.UpdatedUser = requestDTO.CreatedUser;
                await _unitOfWork.VaccineSchedule.AddAsync(model);
                await _unitOfWork.CommitAsync();

                _response.StatusCode = HttpStatusCode.OK;
                _response.IsSuccess = true;
                _response.Result = _mapper.Map<VaccineScheduleDTO>(model);
                return CreatedAtAction("GetById", new { id = model.VaccineScheduleId }, _response);
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
        public async Task<ActionResult<APIResponse>> UpdateAsync([FromRoute] int id, [FromBody] VaccineScheduleUpdateDTO requestDTO)
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

                //if (await UpdateRequestValidator(requestDTO) == false)
                //{
                //    _response.StatusCode = HttpStatusCode.BadRequest;
                //    _response.IsSuccess = false;
                //    _logger.LogWarning(string.Join(", ", _response.ErrorMessages));
                //    return BadRequest(_response);
                //}

                var existingModel = await _unitOfWork.VaccineSchedule.GetByIdAsync(j => j.VaccineScheduleId == id, tracked: false);
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
                if (await _unitOfWork.VaccineSchedule.GetByIdAsync(x => x.VaccineScheduleId != id, tracked: false) != null)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    _response.ErrorMessages.Add($"Selected '{_EntityName}' already exist and either Acive or Inacive.");
                    return BadRequest(_response);
                }
                var model = _mapper.Map<VaccineSchedule>(requestDTO);
                model.VaccineScheduleId = id;
                model.CreatedDateTime = existingModel.CreatedDateTime;
                model.UpdatedDateTime = DateTime.UtcNow;
                model.CreatedUser = existingModel.CreatedUser;
                _unitOfWork.VaccineSchedule.Update(model);
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

                var existingModel = await _unitOfWork.VaccineSchedule.GetByIdAsync(j => j.VaccineScheduleId == id);
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
                _unitOfWork.VaccineSchedule.Remove(existingModel);
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
    }

}
