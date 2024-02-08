using AutoMapper;
using DiscussionForumAPI.Contracts;
using DiscussionForumAPI.CustomActionFilters;
using DiscussionForumAPI.Models;
using DiscussionForumAPI.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Security.Claims;

namespace DiscussionForumAPI.Controllers
{
    /// <summary author = Kirti Garg>
    /// This is Answer controller containing get by id answer method, create answer method,
    /// update answer method and delete answer method. 
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AnswerController : ControllerBase
    {
        private readonly DiscussionForumContext dbContext;
        private readonly IAnswer answer;
        private readonly IMapper mapper;
        private readonly ILogger<AnswerController> logger;

        public AnswerController(DiscussionForumContext _dbContext,
            IAnswer _answer,
            IMapper _mapper,
            ILogger<AnswerController> _logger)
        {
            dbContext = _dbContext;
            answer = _answer;
            mapper = _mapper;
            logger = _logger;
        }

        // GET SINGLE ANSWER (Get Answer By ID)
        // GET: https://localhost:portnumber/api/answer/{id}
        [HttpGet]
        [Route("{id}")]
        [Authorize(Roles = "User,Admin")]
        public async Task<IActionResult> GetById([FromRoute] string id)
        {
            try
            {
                var answerDomain = await answer.GetByIdAsync(id);
                if (answerDomain == null) { return NotFound(); }
                return Ok(mapper.Map<DiscussionForumAnswerDTO>(answerDomain));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        // POST To Create New Answer
        // POST: https://localhost:portnumber/api/answer
        [HttpPost]
        [ValidateModel]
        [Authorize(Roles = "User,Admin")]
        public async Task<IActionResult> Create([FromBody] AddAnswerDTO addAnswerDTO)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var answerDomainModel = mapper.Map<DiscussionForumAnswer>(addAnswerDTO);
                answerDomainModel.CreatedBy = userId;
                answerDomainModel = await answer.CreateAsync(answerDomainModel);
                var answerDTO = mapper.Map<DiscussionForumAnswerDTO>(answerDomainModel);
                if (answerDTO.AnswerId == null)
                {
                    return BadRequest("Answer Already Exists!");
                }
                else
                {
                    return CreatedAtAction(nameof(GetById), new { id = answerDTO.AnswerId }, answerDTO);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Update Answer
        // PUT: https://localhost:portnumber/api/answer/{id}
        [HttpPut]
        [Route("{id}")]
        [ValidateModel]
        [Authorize(Roles = "User,Admin")]
        public async Task<IActionResult> Update([FromRoute] string id, [FromBody] UpdateAnswerDTO updateAnswerDTO)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var answerDomainModel = mapper.Map<DiscussionForumAnswer>(updateAnswerDTO);
                answerDomainModel.UpdatedBy = userId;
                answerDomainModel = await answer.UpdateAsync(id, answerDomainModel);
                if (answerDomainModel == null)
                {
                    return NotFound();
                }
                return Ok(mapper.Map<UpdateAnswerDTO>(answerDomainModel));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Delete Answer
        // DELETE: https://localhost:portnumber/api/answer/{id}
        [HttpDelete]
        [Route("{id}")]
        [Authorize(Roles = "User,Admin")]
        public async Task<IActionResult> Delete([FromRoute] string id)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var answerDomainModel = await answer.DeleteAsync(id, userId);

                if (answerDomainModel == null)
                {
                    return NotFound();
                }

                return Ok(mapper.Map<DiscussionForumAnswerDTO>(answerDomainModel));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET SINGLE ANSWER (Get Answer By ID)
        // GET: https://localhost:portnumber/api/answer/GetQuestionAnswerDetailsById/{questionId}/{answerId}
        [HttpGet]
        [Route("GetQuestionAnswerDetailsById/{questionId}/{answerId}")]
        [Authorize(Roles = "User,Admin")]
        public async Task<IActionResult> GetQuesAnsById([FromRoute] string questionId, [FromRoute] string answerId)
        {
            try
            {
                var answerDomain = await answer.GetQuesAnsByIdAsync(questionId, answerId);
                if (answerDomain == null) { return NotFound(); }
                return Ok(answerDomain);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
