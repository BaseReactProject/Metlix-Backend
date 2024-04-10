
using Application.Services.Extensions;
using Core.Security.Encryption;
using Core.Security.Entities;
using Core.Security.JWT;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace Application.Services.JWT;

public class JwtHelper:IJwtHelper

{
    public IConfiguration Configuration { get; }
    private readonly TokenOptions _tokenOptions;
    private DateTime _accessTokenExpiration;
    private readonly IHttpContextAccessor httpContextAccessor;

    public JwtHelper(IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
    {
        Configuration = configuration;
        const string configurationSection = "TokenOptions";
        _tokenOptions =
            Configuration.GetSection(configurationSection).Get<TokenOptions>()
            ?? throw new NullReferenceException($"\"{configurationSection}\" section cannot found in configuration.");
        this.httpContextAccessor = httpContextAccessor;
    }

    public AccessToken CreateToken(int accountId,int profileId,User user, IList<OperationClaim> operationClaims)
    {
        _accessTokenExpiration = DateTime.Now.AddMinutes(_tokenOptions.AccessTokenExpiration);
        SecurityKey securityKey = SecurityKeyHelper.CreateSecurityKey(_tokenOptions.SecurityKey);
        SigningCredentials signingCredentials = SigningCredentialsHelper.CreateSigningCredentials(securityKey);
        JwtSecurityToken jwt = CreateJwtSecurityToken(_tokenOptions,accountId,profileId, user, signingCredentials, operationClaims);
        JwtSecurityTokenHandler jwtSecurityTokenHandler = new();
        string? token = jwtSecurityTokenHandler.WriteToken(jwt);

        return new AccessToken { Token = token, Expiration = _accessTokenExpiration };
    }

    public RefreshToken CreateRefreshToken(User user, string ipAddress)
    {
        RefreshToken refreshToken =
            new()
            {
                UserId = user.Id,
                Token = RandomRefreshToken(),
                Expires = DateTime.UtcNow.AddDays(7),
                CreatedByIp = ipAddress
            };

        return refreshToken;
    }

    public JwtSecurityToken CreateJwtSecurityToken(
        TokenOptions tokenOptions,
        int accountId,int profileId,
        User user,
        SigningCredentials signingCredentials,
        IList<OperationClaim> operationClaims
    )
    {
        JwtSecurityToken jwt =
            new(
                tokenOptions.Issuer,
                tokenOptions.Audience,
                expires: _accessTokenExpiration,
                notBefore: DateTime.Now,
                claims: SetClaims(accountId,profileId,user, operationClaims),
                signingCredentials: signingCredentials
            );
        return jwt;
    }

    private IEnumerable<Claim> SetClaims(int accountId,int profileId,User user, IList<OperationClaim> operationClaims)
    {
        List<Claim> claims = new();
        claims.AddNameIdentifier(user.Id.ToString());
        claims.AddEmail(user.Email);
        claims.AddName($"{user.FirstName} {user.LastName}");
        claims.AddRoles(operationClaims.Select(c => c.Name).ToArray());
        claims.AddAccountIdentifier(accountId.ToString());
        claims.AddProfileIdentifier(profileId.ToString());

        return claims;
    }

    private string RandomRefreshToken()
    {
        byte[] numberByte = new byte[32];
        using var random = RandomNumberGenerator.Create();
        random.GetBytes(numberByte);
        return Convert.ToBase64String(numberByte);
    }
}