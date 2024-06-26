﻿using Application.Features.Accounts.Queries.GetById;
using Application.Features.Accounts.Queries.GetList;
using Application.Features.Auth.Commands.EnableEmailAuthenticator;
using Application.Features.Auth.Commands.EnableOtpAuthenticator;
using Application.Features.Auth.Commands.ForgetPassword;
using Application.Features.Auth.Commands.Login;
using Application.Features.Auth.Commands.ProfileLogin;
using Application.Features.Auth.Commands.RefreshToken;
using Application.Features.Auth.Commands.Register;
using Application.Features.Auth.Commands.RevokeToken;
using Application.Features.Auth.Commands.UpdatePassword;
using Application.Features.Auth.Commands.VerifyEmailAuthenticator;
using Application.Features.Auth.Commands.VerifyOtpAuthenticator;
using Application.Features.Auth.Queries.GetProfiles;
using Core.Application.Dtos;
using Core.Application.Responses;
using Core.Security.Entities;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : BaseController
{
    private readonly WebApiConfiguration _configuration;

    public AuthController(IConfiguration configuration)
    {
        const string configurationSection = "WebAPIConfiguration";
        _configuration =
            configuration.GetSection(configurationSection).Get<WebApiConfiguration>()
            ?? throw new NullReferenceException($"\"{configurationSection}\" section cannot found in configuration.");
    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login([FromBody] UserForLoginDto userForLoginDto)
    {
        LoginCommand loginCommand = new() { UserForLoginDto = userForLoginDto, IpAddress = getIpAddress() };
        LoggedResponse result = await Mediator.Send(loginCommand);

        if (result.RefreshToken is not null)
            setRefreshTokenToCookie(result.RefreshToken);

        return Ok(result.ToHttpResponse());
    }
    
    [HttpGet("GetProfiles")]
    public async Task<IActionResult> GetProfiles()
    {
        GetListResponse<GetListAccountListItemDto> getListResponse = await Mediator.Send(new GetListProfileForActiveUserQuery());
        return Ok(getListResponse);
    }
    [HttpPost("ProfileLogin")]
    public async Task<IActionResult> ProfileLogin([FromBody] ProfileLoginCommand profileLoginCommand)
    {
        ProfileLoginCommand loginCommand = new() { Password = profileLoginCommand.Password, ProfileId = profileLoginCommand.ProfileId };
        LoggedResponse result = await Mediator.Send(loginCommand);

        return Ok(result.ToHttpResponse());
    }
    [HttpPost("Register")]
    public async Task<IActionResult> Register([FromForm] UserForRegisterDto userForRegisterDto)
    {
        RegisterCommand registerCommand = new() { UserForRegisterDto = userForRegisterDto, IpAddress = getIpAddress() };
        RegisteredResponse result = await Mediator.Send(registerCommand);
        return Ok();
    }

    [HttpGet("RefreshToken")]
    public async Task<IActionResult> RefreshToken()
    {
        RefreshTokenCommand refreshTokenCommand = new() { RefreshToken = getRefreshTokenFromCookies(), IpAddress = getIpAddress() };
        RefreshedTokensResponse result = await Mediator.Send(refreshTokenCommand);
        setRefreshTokenToCookie(result.RefreshToken);
        return Created(uri: "", result.AccessToken);
    }

    [HttpPut("RevokeToken")]
    public async Task<IActionResult> RevokeToken([FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Allow)] string? refreshToken)
    {
        RevokeTokenCommand revokeTokenCommand = new() { Token = refreshToken ?? getRefreshTokenFromCookies(), IpAddress = getIpAddress() };
        RevokedTokenResponse result = await Mediator.Send(revokeTokenCommand);
        return Ok(result);
    }

    [HttpPost("EnableEmailAuthenticator")]
    public async Task<IActionResult> EnableEmailAuthenticator(int userId)
    {
        EnableEmailAuthenticatorCommand enableEmailAuthenticatorCommand = new() { UserId = userId };
        var result = await Mediator.Send(enableEmailAuthenticatorCommand  );

        return Ok(result);
    }

    [HttpGet("EnableOtpAuthenticator")]
    public async Task<IActionResult> EnableOtpAuthenticator()
    {
        EnableOtpAuthenticatorCommand enableOtpAuthenticatorCommand = new() { UserId = getUserIdFromRequest() };
        EnabledOtpAuthenticatorResponse result = await Mediator.Send(enableOtpAuthenticatorCommand);

        return Ok(result);
    }

    [HttpGet("VerifyEmailAuthenticator")]
    public async Task<IActionResult> VerifyEmailAuthenticator([FromQuery] VerifyEmailAuthenticatorCommand verifyEmailAuthenticatorCommand)
    {
        await Mediator.Send(verifyEmailAuthenticatorCommand);
        return Ok();
    }

    [HttpPost("VerifyOtpAuthenticator")]
    public async Task<IActionResult> VerifyOtpAuthenticator([FromBody] string authenticatorCode)
    {
        VerifyOtpAuthenticatorCommand verifyEmailAuthenticatorCommand =
            new() { UserId = getUserIdFromRequest(), ActivationCode = authenticatorCode };

        await Mediator.Send(verifyEmailAuthenticatorCommand);
        return Ok();
    }
    [HttpGet("ForgetPassword/{email}")]
    public async Task<IActionResult> GetById([FromRoute] string email)
    {
        ForgetPasswordResponse response = await Mediator.Send(new ForgetPasswordCommand { Email = email });
        return Ok(response);
    }
    [HttpPost("UpdatePassword")]
    public async Task <IActionResult> UpdatePassword([FromBody] UpdatePasswordCommand updatePasswordCommand)
    {
        await Mediator.Send(updatePasswordCommand);
        return Ok();
    }
    private string getRefreshTokenFromCookies() =>
        Request.Cookies["refreshToken"] ?? throw new ArgumentException("Refresh token is not found in request cookies.");

    private void setRefreshTokenToCookie(RefreshToken refreshToken)
    {
        CookieOptions cookieOptions = new() { HttpOnly = true, Expires = DateTime.UtcNow.AddDays(7) };
        Response.Cookies.Append(key: "refreshToken", refreshToken.Token, cookieOptions);
    }
}
