using ToDo.Application.Errors;

namespace ToDo.Application.Exceptions;

public class ApiException : Exception
{
    public string ExceptionCode { get; set; } = StatusCode.UnknownError.ToString();
    
    protected ApiException(string? message) : base(message)
    {
    }
}