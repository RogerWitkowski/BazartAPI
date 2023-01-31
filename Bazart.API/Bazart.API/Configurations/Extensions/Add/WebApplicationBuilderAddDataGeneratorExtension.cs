using Bazart.DataAccess.Seeder;

namespace Bazart.API.Configurations.Extensions.Add
{
    public static class WebApplicationBuilderAddDataGeneratorExtension
    {
        public static WebApplicationBuilder AddDataGenerator(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<DataGenerator>();
            return builder;
        }
    }
}