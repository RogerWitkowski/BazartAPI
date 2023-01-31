namespace Bazart.API.Configurations.Extensions.Add
{
    internal static class WebApplicationBuilderAddCorsPolicyExtension
    {
        internal static WebApplicationBuilder AddCorsPolicy(this WebApplicationBuilder builder)
        {
            var allowedOrigin = builder.Configuration.GetSection("AllowedOrigins:ReactFrontOrigin").Value;
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("FrontendClient", policy => policy
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .WithOrigins(origins: allowedOrigin)
                    .AllowCredentials());
            });
            return builder;
        }
    }
}