namespace Bazart.API.Configurations.Extensions.Use
{
    public static class WebApplicationUseCorsPolicyExtension
    {
        public static WebApplication UseCorsPolicy(this WebApplication app)
        {
            app.UseCors();

            return app;
        }
    }
}