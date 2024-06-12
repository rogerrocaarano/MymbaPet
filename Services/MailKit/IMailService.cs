using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace c18_98_m_csharp.Services.MailKit;

public interface IMailService : IEmailSender
{
    Task SendEmailAsync(string email, string subject, string htmlMessage);
}
