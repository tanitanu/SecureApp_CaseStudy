using Azure.Core;
using MeetingScheduler.Common;
using MeetingScheduler.Entities;
using MeetingScheduler.Model;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace MeetingScheduler.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MeetingController : ControllerBase
    {
        private readonly ILogger<MeetingController> _logger; //logger object
        private readonly IMeetingHandler _meetingHandler; //Meeting handler

        /// <summary>
        /// Meeting Controller constructor
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="meetingHandler"></param>
        public MeetingController(ILogger<MeetingController> logger, IMeetingHandler meetingHandler)
        {
            _logger = logger;
            _meetingHandler = meetingHandler;
        }

        /// <summary>
        /// Get All Meetings Details
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAllMeetings")]
        public async Task<IActionResult> GetAllMeetings()
        {
            try
            {
                _logger.LogInformation(Constants.GETALLMEETINGS_START);
                var response = await _meetingHandler.GetAllMeetings();
                _logger.LogInformation(Constants.GETALLMEETINGS_END);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(Constants.GETALLMEETINGS_EXCEPTION + " \n" + ex.ToString());
                return BadRequest(ex.Message);
            }

        }

        /// <summary>
        /// Get User Meetings by user id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet("GetUserMeetings/{userId}")]
        public async Task<IActionResult> GetUserMeetings(int userId)
        {
            try
            {
                _logger.LogInformation(Constants.GETUSERMEETINGS_START);
                var response = await _meetingHandler.GetUserMeetings(userId);
                _logger.LogInformation(Constants.GETUSERMEETINGS_END);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(Constants.GETUSERMEETINGS_EXCEPTION + " \n" + ex.ToString());
                return BadRequest(ex.Message);
            }

        }

        /// <summary>
        /// Create new meeting
        /// </summary>
        /// <param name="createMeetingRequest"></param>
        /// <returns></returns>
        [HttpPost("CreateMeeting")]
        public async Task<IActionResult> CreateMeeting(MeetingDTO createMeetingRequest)
        {
            try
            {
                _logger.LogInformation(Constants.CREATEMEETING_START);
                var response = await _meetingHandler.CreateMeeting(createMeetingRequest);
                _logger.LogInformation(Constants.CREATEMEETING_END);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(Constants.CREATEMEETING_EXCEPTION + " \n" + ex.ToString());
                return BadRequest(ex.Message);
            }

        }

        /// <summary>
        /// Update Meeting Details
        /// </summary>
        /// <param name="meetingRequest"></param>
        /// <returns></returns>
        [HttpPost("UpdateMeetingDetails")]
        public async Task<IActionResult> UpdateMeetingDetails(MeetingDTO meetingRequest)
        {
            try
            {
                _logger.LogInformation(Constants.UPDATEMEETING_START);
                var response = await _meetingHandler.UpdateMeeting(meetingRequest);
                _logger.LogInformation(Constants.UPDATEMEETING_END);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(Constants.UPADTEMEETING_EXCEPTION + " \n" + ex.ToString());
                return BadRequest(ex.Message);
            }

        }

        /// <summary>
        /// Delete meeting by meeting id
        /// </summary>
        /// <param name="meetingId"></param>
        /// <returns></returns>
        [HttpDelete("DeleteMeeting/{meetingId}")]

        public async Task<IActionResult> DeleteMeeting(int meetingId)
        {
            try
            {
                _logger.LogInformation(Constants.DELETEMEETING_START);
                var response = await _meetingHandler.DeleteMeeting(meetingId);
                _logger.LogInformation(Constants.DELETEMEETING_END);
                return Ok(response);


            }
            catch (Exception ex)
            {
                _logger.LogError(Constants.DELETEMEETING_EXCEPTION + " \n" + ex.ToString());
                return BadRequest(ex.Message);
            }

        }

    }
}