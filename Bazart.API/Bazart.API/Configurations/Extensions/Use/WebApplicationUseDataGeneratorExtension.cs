using Bazart.DataAccess.Seeder;

namespace Bazart.API.Configurations.Extensions.Use
{
    internal static class WebApplicationUseDataGeneratorExtension
    {
        internal static async Task<WebApplication> UseDataGenerator(this WebApplication app)
        {
            await using var scope = app.Services.CreateAsyncScope();
            var dataGenerator = scope.ServiceProvider.GetService<DataGenerator>();
            await dataGenerator!.GenerateData();

            return app;
        }
    }
}