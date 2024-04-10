

using Core.Security.Entities;
using Core.Security.JWT;

namespace Application.Services.JWT;

public interface IJwtHelper
{
    AccessToken CreateToken(int accountId,int profileId,User user, IList<OperationClaim> operationClaims);

    RefreshToken CreateRefreshToken(User user, string ipAddress);
}
