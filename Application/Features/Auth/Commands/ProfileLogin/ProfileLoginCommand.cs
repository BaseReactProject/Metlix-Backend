
using Application.Features.Auth.Commands.Login;
using Application.Features.Auth.Rules;
using Application.Features.Constants;
using Application.Services.AuthenticatorService;
using Application.Services.AuthService;
using Application.Services.UsersService;
using Core.Application.Pipelines.Authorization;
using Core.Security.Entities;
using Core.Security.Extensions;
using Core.Security.JWT;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Features.Auth.Commands.ProfileLogin;

public class ProfileLoginCommand : IRequest<LoggedResponse>,ISecuredRequest
{
    public int ProfileId { get; set; }
    public string Password { get; set; }

    public string[] Roles => new []{MetflixOperationClaims.Account} ;

    public ProfileLoginCommand()
    {
        ProfileId = 0;
        Password = string.Empty;
    }

    public ProfileLoginCommand(int profileId, string password)
    {
        ProfileId = profileId;
        Password = password;
    }

    public class ProfileLoginCommandHandler : IRequestHandler<ProfileLoginCommand, LoggedResponse>
    {
        private readonly AuthBusinessRules _authBusinessRules;
        private readonly IAuthenticatorService _authenticatorService;
        private readonly IAuthService _authService;
        private readonly IUserService _userService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ProfileLoginCommandHandler(
            IUserService userService,
            IAuthService authService,
            AuthBusinessRules authBusinessRules,
            IAuthenticatorService authenticatorService,
            IHttpContextAccessor httpContextAccessor)
        {
            _userService = userService;
            _authService = authService;
            _authBusinessRules = authBusinessRules;
            _authenticatorService = authenticatorService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<LoggedResponse> Handle(ProfileLoginCommand request, CancellationToken cancellationToken)
        {
            User? user = await _userService.GetAsync(
                predicate: u => u.Id ==_httpContextAccessor.HttpContext.User.GetUserId(),
                cancellationToken: cancellationToken
            );

            await _authBusinessRules.ProfilePasswordShouldBeMatch(request.ProfileId,request.Password);
            LoggedResponse loggedResponse = new();


            AccessToken createdAccessToken = await _authService.CreateAccessTokenForProfile(request.ProfileId,user!);

            loggedResponse.AccessToken = createdAccessToken;
            return loggedResponse;
        }
    }
}

