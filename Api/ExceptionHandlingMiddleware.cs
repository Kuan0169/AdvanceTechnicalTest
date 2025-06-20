using Microsoft.Identity.Client;
using System.Diagnostics.Eventing.Reader;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Threading.Tasks;

namespace ApiMyCompany.Test.Api
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;
        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Unhandled exception:{ex.Message}");
                await HandleExceptionAsync(context, ex);
            }
        }
        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            HttpStatusCode statusCode = HttpStatusCode.InternalServerError;
            string result = string.Empty;

            if (exception is KeyNotFoundException)
            {
                statusCode = HttpStatusCode.NotFound;
                result = "Resource not found";
            }
            else
            {
                result = "An unexpected error occurred";
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            return context.Response.WriteAsync(new
            {
                error = result,
                details = exception.Message,
            }.ToJson());
        }
    }

    public static class ObjectExtensions
    {
        public static string ToJson(this object obj)
        {
            return System.Text.Json.JsonSerializer.Serialize(obj);
        }
    }
}
