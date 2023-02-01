using Bazart.Repository.Repository;
using Bazart.Repository.Repository.IRepository;

namespace Bazart.API.Configurations.Extensions.Add
{
    public static class ServiceCollectionAddRepositoriesExtension
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IItemsRepository, ItemsRepository>();
            services.AddScoped<IEventRepository, EventRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IAccountRepository, AccountRepository>();

            return services;
        }
    }
}