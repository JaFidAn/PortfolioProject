using Application.Services;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;

namespace Infrastructure.Services;

public class EmailService : IEmailService
{
    private readonly IConfiguration _configuration;

    public EmailService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task SendEmailAsync(string toEmail, string subject, string body)
    {
        var emailSettings = _configuration.GetSection("EmailSettings");

        var fromEmail = emailSettings["UserName"];
        var password = emailSettings["Password"];
        var host = emailSettings["Host"];
        var port = int.TryParse(emailSettings["Port"], out var parsedPort) ? parsedPort : 587;
        var enableSsl = bool.TryParse(emailSettings["EnableSsl"], out var parsedSsl) && parsedSsl;

        if (string.IsNullOrWhiteSpace(fromEmail) || string.IsNullOrWhiteSpace(password))
        {
            throw new InvalidOperationException("SMTP settings are not configured properly.");
        }

        using var client = new SmtpClient(host, port)
        {
            Credentials = new NetworkCredential(fromEmail, password),
            EnableSsl = enableSsl
        };

        var message = new MailMessage(fromEmail, toEmail, subject, body)
        {
            IsBodyHtml = true
        };

        await client.SendMailAsync(message);
    }
}
