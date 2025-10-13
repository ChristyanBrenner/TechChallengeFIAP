using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Text.Json;

namespace Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext ctx)
        {
            try
            {
                await _next(ctx);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception occurred.");
                ctx.Response.ContentType = "application/json";
                ctx.Response.StatusCode = ex switch
                {
                    ApplicationException => (int)HttpStatusCode.BadRequest,
                    KeyNotFoundException => (int)HttpStatusCode.NotFound,
                    _ => (int)HttpStatusCode.InternalServerError
                };

                var result = JsonSerializer.Serialize(new { error = ex.Message });
                await ctx.Response.WriteAsync(result);
            }
        }
    }
}
