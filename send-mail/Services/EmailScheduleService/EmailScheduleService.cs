using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using RazorEngineCore;
using send_mail.Model;
using send_mail.Services.EmailService;
using System.Text;

namespace send_mail.Services.EmailScheduleService
{
    public class EmailScheduleService : IEmailScheduleService
    {
        IMailService _mailService;

        public EmailScheduleService(IMailService mailService)
        {
            _mailService = mailService;
        }
    }
}
