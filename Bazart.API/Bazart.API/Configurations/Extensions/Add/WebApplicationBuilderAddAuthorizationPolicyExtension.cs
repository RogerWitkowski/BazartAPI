namespace Bazart.API.Configurations.Extensions.Add
{
    public static class WebApplicationBuilderAddAuthorizationPolicyExtension
    {
        public static WebApplicationBuilder AddAuthorizationPolicy(this WebApplicationBuilder builder)
        {
            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("NormalUser", config => config.RequireClaim("User"));
            });
            return builder;
        }
    }
}