using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.Mime;
using System.Text.Json;
using ToDo.Application.Errors;
using ToDo.Application.Exceptions;

namespace ToDo.Api.Middlewares;

public class ErrorHandlerMiddleware
{
    private readonly RequestDelegate _next;

    public ErrorHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception error)
        {
            var response = context.Response;
            response.ContentType = "application/json";
            var responseModel = new ErrorResponse()
            {
                Message = error?.Message, 
                Status = StatusCode.UnknownError.ToString()
            };

            if (error is ApiException ae)
            {
                responseModel.Status = ae.ExceptionCode;
            }

            switch (error)
            {
                case ToDo.Application.Exceptions.NotAllowedException e:
                    // custom application error
                    response.StatusCode = (int)HttpStatusCode.Forbidden;
                    break;
                case ToDo.Application.Exceptions.ValidationException e:
                    // custom application error
                    responseModel.Errors = e.Errors;
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    break;
                case ToDo.Application.Exceptions.NotFoundException e :
                    // not found error
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                    break;
                case KeyNotFoundException e:
                    // not found error
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                    break;
                default:
                    // unhandled error
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    break;
            }
            var result = JsonSerializer.Serialize(responseModel);

            await response.WriteAsync(result);
        }
    }
}