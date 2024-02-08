using AutoMapper;
using DiscussionForumAPI.Contracts;
using DiscussionForumAPI.CustomActionFilters;
using DiscussionForumAPI.Models;
using DiscussionForumAPI.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DiscussionForumAPI.Controllers
{
    /// <summary author = Kirti Garg>
    /// This is Question controller containing get by id question method, create question method,
    /// update question method and delete question method.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        private readonly DiscussionForumContext dbContext;
        private readonly IQuestion question;
        private readonly IMapper mapper;
        private readonly ILogger<QuestionController> logger;

        public QuestionController(DiscussionForumContext _dbContext,
            IQuestion _question,
            IMapper _mapper,
            ILogger<QuestionController> _logger)
        {
            dbContext = _dbContext;
            question = _question;
            mapper = _mapper;
            logger = _logger;
        }

        // GET ALL QUESTION FOR USER
        // GET: https://localhost:portnumber/api/question/
        [HttpGet]
        [Route("GetQuestionsList")]
        [Authorize(Roles = "User,Admin")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var questionDomain = await question.GetAllAsync();
                return Ok(questionDomain);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET SINGLE QUESTION ANSWER (Get Question Answer By ID)
        // GET: https://localhost:portnumber/api/question/GetQuestionAnswerDetailsById/{id}
        [HttpGet]
        [Route("GetQuestionAnswerDetailsById/{id}")]
        [Authorize(Roles = "User,Admin")]
        public async Task<IActionResult> GetQuestionAnswerDetailsById([FromRoute] string id)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var questionAnswerDomain = await question.GetQuestionAnswerDetails(id, userId);
                if (questionAnswerDomain == null) { return NotFound(); }
                return Ok(questionAnswerDomain);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


        // GET SINGLE QUESTION (Get Question By ID)
        // GET: https://localhost:portnumber/api/question/{id}
        [HttpGet]
        [Route("{id}")]
        [Authorize(Roles = "User,Admin")]
        public async Task<IActionResult> GetById([FromRoute] string id)
        {
            try
            {
                var questionDomain = await question.GetByIdAsync(id);
                if (questionDomain == null) { return NotFound(); }
                return Ok(mapper.Map<DiscussionForumQuestionDTO>(questionDomain));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        // POST To Create New Question
        // POST: https://localhost:portnumber/api/question
        [HttpPost]
        [ValidateModel]
        [Authorize(Roles = "User,Admin")]
        public async Task<IActionResult> Create([FromBody] AddQuestionDTO addQuestionDTO)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var questionDomainModel = mapper.Map<DiscussionForumQuestion>(addQuestionDTO);
                questionDomainModel.CreatedBy = userId;
                questionDomainModel = await question.CreateAsync(questionDomainModel);
                var questionDto = mapper.Map<DiscussionForumQuestionDTO>(questionDomainModel);
                if (questionDto.QuestionId == null)
                {

                    return BadRequest("Question Already Exists!");
                }
                else
                {
                    return CreatedAtAction(nameof(GetById), new { id = questionDto.QuestionId }, questionDto);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Update Question
        // PUT: https://localhost:portnumber/api/question/{id}
        [HttpPut]
        [Route("{id}")]
        [ValidateModel]
        [Authorize(Roles = "User,Admin")]
        public async Task<IActionResult> Update([FromRoute] string id, [FromBody] UpdateQuestionDTO updateQuestionDTO)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var questionDomainModel = mapper.Map<DiscussionForumQuestion>(updateQuestionDTO);
                questionDomainModel.UpdatedBy = userId;
                questionDomainModel = await question.UpdateAsync(id, questionDomainModel);
                if (questionDomainModel == null)
                {
                    return BadRequest("Question doesn't exists!");
                }
                return Ok(mapper.Map<UpdateQuestionDTO>(questionDomainModel));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        // Delete Question
        // DELETE: https://localhost:portnumber/api/question/{id}
        [HttpDelete]
        [Route("{id}")]
        [Authorize(Roles = "User,Admin")]
        public async Task<IActionResult> Delete([FromRoute] string id)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var questionDomainModel = await question.DeleteAsync(id, userId);

                if (questionDomainModel == null)
                {
                    return BadRequest("Question doesn't exists!");
                }

                return Ok(mapper.Map<DiscussionForumQuestionDTO>(questionDomainModel));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET ALL CATEGORIES
        // GET: https://localhost:portnumber/api/question/
        [HttpGet]
        [Route("GetCategories")]
        [Authorize(Roles = "User,Admin")]
        public async Task<IActionResult> GetCategories()
        {
            try
            {
                var questionCategoriesDomain = await question.GetAllCategories();
                return Ok(mapper.Map<List<DiscussionForumCategoriesDTO>>(questionCategoriesDomain));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Update Status By Question
        // UPDATE: https://localhost:portnumber/api/question/status/{id}
        [HttpPost]
        [Route("Status/{id}")]
        [Authorize(Roles = "User,Admin")]
        public async Task<IActionResult> Post([FromRoute] string id)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var questionDomainModel = await question.UpdateAsync(id, userId);

                if (questionDomainModel == null)
                {
                    return BadRequest("Question doesn't exists!");
                }

                return Ok(mapper.Map<DiscussionForumQuestionDTO>(questionDomainModel));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}
