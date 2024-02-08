using AutoMapper;
using DiscussionForumAPI.Contracts;
using DiscussionForumAPI.Models;
using DiscussionForumAPI.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace DiscussionForumAPI.Controllers
{
    /// <summary author = Kirti Garg>
    /// This is Report controller containing report method.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly DiscussionForumContext dbContext;
        private readonly IReport report;
        private readonly ILogger<ReportController> logger;

        public ReportController(DiscussionForumContext _dbContext,IReport _report,ILogger<ReportController> _logger)
        {
            dbContext = _dbContext;
            report = _report;
            logger = _logger;
        }

        // GET Report
        // GET: https://localhost:portnumber/api/report/{status}/{reportType}/{fromDate}/{toDate}
        [HttpGet]
        [Route("{status}/{reportType}/{fromDate}/{toDate?}")]
        [Authorize(Roles = "Admin")] 
        public async Task<IActionResult> Get([FromRoute] string status, [FromRoute] string reportType, [FromRoute] string fromDate, [FromRoute] string? toDate=null)
        {
            try
            {
                var reportDomain = await report.GetAsync(status,reportType, fromDate, toDate);
                return Ok(reportDomain);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
