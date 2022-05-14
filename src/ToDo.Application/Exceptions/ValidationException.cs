

using FluentValidation.Results;
using ToDo.Application.Errors;

namespace ToDo.Application.Exceptions
{
    public class ValidationException : ApiException
    {
        public IDictionary<string, string[]> Errors { get; }

        public ValidationException() : base("One or more validation failures have occurred.")
        {
            Errors = new Dictionary<string, string[]>();
            ExceptionCode = StatusCode.InvalidValidation.ToString();
        }

        public ValidationException(IEnumerable<ValidationFailure> failures) : this()
        {
            Errors = failures
                .GroupBy(e => e.PropertyName, e => e.ErrorMessage)
                .ToDictionary(
                    failureGroup => failureGroup.Key,
                    failureGroup => failureGroup.ToArray());
        }
    }
}
