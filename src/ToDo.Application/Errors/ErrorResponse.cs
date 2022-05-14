namespace ToDo.Application.Errors;

public class ErrorResponse
{
    /// <summary>
    /// StatusCode
    /// </summary>
    public string Status { get; set; }
    public string Message { get; set; }
    
    public IDictionary<string, string[]>? Errors { get; set; }
}