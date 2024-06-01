using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;

namespace c18_98_m_csharp.Services.MailKit;

public class MailService : IMailService
{
    private readonly MailSettings _mailSettings;
    private readonly SmtpClient _client;
    public MailService(IOptions<MailSettings> mailSettingsOptions)
    {
        _mailSettings = mailSettingsOptions.Value;
        _client = new SmtpClient();
    }

    public async Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress(_mailSettings.SenderName, _mailSettings.SenderEmail));
        message.To.Add(MailboxAddress.Parse(email));
        message.Subject = subject;
        message.Body = new TextPart("html") { Text = htmlMessage };

        await _client.ConnectAsync(_mailSettings.Server, _mailSettings.Port, SecureSocketOptions.Auto);
        await _client.AuthenticateAsync(_mailSettings.UserName, _mailSettings.Password);
        await _client.SendAsync(message);
        await _client.DisconnectAsync(true);
    }
}
