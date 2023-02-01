namespace Bazart.API.Configurations.Extensions.Add
{
    public static class ServiceCollectionAddControllersExtension
    {
        public static IServiceCollection AddControllersCollection(this IServiceCollection service)
        {
            var assembly = typeof(Bazart.Controllers.Controllers.AccountController).Assembly;
            service.AddMvc().AddApplicationPart(assembly);
            return service;
        }
    }
}