using Bazart.API.Configurations.Middleware;

namespace Bazart.API.Configurations.Extensions.Add
{
    public static class WebApplicationBuilderAddGlobalErrorHandlerExtension
    {
        public static WebApplicationBuilder AddGlobalErrorHandlers(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<ErrorHandlingMiddleware>();
            builder.Services.AddScoped<RequestTimeMiddleware>();

            return builder;
        }
    }
}