using CRMSystem.Application.Exceptions;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace CRMSystem.API.Exceptions
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            var response = context.Response;

            switch (exception)
            {
                case NotFoundException notFoundException:
                    response.StatusCode = StatusCodes.Status404NotFound;
                    break;
                case DuplicateEmailException duplicateEmailException:
                    response.StatusCode = StatusCodes.Status400BadRequest;
                    break;
                case AuthenticationException authenticationException:
                    response.StatusCode = StatusCodes.Status400BadRequest;
                    break;
                default:
                    response.StatusCode = StatusCodes.Status500InternalServerError;
                    break;
            }

            var result = JsonSerializer.Serialize(new
            {
                statusCode = response.StatusCode,
                message = exception.Message ?? "An unexpected error occurred."
            });

            return context.Response.WriteAsync(result);
        }
    }
}
