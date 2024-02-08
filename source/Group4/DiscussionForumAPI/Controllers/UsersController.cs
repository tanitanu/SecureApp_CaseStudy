using AutoMapper;
using DiscussionForumAPI.Contracts;
using DiscussionForumAPI.Models;
using DiscussionForumAPI.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Security.Claims;

namespace DiscussionForumAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly DiscussionForumContext dbContext;
        private readonly IUser user;
        private readonly ILogger<UsersController> logger;
        public UsersController(DiscussionForumContext _dbContext,
            IUser _user,
            ILogger<UsersController> _logger)
        {
            dbContext = _dbContext;
            user = _user;
            logger = _logger;
        }

        // GET ALL USER
        // GET: https://localhost:portnumber/api/user/
        [HttpGet]
        [Route("GetUsersList")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var userDomain = await user.GetAllUsersAsync();
                return Ok(userDomain);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // In-Active User
        // DELETE: https://localhost:portnumber/api/user/{id}
        [HttpDelete]
        [Route("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete([FromRoute] string id)
        {
            try
            {
                var userDomainModel = await user.DeleteAsync(id);

                if (userDomainModel == null)
                {
                    return NotFound();
                }
                return Ok("Deleted Successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
