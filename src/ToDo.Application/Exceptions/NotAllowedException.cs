using System;
using ToDo.Application.Errors;

namespace ToDo.Application.Exceptions
{
    public class NotAllowedException : ApiException
    {
        public NotAllowedException() : base($"Not allowed to do this action")
        {
            ExceptionCode = StatusCode.NotAllowed.ToString();
        }
    }
}
