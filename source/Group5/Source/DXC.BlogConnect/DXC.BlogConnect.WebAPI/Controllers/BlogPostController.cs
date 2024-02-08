using AutoMapper;
using DXC.BlogConnect.DTO;
using DXC.BlogConnect.WebAPI.Models.Domain;
using DXC.BlogConnect.WebAPI.Models.DTO;
using DXC.BlogConnect.WebAPI.Services;
using DXC.BlogConnect.WebAPI.Services.Interfaces;
using DXC.BlogConnect.WebAPI.Utilities;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using static DXC.BlogConnect.DTO.ErrorCode;

namespace DXC.BlogConnect.WebAPI.Controllers
{
    [Route("api/blogposts")]
    [ApiController]
    public class BlogPostController : ControllerBase
    {
        private readonly IBlogPostService _blogPostService;
        private readonly IMapper _mapper;
        private readonly IValidator<BlogPostAddDTO> _blogPostValidator;
        private readonly ILogger<BlogPostController> _logger;
        private readonly List<Error> _errorMessages;
        public BlogPostController(IBlogPostService blogPostService, IMapper mapper, IValidator<BlogPostAddDTO> blogPostValidator, ILogger<BlogPostController> logger)
        {
            _blogPostService = blogPostService;
            _mapper = mapper;
            _blogPostValidator = blogPostValidator;
            _errorMessages = new List<Error>();
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetAllAsync()
        {
            var _response = new APIResponse<BlogPostGetDTO>();
            try
            {

                var model = await _blogPostService.GetAllAsync();

                _response.Result = _mapper.Map<List<BlogPostGetDTO>>(model);
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

        [HttpGet]
        [Route("GetByTagName")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetAllAsync(string tagName)
        {
            var _response = new APIResponse<BlogPostGetDTO>();
            try
            {

                var model = await _blogPostService.GetAllAsync(tagName);

                _response.Result = _mapper.Map<List<BlogPostGetDTO>>(model);
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


        [HttpPost]
        [Route("Add")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> CreateAsync([FromBody] BlogPostAddDTO createDTO)
        {
            var _response = new APIResponse<BlogPostAddDTO>();
            try
            {
                var validationResult = _blogPostValidator.Validate(createDTO);
                if (!validationResult.IsValid)
                {
                    var errors = validationResult.Errors.Select(e => new Error(e.ErrorCode, e.ErrorMessage));
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    _response.ErrorMessages = errors;
                    return BadRequest(_response);
                }


                var model = _mapper.Map<Post>(createDTO);
                model.CreatedDate = DateTime.Now;
                model.UpdatedDate = DateTime.Now;
                model.CreatedBy = createDTO.UserName;
                model.UpdatedBy = createDTO.UserName;


                await _blogPostService.AddAsync(model);

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

        /// <summary>
        /// Get user by id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet("{userId}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetUserById(int blogId)
        {
            var _response = new APIResponse<BlogPostDTO>();
            try
            {
                var userDetails = await _blogPostService.GetAsync(blogId);

                if (userDetails != null)
                {
                    _response.StatusCode = HttpStatusCode.OK;
                    _response.IsSuccess = true;
                    _response.Result = _mapper.Map<List<BlogPostDTO>>(userDetails);
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
    }

}
