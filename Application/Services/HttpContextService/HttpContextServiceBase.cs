
using Microsoft.AspNetCore.Http;

namespace Application.Services.HttpContextService;

public class HttpContextServiceBase : IHttpContextAccessor
{
    public HttpContext? HttpContext { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
}
