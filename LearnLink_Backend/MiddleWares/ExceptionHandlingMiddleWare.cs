using LearnLink_Backend.Exceptions;

namespace LearnLink_Backend.MiddleWares
{
    public class ExceptionHandlingMiddleWare(RequestDelegate next, ILogger<ExceptionHandlingMiddleWare> logger)
    {
        private readonly RequestDelegate _next = next;
        private readonly ILogger<ExceptionHandlingMiddleWare> _logger = logger;

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (BadHttpRequestException ex)
            {
                _logger.LogError(ex, "A bad request exception occurred.");
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsJsonAsync(new { Error = ex.Message });
            }
            catch (NotFoundException ex)
            {
                _logger.LogError(ex, "A bad request exception occurred.");
                context.Response.StatusCode = StatusCodes.Status404NotFound;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsJsonAsync(new { Error = ex.Message });
            }
            catch (ConfilctException ex)
            {
                _logger.LogError(ex, "A bad request exception occurred.");
                context.Response.StatusCode = StatusCodes.Status409Conflict;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsJsonAsync(new { Error = ex.Message });
            }
            // generic exception handling 500s
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception occurred.");
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsJsonAsync(new { Error = "An error occurred. Please try again later." });
            }
        }
    }
}
