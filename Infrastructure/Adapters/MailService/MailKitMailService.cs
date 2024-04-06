using Microsoft.Extensions.Configuration;
using MailKit.Net.Smtp;
using MimeKit;
using Core.Mailing;
using Application.Services.MailService;

namespace Infrastructure.Adapters.MailService;

public class MailKitMailService:MailServiceBase
{
    private readonly MailSettings _emailSettings;
    public MailKitMailService(IConfiguration configuration)
    {
        const string configurationSection = "MailSettings";
        _emailSettings =
            configuration.GetSection(configurationSection).Get<MailSettings>()
            ?? throw new NullReferenceException($"\"{configurationSection}\" section cannot found in configuration.");
    }
    public override void SendEmail(Core.Mailing.Mail mail)
    {
        var email = new MimeMessage();
        email.From.Add(new MailboxAddress(_emailSettings.SenderFullName, _emailSettings.SenderEmail));
        email.To.AddRange(mail.ToList);
        email.Subject = mail.Subject;
        BodyBuilder bodyBuilder = new() { HtmlBody = mail.HtmlBody, TextBody = mail.TextBody };
        email.Body = bodyBuilder.ToMessageBody();
        using var smtpClient = new SmtpClient();
        smtpClient.Connect(_emailSettings.Server, _emailSettings.Port, false);
        smtpClient.Authenticate(_emailSettings.UserName, _emailSettings.Password);
        smtpClient.Send(email);
        smtpClient.Disconnect(true);
        email.Dispose();


    }
    public override async Task SendEmailAsync(Core.Mailing.Mail mail)
    {
        var email = new MimeMessage();
        email.From.Add(new MailboxAddress(_emailSettings.SenderFullName, _emailSettings.SenderEmail));
        email.To.AddRange(mail.ToList);
        email.Subject = mail.Subject;
        BodyBuilder bodyBuilder = new() { HtmlBody = mail.HtmlBody, TextBody = mail.TextBody };
        email.Body = bodyBuilder.ToMessageBody();
        using var smtpClient = new SmtpClient();
        smtpClient.Connect(_emailSettings.Server, _emailSettings.Port, false);
        smtpClient.Authenticate(_emailSettings.UserName, _emailSettings.Password);
        await smtpClient.SendAsync(email);
        smtpClient.Disconnect(true);
        email.Dispose();
    }
}
