using Amazon;
using Amazon.Runtime;
using Amazon.SimpleEmailV2;
using Amazon.SimpleEmailV2.Model;
using EnglishTutorAI.Application.Configurations;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace EnglishTutorAI.Application.Services;

public class EmailService : IEmailSender
{
    private readonly IAmazonSimpleEmailServiceV2 _client;
    private readonly EmailSettings _emailSettings;
    private readonly ILogger<EmailService> _logger;

    public EmailService(
        IAmazonSimpleEmailServiceV2 client,
        ILogger<EmailService> logger,
        IOptions<EmailSettings> emailSettings)
    {
        _client = client;
        _logger = logger;
        _emailSettings = emailSettings.Value;
    }

    public async Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        var sendRequest = new SendEmailRequest
        {
            FromEmailAddress = _emailSettings.FromAddress,
            Destination = new Destination
            {
                ToAddresses = [email]
            },
            Content = new EmailContent
            {
                Simple = new Message
                {
                    Subject = new Content
                    {
                        Data = subject
                    },
                    Body = new Body
                    {
                        Html = new Content
                        {
                            Data = htmlMessage
                        }
                    }
                }
            }
        };

        try
        {
            await _client.SendEmailAsync(sendRequest);
            _logger.LogInformation("Email sent successfully to {Recipient}", email);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to send email to {Recipient}", email);
        }
    }
}