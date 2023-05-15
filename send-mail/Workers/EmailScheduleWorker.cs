using Microsoft.Extensions.Configuration;
using send_mail.Model;
using send_mail.Services.EmailScheduleService;
using send_mail.Services.EmailService;

namespace send_mail.Workers
{
    public class EmailScheduleWorker : IHostedService, IDisposable
    {
        readonly ILogger<EmailScheduleWorker> _logger;
        readonly IConfiguration _configuration;
        readonly IMailService _mailService;
        readonly List<DateTime> _schedules;

        public EmailScheduleWorker(IMailService mailService, IConfiguration configuration, ILogger<EmailScheduleWorker> logger)
        {
            _mailService = mailService;
            _configuration = configuration;
            _logger = logger;
            _schedules = new List<DateTime> { new DateTime(2023, 05, 15, 12, 55, 0, DateTimeKind.Local) };
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Service EmailScheduleWorker Start...");
            while (!cancellationToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at:{time}...{delay}", DateTimeOffset.UtcNow, int.Parse(_configuration["EmailScheduleDelay"]));
                await ProcessSendMailSchedule(cancellationToken);
                await Task.Delay(int.Parse(_configuration["EmailScheduleDelay"]), cancellationToken);
            }
        }

        public async Task ProcessSendMailSchedule(CancellationToken cancellationToken)
        {
            //_mailService.SendAsync();
            WelcomeEmail welcomeMail = new WelcomeEmail() { Email = "haidv2801@gmail.com", Name = "Do Van Hai" };
            MailData mailData = new MailData(new List<string> { "haidv2801@gmail.com" }, "Test schedule email", _mailService.GetEmailTemplate("Welcome", welcomeMail));

            await _mailService.SendAsync(mailData, cancellationToken);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Service EmailScheduleWorker Stop...");
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
