using System.Text.Json.Serialization;

namespace Bazart.API.Configurations.Extensions.Add
{
    internal static class WebApplicationBuilderAddControllersWithJsonOptionsExtension
    {
        internal static WebApplicationBuilder AddControllersWithJsonOptions(this WebApplicationBuilder builder)
        {
            builder.Services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                options.JsonSerializerOptions.WriteIndented = true;
            }).AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore).AddXmlDataContractSerializerFormatters();

            return builder;
        }
    }
}