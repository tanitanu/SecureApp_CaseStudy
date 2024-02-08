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
    /// This is User Reported controller containing get by id user reported method, create and update user reported method. 
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UserReportedController : ControllerBase
    {
        private readonly DiscussionForumContext dbContext;
        private readonly IUserReported userReported;
        private readonly IMapper mapper;
        private readonly ILogger<UserReportedController> logger;

        public UserReportedController(DiscussionForumContext _dbContext,
            IUserReported _userReported,
            IMapper _mapper,
            ILogger<UserReportedController> _logger)
        {
            dbContext = _dbContext;
            userReported = _userReported;
            mapper = _mapper;
            logger = _logger;
        }
        // GET SINGLE USER REPORTED (Get User Reported By ID)
        // GET: https://localhost:portnumber/api/UserReported/{id}
        [HttpGet]
        [Route("{id}")]
        [Authorize(Roles = "User,Admin")]
        public async Task<IActionResult> GetById([FromRoute] string id)
        {
            try
            {
                var userReportedDomain = await userReported.GetByIdAsync(id);
                if (userReportedDomain == null) { return NotFound(); }
                return Ok(mapper.Map<DiscussionForumUserReportedDTO>(userReportedDomain));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        // GET ALL USER REPORTED
        // GET: https://localhost:portnumber/api/UserReported
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllUsersAsync()
        {
            try
            {
                var userReportedDomain = await userReported.GetAllReportedUsersAsync();
                if (userReportedDomain == null) { return NotFound(); }
                return Ok(userReportedDomain);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        // POST To Create New User Reported
        // POST: https://localhost:portnumber/api/UserReported
        [HttpPost]
        [ValidateModel]
        [Authorize(Roles = "User,Admin")]
        public async Task<IActionResult> Create([FromBody] AddUpdateUserReporterDTO addUpdateUserReporterDTO)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                addUpdateUserReporterDTO.ReporterUserId = userId;
                var userReportedDomain = mapper.Map<DiscussionForumUserReported>(addUpdateUserReporterDTO);
                userReportedDomain.CreatedBy = userId;
                userReportedDomain.UpdatedBy = userId;
                userReportedDomain = await userReported.CreateUpdateAsync(userReportedDomain);
                var userReportedDTO = mapper.Map<DiscussionForumUserReportedDTO>(userReportedDomain);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
