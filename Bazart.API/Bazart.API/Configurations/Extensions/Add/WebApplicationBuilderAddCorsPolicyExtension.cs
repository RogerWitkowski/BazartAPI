namespace Bazart.API.Configurations.Extensions.Add
{
    public static class WebApplicationBuilderAddCorsPolicyExtension
    {
        public static WebApplicationBuilder AddCorsPolicy(this WebApplicationBuilder builder)
        {
            var allowedOrigin = builder.Configuration.GetSection("AllowedOrigins:ReactFrontOrigin").Value;
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("FrontendClient", policy => policy
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    //.WithOrigins(origins: allowedOrigin)
                    //.AllowCredentials()
                    .AllowAnyOrigin()
                );
            });
            return builder;
        }
    }
}