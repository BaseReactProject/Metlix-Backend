using Application.Features.Auth.Rules;
using Application.Services.Accounts;
using Application.Services.Repositories;
using AutoMapper;
using Core.Security.Entities;
using Core.Security.Hashing;
using Domain.Entities;
using MediatR;

namespace Application.Features.Auth.Commands.UpdatePassword;

public class UpdatePasswordCommand:IRequest
{
    public string FakeId { get; set; }
    public string Password { get; set; }

    public UpdatePasswordCommand()
    {
        FakeId = string.Empty;
        Password = string.Empty;
    }

    public UpdatePasswordCommand(string fakeId, string password)
    {
        FakeId = fakeId;
        Password = password;
    }


    public class UpdatePasswordCommandHandler : IRequestHandler<UpdatePasswordCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly AuthBusinessRules _authBusinessRules;
        private readonly IAccountsService _accountsService;

        public UpdatePasswordCommandHandler(IUserRepository userRepository, AuthBusinessRules authBusinessRules, IAccountsService accountsService)
        {
            _userRepository = userRepository;
            _authBusinessRules = authBusinessRules;
            _accountsService = accountsService;
        }

        public async Task Handle(UpdatePasswordCommand request, CancellationToken cancellationToken)
        {
            Account? account = await _accountsService.GetAsync(a => a.FakeId == request.FakeId, cancellationToken: cancellationToken);
            User? user = await _userRepository.GetAsync(predicate: u => u.Id == account.UserId, cancellationToken: cancellationToken);
            await _authBusinessRules.UserShouldBeExistsWhenSelected(user);

            HashingHelper.CreatePasswordHash(
                request.Password,
                passwordHash: out byte[] passwordHash,
                passwordSalt: out byte[] passwordSalt
            );
            user!.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            await _userRepository.UpdateAsync(user);

        }
}
}

