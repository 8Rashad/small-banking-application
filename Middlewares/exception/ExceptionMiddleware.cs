using System.Net;
using System.Text.Json;
using TaskApi.Response;

namespace BankApplication.Middlewares.exception
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
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
                _logger.LogError(ex, ex.Message);
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            _logger.LogError(ex, "An unhandled exception occured");

            var response = new ResponseModel<string>
            {
                IsSuccess = false,
                StatusCode = (int)HttpStatusCode.InternalServerError,
                Errors = new List<string> { "An unexpected error occurred. Please try again later." }
            };

            var responseBody = JsonSerializer.Serialize(response);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = response.StatusCode;

            await context.Response.WriteAsync(responseBody);
        }
    }
}
