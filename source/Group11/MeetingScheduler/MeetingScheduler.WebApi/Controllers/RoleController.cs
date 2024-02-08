using MeetingScheduler.Common;
using MeetingScheduler.Entities;
using MeetingScheduler.Model;
using Microsoft.AspNetCore.Mvc;

namespace MeetingScheduler.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly ILogger<RoleController> _logger; //logger object
        private readonly IRoleHandler _roleHandler; //role handler object

        /// <summary>
        /// Role Controller constructor
        /// </summary>
        /// <param name="logger"></param>
        public RoleController(ILogger<RoleController> logger)
        {
            _logger = logger;
            _roleHandler = new RoleHandler();
        }

        /// <summary>
        /// Get Role by id
        /// </summary>
        /// <param name="userLogin"></param>
        /// <returns></returns>
        [HttpPost("GetRole")]
        public async Task<IActionResult> GetRole(UserLogin userLogin)
        {
            try
            {
                _logger.LogInformation(Constants.GETROLE_START);
                var response = await _roleHandler.GetRole(userLogin);
                _logger.LogInformation(Constants.GETROLE_END);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(Constants.GETROLE_EXCEPTION + " \n" + ex.ToString());
                return BadRequest(ex.Message);
            }

        }

        /// <summary>
        /// Get all avaialble roles
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetRoles")]
        public async Task<IActionResult> GetRoles()
        {
            try
            {
                _logger.LogInformation(Constants.GETROLES_START);
                var response = await _roleHandler.GetRoles();
                _logger.LogInformation(Constants.GETROLES_END);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(Constants.GETROLES_EXCEPTION + " \n" + ex.ToString());
                return BadRequest(ex.Message);
            }

        }

    }
}
