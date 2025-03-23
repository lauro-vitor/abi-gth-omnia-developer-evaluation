using System.Net;
using System.Text.Json;

namespace Ambev.DeveloperEvaluation.WebApi.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var errorResponse = exception switch
            {
                KeyNotFoundException => new ErrorResponse("ResourceNotFound", "Resource not found", exception.Message, HttpStatusCode.NotFound),
                UnauthorizedAccessException => new ErrorResponse("AuthenticationError", "Unauthorized", exception.Message, HttpStatusCode.Unauthorized),
                ArgumentException => new ErrorResponse("ValidationError", "Invalid input data", exception.Message, HttpStatusCode.BadRequest),
                _ => new ErrorResponse("ServerError", "An unexpected error occurred", exception.Message, HttpStatusCode.InternalServerError)
            };

            var response = JsonSerializer.Serialize(errorResponse);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)errorResponse.StatusCode;

            return context.Response.WriteAsync(response);
        }
    }

    public class ErrorResponse
    {
        public string Type { get; }
        public string Error { get; }
        public string Detail { get; }
        public HttpStatusCode StatusCode { get; }

        public ErrorResponse(string type, string error, string detail, HttpStatusCode statusCode)
        {
            Type = type;
            Error = error;
            Detail = detail;
            StatusCode = statusCode;
        }
    }

}
