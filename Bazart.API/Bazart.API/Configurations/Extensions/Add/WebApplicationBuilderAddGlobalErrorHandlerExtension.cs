using Bazart.ErrorHandlingMiddleware.Middleware;

namespace Bazart.API.Configurations.Extensions.Add
{
    public static class WebApplicationBuilderAddGlobalErrorHandlerExtension
    {
        public static WebApplicationBuilder AddGlobalErrorHandlers(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<ErrorHandlingMiddleware.Middleware.ErrorHandlingMiddleware>();
            builder.Services.AddScoped<RequestTimeMiddleware>();

            return builder;
        }
    }
}