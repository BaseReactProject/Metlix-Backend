using System.Security.Cryptography;

namespace Core.Security.EmailAuthenticator;

public class EmailAuthenticatorHelper : IEmailAuthenticatorHelper
{
    public Task<string> CreateEmailActivationKey()
    {
        string key = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
        return Task.FromResult(key);
    }

    public Task<string> CreateEmailActivationCode()
    {
        string code = RandomNumberGenerator
            .GetInt32(Convert.ToInt32(Math.Pow(x: 10, y: 8)))
            .ToString()
            .PadLeft(totalWidth: 8, paddingChar: '0');
        return Task.FromResult(code);
    }
    public Task<string> CreateEmailMiniActivationCode()
    {
        string code = RandomNumberGenerator
             .GetInt32(Convert.ToInt32(Math.Pow(x: 10, y: 8)))
             .ToString()
             .PadLeft(totalWidth: 4, paddingChar: '0');
        return Task.FromResult(code);
    }
}
