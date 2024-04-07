using Application.Features.Auth.Rules;
using Application.Services.Accounts;
using Application.Services.AuthenticatorService;
using Application.Services.AuthService;
using Application.Services.MailService;
using Application.Services.Repositories;
using Core.Application.Dtos;
using Core.Mailing;
using Core.Security.Entities;
using Core.Security.Enums;
using Core.Security.Hashing;
using Core.Security.JWT;
using Domain.Entities;
using MediatR;
using MimeKit;
using Org.BouncyCastle.Bcpg;

namespace Application.Features.Auth.Commands.Register;

public class RegisterCommand : IRequest<RegisteredResponse>
{
    public UserForRegisterDto UserForRegisterDto { get; set; }
    public string IpAddress { get; set; }

    public RegisterCommand()
    {
        UserForRegisterDto = null!;
        IpAddress = string.Empty;
    }

    public RegisterCommand(UserForRegisterDto userForRegisterDto, string ipAddress)
    {
        UserForRegisterDto = userForRegisterDto;
        IpAddress = ipAddress;
    }

    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, RegisteredResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthService _authService;
        private readonly IAuthenticatorService _authenticatorService;
        private readonly AuthBusinessRules _authBusinessRules;
        private readonly MailServiceBase mailServiceBase;
        private readonly IEmailAuthenticatorRepository _emailAuthenticatorRepository;
        private readonly IAccountsService _accountService;

        public RegisterCommandHandler(IUserRepository userRepository, IAuthService authService, AuthBusinessRules authBusinessRules, MailServiceBase mailServiceBase, IAuthenticatorService authenticatorService, IEmailAuthenticatorRepository emailAuthenticatorRepository, IAccountsService accountService)
        {
            _userRepository = userRepository;
            _authService = authService;
            _authBusinessRules = authBusinessRules;
            this.mailServiceBase = mailServiceBase;
            _authenticatorService = authenticatorService;
            _emailAuthenticatorRepository = emailAuthenticatorRepository;
            _accountService = accountService;
        }

        public async Task<RegisteredResponse> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            await _authBusinessRules.UserEmailShouldBeNotExists(request.UserForRegisterDto.Email);

            HashingHelper.CreatePasswordHash(
                request.UserForRegisterDto.Password,
                passwordHash: out byte[] passwordHash,
                passwordSalt: out byte[] passwordSalt
            );
            User newUser =
                new()
                {
                    Email = request.UserForRegisterDto.Email,
                    FirstName = request.UserForRegisterDto.FirstName,
                    LastName = request.UserForRegisterDto.LastName,
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt,
                    Status = true
                };
            User createdUser = await _userRepository.AddAsync(newUser);


            Account account = new()
            {
                FakeId =await _accountService.CreateFakeIdCodeGenerator(),
                UserId = createdUser.Id
            };
            await _accountService.AddAsync(account);




            EmailAuthenticator emailAuthenticator = await _authenticatorService.CreateEmailAuthenticator(createdUser);
            EmailAuthenticator addedEmailAuthenticator = await _emailAuthenticatorRepository.AddAsync(emailAuthenticator);
            var toEmailList = new List<MailboxAddress> { new(name: $"{createdUser.FirstName} {createdUser.LastName}", createdUser.Email) };
            await mailServiceBase.SendEmailAsync(new Mail
            {
                ToList = toEmailList,
                Subject = "Email Doğrulama - METFLİX",
                HtmlBody =
                        $"Your Verification Key = {addedEmailAuthenticator.ActivationKey} "
            });
            RegisteredResponse registeredResponse = new() {};
            return registeredResponse;
        }
    }
}
