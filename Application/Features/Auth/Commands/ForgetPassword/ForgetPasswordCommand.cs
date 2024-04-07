using Application.Features.Auth.Rules;
using Application.Services.Accounts;
using Application.Services.MailService;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using Core.Mailing;
using Core.Security.Entities;
using Domain.Entities;
using MediatR;
using MimeKit;


namespace Application.Features.Auth.Commands.ForgetPassword;

public class ForgetPasswordCommand : IRequest<ForgetPasswordResponse>, ILoggableRequest, ITransactionalRequest
{
    public ForgetPasswordCommand()
    {
        Email = string.Empty;
    }
    public ForgetPasswordCommand(string email)
    {
        Email = email;
    }

    public string Email { get; set; }

    public class CreateForgetPasswordCommandHandler : IRequestHandler<ForgetPasswordCommand, ForgetPasswordResponse>
    {
        private readonly IMapper _mapper;
        private readonly MailServiceBase mailServiceBase;
        private readonly IUserRepository _userRepository;
        private readonly IAccountsService accountsService;
        private readonly AuthBusinessRules authBusinessRules;

        public CreateForgetPasswordCommandHandler(IMapper mapper, IUserRepository userRepository,
                                         AuthBusinessRules authBusinessRules, MailServiceBase mailServiceBase, IAccountsService accountsService)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            this.authBusinessRules = authBusinessRules;
            this.mailServiceBase = mailServiceBase;
            this.accountsService = accountsService;
        }

        public async Task<ForgetPasswordResponse> Handle(ForgetPasswordCommand request, CancellationToken cancellationToken)
        {
            await authBusinessRules.EmailShouldBeMatch(request.Email);
            User user = await _userRepository.GetAsync(u => u.Email == request.Email);
            Account account = await accountsService.GetAsync(a => a.UserId == user.Id);

            var toEmailList = new List<MailboxAddress> { new(name: $"{user.FirstName} {user.LastName}", user.Email) };

            await mailServiceBase.SendEmailAsync(
                new Mail
                {
                    ToList = toEmailList,
                    Subject = "Metflix - Şifre Değiştirme",
                    HtmlBody = $@"
                        <!DOCTYPE html>
                        <html lang='en'>
                        <head>
                            <meta charset='UTF-8'>
                            <meta name='viewport' content='width=device-width, initial-scale=1.0'>
                            <title>Şifre Değiştirme</title>
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
                                    text-decoration:none;
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
                                <p>Şifre Değiştirme Bağlantınız:</p>
                                <a class='code' src='{"localhost:3000/"+account.FakeId}'>{"localhost:3000/" + account.FakeId}</a>
                                <p>Lütfen bu kodu kullanarak işlemi tamamlayınız.</p>
                            </div>
                        </body>
                        </html>"
                }
            );
            ForgetPasswordResponse response = new();
            return response;
        }
    }
}