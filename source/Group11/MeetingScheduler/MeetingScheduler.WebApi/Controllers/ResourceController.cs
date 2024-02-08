
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
    public class ResourceController : ControllerBase
    {

        private readonly ILogger<ResourceController> _logger; //logger object
        private readonly IResourceHandler _resourceHandler; //handler object to 

        /// <summary>
        /// Resource Controller constructor
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="resourceHandler"></param>
        public ResourceController(ILogger<ResourceController> logger, IResourceHandler resourceHandler)
        {
            _logger = logger;
            _resourceHandler = resourceHandler;
        }
        /// <summary>
        /// Create meeting resources
        /// </summary>
        /// <param name="createResourceRequest"></param>
        /// <returns></returns>
        [HttpPost("CreateResources")]
        public async Task<IActionResult> CreateResources(Resource createResourceRequest)
        {
            try
            {
                _logger.LogInformation(Constants.CREATERESOURCES_START);
                var response = await _resourceHandler.CreateResources(createResourceRequest);
                _logger.LogInformation(Constants.CREATERESOURCES_END);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(Constants.CREATERESOURCES_EXCEPTION + " \n" + ex.ToString());
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Get meeting resources by meeting id
        /// </summary>
        /// <param name="MeetingId"></param>
        /// <returns></returns>
        [HttpGet("{MeetingId}")]
        public async Task<ActionResult> GetResourcesByMeeting(int MeetingId)
        {
            try
            {
                _logger.LogInformation(Constants.GETRESOURCESBYMEETING_START);
                var response = await _resourceHandler.GetResourcesByMeeting(MeetingId);
                _logger.LogInformation(Constants.GETRESOURCESBYMEETING_END); ;
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(Constants.GETRESOURCESBYMEETING_EXCEPTION);
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// Update meeting resource details
        /// </summary>
        /// <param name="resourceRequest"></param>
        /// <returns></returns>
        [HttpPut("UpdateResourceDetails")]
        public async Task<IActionResult> UpdateResourceDetails(Resource resourceRequest)
        {
            try
            {
                _logger.LogInformation(Constants.UPDATERESOURCEDETAILS_START);
                var response = await _resourceHandler.UpdateResource(resourceRequest);
                _logger.LogInformation(Constants.UPDATERESOURCEDETAILS_END);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(Constants.UPDATERESOURCEDETAILS_EXCEPTION + " \n" + ex.ToString());
                return BadRequest(ex.Message);
            }

        }

        /// <summary>
        /// Get resource details
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetResourceDetails()
        {
            try
            {
                _logger.LogInformation(Constants.GETRESOURCEDETAILS_START);
                var response = await _resourceHandler.GetResourceDetails();
                _logger.LogInformation(Constants.GETRESOURCEDETAILS_END);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(Constants.GETRESOURCEDETAILS_EXCEPTION  + " \n" + ex.ToString());
                return BadRequest(ex.Message);
            }

        }
        /// <summary>
        /// Delete meeting resource
        /// </summary>
        /// <param name="deleteResourceRequest"></param>
        /// <returns></returns>
        [HttpDelete("DeleteResource")]
        public async Task<IActionResult> DeleteResource(Resource deleteResourceRequest)
        {
            try
            {
                _logger.LogInformation(Constants.DELETERESOURCE_START);
                var response = await _resourceHandler.DeleteResource(deleteResourceRequest);
                _logger.LogInformation(Constants.DELETERESOURCE_END);
                return Ok(response);


            }
            catch (Exception ex)
            {
                _logger.LogError(Constants.DELETERESOURCE_EXCEPTION + " \n" + ex.ToString());
                return BadRequest(ex.Message);
            }

        }

    }
}