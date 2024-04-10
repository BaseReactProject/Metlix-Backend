using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Application.Services.Extensions;

public static class ClaimExtensions
{
    public static void AddEmail(this ICollection<Claim> claims, string email) =>
        claims.Add(new Claim(JwtRegisteredClaimNames.Email, email));

    public static void AddName(this ICollection<Claim> claims, string name) => claims.Add(new Claim(ClaimTypes.Name, name));

    public static void AddNameIdentifier(this ICollection<Claim> claims, string nameIdentifier) =>
        claims.Add(new Claim(ClaimTypes.NameIdentifier, nameIdentifier));
    public static void AddAccountIdentifier(this ICollection<Claim> claims, string accountIdentifier) =>
        claims.Add(new Claim(ClaimTypes.Anonymous, accountIdentifier));
    public static void AddProfileIdentifier(this ICollection<Claim> claims, string profileIdentifier) =>
        claims.Add(new Claim(ClaimTypes.SerialNumber, profileIdentifier));

    public static void AddRoles(this ICollection<Claim> claims, string[] roles) =>
        roles.ToList().ForEach(role => claims.Add(new Claim(ClaimTypes.Role, role)));
}
