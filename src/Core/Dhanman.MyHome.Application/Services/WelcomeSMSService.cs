using B2aTech.CrossCuttingConcern.Abstractions;
using Dhanman.MyHome.Application.Abstractions;
using Microsoft.Extensions.Logging;

namespace Dhanman.MyHome.Application.Services;

public class WelcomeSMSService : ISMSService
{
    private readonly IEmailService _emailService;
    private readonly IEmailTemplateService _emailTemplateService;
    private readonly ILogger<WelcomeSMSService> _logger;

    public WelcomeSMSService(
        IEmailService emailService,
        IEmailTemplateService emailTemplateService,
        ILogger<WelcomeSMSService> logger)
    {
        _emailService = emailService;
        _emailTemplateService = emailTemplateService;
        _logger = logger;
    }

    public async Task<bool> SendSMS(string recipient, string welcomeMessage)
    {
            var template = await _emailTemplateService.GetProcessedTemplateAsync(
                templateId: 1,
                new Dictionary<string, string>
                {
                        { "welcome_message", welcomeMessage }
                });

            await _emailService.SendEmailAsync(recipient, template.Subject, template.BodyHtml);

            return true;
    }
}
