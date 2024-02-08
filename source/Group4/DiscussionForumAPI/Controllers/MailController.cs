using DiscussionForumAPI.Contracts;
using DiscussionForumAPI.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DiscussionForumAPI.Controllers
{
    /// <summary author = Kirti Garg>
    /// This is Mail Controller containg send mail method.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class MailController : ControllerBase
    {
        private readonly IMail _mailService;
        //injecting the IMailService into the constructor
        public MailController(IMail _MailService)
        {
            _mailService = _MailService;
        }

        /// <summary Author = Kirti Garg>
        /// Add a POST Method to send off the email.
        /// </summary>
        /// <param name="mailData">MailData</param>
        /// <returns></returns>
        [HttpPost]
        [Route("SendMail")]
        public bool SendMail(MailData mailData)
        {
            return _mailService.SendMailAsync(mailData);
        }
    }
}
