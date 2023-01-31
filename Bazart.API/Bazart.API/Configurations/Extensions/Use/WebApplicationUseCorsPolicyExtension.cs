namespace Bazart.API.Configurations.Extensions.Use
{
    public static class WebApplicationUseCorsPolicyExtension
    {
        public static WebApplication UseCorsPolicy(this WebApplication app)
        {
            app.UseCors(policyName: "FrontendClient");

            return app;
        }
    }
}