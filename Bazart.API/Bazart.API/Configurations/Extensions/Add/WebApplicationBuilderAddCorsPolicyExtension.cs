using Bazart.ErrorHandlingMiddleware.Exceptions;
using static System.Net.WebRequestMethods;

namespace Bazart.API.Configurations.Extensions.Add
{
    public static class WebApplicationBuilderAddCorsPolicyExtension
    {
        public static WebApplicationBuilder AddCorsPolicy(this WebApplicationBuilder builder)
        {
            var allowedOrigin = builder.Configuration.GetSection("AllowedOrigins:ReactFrontOrigin").Value;
            if (string.IsNullOrEmpty(allowedOrigin))
            {
                throw new NotFoundException("Origin not found.");
            }
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("FrontendClient", policy => policy

                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        //policy.WithOrigins(origins: allowedOrigin);
                        //policy.AllowCredentials();
                        .AllowAnyOrigin()

                );
            });
            return builder;
        }
    }
}