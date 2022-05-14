using System.Security.Claims;
using ToDo.Application.Interfaces.Services;

namespace ToDo.Api.Services;

public class HttpContextService : IHttpContextService
{
    public HttpContextService(IHttpContextAccessor httpContextAccessor)
    {
        UserId = httpContextAccessor.HttpContext?.User?.FindFirstValue("uid");
    }

    public string UserId { get; }
}