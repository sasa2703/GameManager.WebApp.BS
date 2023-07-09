using GameManager.WebApp.BS.Shared.Exceptions.Middleware;
using Microsoft.AspNetCore.Builder;

namespace GameManager.WebApp.BS.Shared.Middleware
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionHandlerMiddleware>(Array.Empty<object>());
            return app;
        }
    }
}
