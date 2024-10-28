using HelloWorldWebAPI.Errors;
using System.Net;
using System.Text.Json;

namespace HelloWorldWebAPI.Middleware
{
    public class ExceptionMiddleware(IHostEnvironment _env, RequestDelegate _next)
    {
        public async Task InvokeAsync(HttpContext context)
        {
			try
			{
				await _next(context);
			}
			catch (Exception ex)
			{
				await HandleExceptionAsync(context, ex, _env);
			}
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception ex, IHostEnvironment _env)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var response = _env.IsDevelopment()
                ? new ApiErrorResponse(context.Response.StatusCode, ex.Message, ex.StackTrace)
                : new ApiErrorResponse(context.Response.StatusCode, ex.Message, "Internal Server Error");

            var options = new JsonSerializerOptions 
            { 
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            };

            var json =  JsonSerializer.Serialize(response, options);

            return context.Response.WriteAsync(json);

        }
    }
}
