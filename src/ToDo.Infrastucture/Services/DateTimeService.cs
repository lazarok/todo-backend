using ToDo.Application.Interfaces.Services;

namespace ToDo.Infrastucture.Services;

public class DateTimeService : IDateTimeService
{
    public DateTime Now => DateTime.Now;
    public DateTime UtcNow => DateTime.UtcNow;
}