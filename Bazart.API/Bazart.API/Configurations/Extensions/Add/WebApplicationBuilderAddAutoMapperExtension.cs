using System.Reflection;

namespace Bazart.API.Configurations.Extensions.Add
{
    public static class WebApplicationBuilderAddAutoMapperExtension
    {
        public static WebApplicationBuilder AddAutoMapper(this WebApplicationBuilder builder)
        {
            builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
            return builder;
        }
    }
}