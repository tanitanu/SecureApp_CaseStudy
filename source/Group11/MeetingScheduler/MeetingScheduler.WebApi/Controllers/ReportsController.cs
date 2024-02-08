using Azure.Core;
using MeetingScheduler.Common;
using MeetingScheduler.Entities;
using MeetingScheduler.Model;
using MeetingScheduler.Model;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace MeetingScheduler.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly ILogger<ReportsController> _logger;
        private readonly IReportsHandler _reportsHandler;

        /// <summary>
        /// Reports Controller constuctor
        /// </summary>
        /// <param name="logger"></param>
        public ReportsController(ILogger<ReportsController> logger)
        {
            _logger = logger;
            _reportsHandler = new ReportsHandler();
        }

        /// <summary>
        /// Get Weekly Meetings bi user id and dates
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="customStartDate"></param>
        /// <param name="roleName"></param>
        /// <returns></returns>
        [HttpGet("GetWeeklyMeetings")]
        public async Task<IActionResult> GetWeeklyMeetings(int userId, DateTime customStartDate,string roleName)
        {
            try
            {
                _logger.LogInformation(Constants.GETWEEKLYMEETINGS_START);
                var response = await _reportsHandler.GetWeeklyMeetings(userId, customStartDate,roleName);
                _logger.LogInformation(Constants.GETWEEKLYMEETINGS_END);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(Constants.GETWEEKLYMEETINGS_EXCEPTION + " \n" + ex.ToString());
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Get Monthly Meetings  by user id,month and role
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="customMonth"></param>
        /// <param name="roleName"></param>
        /// <returns></returns>
        [HttpGet("GetMonthlyMeetings")]
        public async Task<IActionResult> GetMonthlyMeetings(int userId, DateTime customMonth, string roleName)
        {
            try
            {
                _logger.LogInformation(Constants.GETMONTHLY_START);
                var response = await _reportsHandler.GetMonthlyMeetings(userId, customMonth,roleName);
                _logger.LogInformation(Constants.GETUSERMEETINGS_END);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(Constants.GETMONTHLY_EXCEPTION + " \n" + ex.ToString());
                return BadRequest(ex.Message);
            }

        }

    }
}