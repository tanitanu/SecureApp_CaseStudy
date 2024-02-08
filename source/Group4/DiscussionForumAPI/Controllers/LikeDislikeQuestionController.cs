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
    /// This is Like Dislike controller containing get by id like dislike question method, create like dislike question method,
    /// update like dislike question method. 
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class LikeDislikeQuestionController : ControllerBase
    {
        private readonly DiscussionForumContext dbContext;
        private readonly ILikeDislikeQuestion likeDislikeQuestion;
        private readonly IMapper mapper;
        private readonly ILogger<LikeDislikeQuestionController> logger;

        public LikeDislikeQuestionController(DiscussionForumContext _dbContext,
            ILikeDislikeQuestion _likeDislikeQuestion,
            IMapper _mapper,
            ILogger<LikeDislikeQuestionController> _logger)
        {
            dbContext = _dbContext;
            likeDislikeQuestion = _likeDislikeQuestion;
            mapper = _mapper;
            logger = _logger;
        }

        // GET SINGLE LIKE DISLIKE (Get Like Dislike By ID)
        // GET: https://localhost:portnumber/api/likedislikequestion/{id}
        [HttpGet]
        [Route("{id}")]
        [Authorize(Roles = "User,Admin")]
        public async Task<IActionResult> GetById([FromRoute] string id)
        {
            try
            {
                var likesDislikeDomain = await likeDislikeQuestion.GetByIdAsync(id);
                if (likesDislikeDomain == null) { return NotFound(); }
                return Ok(mapper.Map<DiscussionForumLikeDislikeDTO>(likesDislikeDomain));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        // POST To Create New Like Dislike
        // POST: https://localhost:portnumber/api/likedislikequestion
        [HttpPost]
        [ValidateModel]
        [Authorize(Roles = "User,Admin")]
        public async Task<IActionResult> Create([FromBody] AddLikeDislikeQuestionDTO addLikeDislikeQuestionDTO)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var likesDislikeDomain = mapper.Map<DiscussionForumLikesDislike>(addLikeDislikeQuestionDTO);
                likesDislikeDomain.CreatedBy = userId;
                likesDislikeDomain.UpdatedBy = userId;
                likesDislikeDomain = await likeDislikeQuestion.CreateUpdateAsync(likesDislikeDomain);
                var likesDislikeDTO = mapper.Map<DiscussionForumLikeDislikeDTO>(likesDislikeDomain);
                return CreatedAtAction(nameof(GetById), new { id = likesDislikeDTO.LikeDislikeId }, likesDislikeDTO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}
