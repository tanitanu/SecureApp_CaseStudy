using MeetingScheduler.Entities.Email;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace MeetingScheduler.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        //private readonly IEmail _emailService;

        //public EmailController(IEmail email)
        //{
        //    _emailService = email;
        //}

        //[HttpPost("SendEmail")]

        //public IActionResult SendEmail(EmailMessage emailMessage)
        //{
        //    _emailService.SendEmail(emailMessage);
        //    return Ok();
        //}
    }
}
