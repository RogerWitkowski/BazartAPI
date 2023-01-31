using Bazart.API.Configurations.Middleware;

namespace Bazart.API.Configurations.Extensions.Use
{
    public static class WebApplicationUseGlobalErrorHandlerMiddlewareExtension
    {
        internal static WebApplication UseGlobalErrorHandlersMiddleware(this WebApplication app)
        {
            app.UseMiddleware<ErrorHandlingMiddleware>();
            app.UseMiddleware<RequestTimeMiddleware>();
            return app;
        }
    }
}