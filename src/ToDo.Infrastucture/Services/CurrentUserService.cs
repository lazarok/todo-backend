using ToDo.Application.Interfaces.Services;

namespace ToDo.Infrastucture.Services;

public class CurrentUserService : ICurrentUserService
{
    public Guid? UserId { get; } = Guid.NewGuid();
}