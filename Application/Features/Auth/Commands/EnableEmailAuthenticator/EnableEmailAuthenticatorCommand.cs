using Application.Features.Auth.Rules;
using Application.Services.AuthenticatorService;
using Application.Services.MailService;
using Application.Services.Repositories;
using Application.Services.UsersService;
using Core.Mailing;
using Core.Security.Entities;
using Core.Security.Enums;
using MailKit;
using MediatR;
using MimeKit;
using System.Web;

namespace Application.Features.Auth.Commands.EnableEmailAuthenticator;

public class EnableEmailAuthenticatorCommand : IRequest<EnableEmailAuthenticatorResponse>
{
    public int UserId { get; set; }

    public class EnableEmailAuthenticatorCommandHandler : IRequestHandler<EnableEmailAuthenticatorCommand,EnableEmailAuthenticatorResponse>
    {
        private readonly AuthBusinessRules _authBusinessRules;
        private readonly IAuthenticatorService _authenticatorService;
        private readonly IEmailAuthenticatorRepository _emailAuthenticatorRepository;
        private readonly Core.Mailing.IMailService _mailService;
        private readonly IUserService _userService;
        private readonly MailServiceBase mailServiceBase;

        public EnableEmailAuthenticatorCommandHandler(
            IUserService userService,
            IEmailAuthenticatorRepository emailAuthenticatorRepository,
            Core.Mailing.IMailService mailService,
            AuthBusinessRules authBusinessRules,
            IAuthenticatorService authenticatorService
,
            MailServiceBase mailServiceBase)
        {
            _userService = userService;
            _emailAuthenticatorRepository = emailAuthenticatorRepository;
            _mailService = mailService;
            _authBusinessRules = authBusinessRules;
            _authenticatorService = authenticatorService;
            this.mailServiceBase = mailServiceBase;
        }

        public async Task<EnableEmailAuthenticatorResponse> Handle(EnableEmailAuthenticatorCommand request, CancellationToken cancellationToken)
        {
            User? user = await _userService.GetAsync(predicate: u => u.Id == request.UserId, cancellationToken: cancellationToken);
            await _authBusinessRules.UserShouldBeExistsWhenSelected(user);
            await _authBusinessRules.UserShouldNotBeHaveAuthenticator(user!);

            user!.AuthenticatorType = AuthenticatorType.Email;
            await _userService.UpdateAsync(user);

            var toEmailList = new List<MailboxAddress> { new(name: $"{user.FirstName} {user.LastName}", user.Email) };

            await mailServiceBase.SendEmailAsync(
    new Mail
    {
        ToList = toEmailList,
        Subject = "Metflix - Doğrulama Kodu",
        HtmlBody = $@"
            <!DOCTYPE html>
            <html lang='en'>
            <head>
                <meta charset='UTF-8'>
                <meta name='viewport' content='width=device-width, initial-scale=1.0'>
                <title>Doğrulama Kodu</title>
                <style>
                    body {{
                        font-family: Arial, sans-serif;
                        background-color: #f4f4f4;
                        color: #333;
                        text-align: center;
                    }}
                    .container {{
                        max-width: 600px;
                        width:100%;
                        margin: 0 auto;
                        padding: 20px;
                        background-color: #fff;
                        border-radius: 10px;
                        box-shadow: 0px 0px 10px rgba(0, 0, 0, 0.1);
                    }}
                    .code {{
                        font-size: 36px;
                        color: #007bff;
                        margin-top: 20px;
                    }}
                    .logo {{
                        background-color: black;
                        color: red;
                        text-transform: uppercase;
                        padding: 10px;
                        border-radius: 5px;
                        font-size: 24px;
                        margin-bottom: 20px;
                    }}
                    .image{{
;
                    }}
                </style>
            </head>
            <body>
                <div class='container'>
                    <h1 class='logo' style='width:75%;align-items:center;justify-content:center;margin-left:12.5%'><b style='font-size:36px;margin-top:5px;'>M</b>ETFLIX</h1>
                    <p>Merhaba,</p>
                    <p>Doğrulama kodunuz:</p>
                    <div class='code'>{"x"}</div>
                    <p>Lütfen bu kodu kullanarak işlemi tamamlayınız.</p>
                </div>
            </body>
            </html>"
    }
);

            EnableEmailAuthenticatorResponse enableEmailAuthenticatorResponse = new();
            return enableEmailAuthenticatorResponse;
        }
    }
}
