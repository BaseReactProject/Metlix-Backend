

using Core.Mailing;

namespace Application.Services.MailService;

public abstract class MailServiceBase
{
    public abstract  Task SendEmailAsync(Mail mail);
    public abstract void SendEmail(Mail mail);
}
