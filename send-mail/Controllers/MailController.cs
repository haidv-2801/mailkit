using Microsoft.AspNetCore.Mvc;
using send_mail.Model;
using send_mail.Services.EmailService;
using System.Collections.Generic;

namespace send_mail.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MailController : ControllerBase
    {
        private readonly IMailService _mail;

        public MailController(IMailService mail)
        {
            _mail = mail;
        }

        [HttpPost("sendmail")]
        public async Task<IActionResult> SendMailAsync(MailData mailData)
        {
            bool result = await _mail.SendAsync(mailData, new CancellationToken());

            if (result)
            {
                return StatusCode(StatusCodes.Status200OK, "Mail has successfully been sent.");
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occured. The Mail could not be sent.");
            }
        }


        [HttpPost("sendemailusingtemplate")]
        public async Task<IActionResult> SendEmailUsingTemplate(WelcomeEmail welcomeMail)
        {
            // Create MailData object
            MailData mailData = new MailData(
                new List<string> { welcomeMail.Email },
                "Welcome to the MailKit Demo",
                _mail.GetEmailTemplate("Welcome", welcomeMail));


            bool sendResult = await _mail.SendAsync(mailData, new CancellationToken());

            if (sendResult)
            {
                return StatusCode(StatusCodes.Status200OK, "Mail has successfully been sent using template.");
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occured. The Mail could not be sent.");
            }
        }
    }
}