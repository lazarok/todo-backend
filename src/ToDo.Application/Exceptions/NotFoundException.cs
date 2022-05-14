using System;
using ToDo.Application.Errors;

namespace ToDo.Application.Exceptions
{
    public class NotFoundException : ApiException
    {
        public NotFoundException(string entity, string id) : base($"Not found {entity} with id '{id}'")
        {
            ExceptionCode = StatusCode.NotFound.ToString();
        }
    }
}
