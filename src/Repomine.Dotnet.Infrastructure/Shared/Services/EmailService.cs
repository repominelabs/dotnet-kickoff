using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;
using Repomine.Dotnet.Core.Application.DTOs.Email;
using Repomine.Dotnet.Core.Application.Exceptions;
using Repomine.Dotnet.Core.Application.Interfaces.Shared;
using Repomine.Dotnet.Core.Domain.Wrappers;

namespace Repomine.Dotnet.Infrastructure.Shared.Services;

public class EmailService : IEmailService
{
    public EmailSettings _emailSettings { get; }
    public ILogger<EmailService> _logger { get; }

    public EmailService(IOptions<EmailSettings> emailSettings, ILogger<EmailService> logger)
    {
        _emailSettings = emailSettings.Value;
        _logger = logger;
    }

    public async Task SendEmailAsync(SendEmailAsyncRequest request)
    {
        try
        {
            // Create Html Body
            var builder = new BodyBuilder
            {
                HtmlBody = request.Body
            };

            // Create Mime Message
            var email = new MimeMessage
            {
                Sender = new MailboxAddress(_emailSettings.DisplayName, request.From ?? _emailSettings.From),
                Subject = request.Subject,
                Body = builder.ToMessageBody(),
            };
            email.To.Add(MailboxAddress.Parse(request.To));

            // Smtp Operations (Connect & Authenticate & Send Async & Disconnect)
            using var smtp = new SmtpClient();
            smtp.Connect(_emailSettings.Host, _emailSettings.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(_emailSettings.UserName, _emailSettings.Password);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, ex);
            throw new ApiException(ex.Message);
        }
    }
}