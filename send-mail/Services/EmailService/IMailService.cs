using send_mail.Model;

namespace send_mail.Services.EmailService
{
    public interface IMailService
    {
        Task<bool> SendAsync(MailData mailData, CancellationToken ct);

        string GetEmailTemplate(string emailTemplate, WelcomeEmail emailTemplateModel);
    }
}
