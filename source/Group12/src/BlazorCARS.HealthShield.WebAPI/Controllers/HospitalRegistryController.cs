using AutoMapper;
using BlazorCARS.HealthShield.DAL.Entity;
using BlazorCARS.HealthShield.DAL.UnitOfWork;
using BlazorCARS.HealthShield.Utility.RequestValidator;
using BlazorCARS.HealthShield.Utility.Response;
using BlazorCARS.HealthShield.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using BlazorCARS.HealthShield.DataObject.DTO;
using BlazorCARS.HealthShield.Utility.Extensions;
using Microsoft.AspNetCore.Authorization;

namespace BlazorCARS.HealthShield.WebAPI.Controllers
{
    [Route("api/hospitalRegistry")]
    [ApiController]
    [AllowAnonymous]
    public class HospitalRegistryController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<HospitalRegistryController> _logger;
        private readonly APIResponse _response;
        private const string _EntityName = "Hospital Registry";
        public HospitalRegistryController(IMapper mapper, IUnitOfWork unitOfWrok, ILogger<HospitalRegistryController> logger)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWrok;
            _logger = logger;
            _response = new APIResponse();
        }

        #region Create/Update/Delete operations
        /// <summary>
        /// 
        /// </summary>
        /// <param name="requestDTO"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> CreateAsync([FromBody] HospitalRegistryDTO requestDTO)
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
                if (dupcheck != null)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    _response.ErrorMessages.Add($"UserName {requestDTO.UserName} already exist.");
                    _logger.LogWarning(string.Join(", ", _response.ErrorMessages));
                    return BadRequest(_response);
                }
                if (await _unitOfWork.Hospital.GetByIdAsync(x => x.Name.ToLower() == requestDTO.Name.ToLower()) != null)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    _response.ErrorMessages.Add($"{_EntityName} already exist and either Acive or Inacive.");
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
                Hospital model = _mapper.Map<Hospital>(requestDTO);
                model.CreatedDateTime = DateTime.UtcNow;
                model.UpdatedDateTime = DateTime.UtcNow;
                model.UpdatedUser = requestDTO.CreatedUser;
                model.RegistrationStatus = DataObject.Enums.RegistrationStatus.Submitted.ToString();
                await _unitOfWork.Hospital.AddAsync(model);
                await _unitOfWork.CommitAsync();
                //Create credential
                UserStore userStore = new()
                {
                    UserName = requestDTO.UserName,
                    Password = encryptedPassword,
                    CreatedUser = requestDTO.CreatedUser,
                    CreatedDateTime = DateTime.UtcNow,
                    UpdatedDateTime = DateTime.UtcNow,
                    UpdatedUser = requestDTO.CreatedUser,
                    DiscriminationId = model.HospitalId,
                    UserRoleId = (int)DataObject.Enums.UserRole.Admin
                };
                await _unitOfWork.UserStore.AddAsync(userStore);
                await _unitOfWork.CommitAsync();

                _response.StatusCode = HttpStatusCode.OK;
                _response.IsSuccess = true;
                _response.Result = null;// _mapper.Map<InventoryDTO>(model);
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
        private async Task<bool> CreateRequestValidator(HospitalRegistryDTO model)
        {
            string result;
            result = model.CreatedUser.NullOrWhiteSpace("Create User");
            if (result != string.Empty)
            {
                _response.ErrorMessages.Add(result);
            }
            result = model.UserName.NullOrWhiteSpace("User Name");
            if (result != string.Empty)
            {
                _response.ErrorMessages.Add(result);
            }
            result = model.Password.NullOrWhiteSpace("Password");
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        private void RequestValidator(HospitalRequest model)
        {
            string result;
            result = model.Name.NullOrWhiteSpace("Hospital Name");
            if (result != string.Empty)
            {
                _response.ErrorMessages.Add(result);
            }
            result = model.LicenseNo.NullOrWhiteSpace("License Number");
            if (result != string.Empty)
            {
                _response.ErrorMessages.Add(result);
            }
            result = model.Address1.NullOrWhiteSpace("Address1");
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
            result = model.CountryId.GreaterThan(0, "Country");
            if (result != string.Empty)
            {
                _response.ErrorMessages.Add(result);
            }
            result = model.StateId.GreaterThan(0, "State");
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
            result = model.EmailId.NullOrWhiteSpace("Email");
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
        private async Task<bool> RequestDependentValidator(HospitalRequest model)
        {
            var UserModel = await _unitOfWork.UserStore.GetByIdAsync(x => x.UserName == model.UserName);
            if (UserModel != null)
            {
                _response.ErrorMessages.Add("User Name already taken. Please try differently.");
            }
            var CountryModel = await _unitOfWork.Country.GetByIdAsync(x => x.CountryId == model.CountryId);
            if (CountryModel == null)
            {
                _response.ErrorMessages.Add("Selected Country is invalid.");
            }
            else if (CountryModel != null && CountryModel.IsDeleted)
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
