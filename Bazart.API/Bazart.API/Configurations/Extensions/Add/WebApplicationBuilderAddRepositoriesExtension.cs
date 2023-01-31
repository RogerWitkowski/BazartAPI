using Bazart.API.Repository.Bazart.API.Repository;
using Bazart.API.Repository.IRepository;
using Bazart.API.Repository;

namespace Bazart.API.Configurations.Extensions.Add
{
    public static class WebApplicationBuilderAddRepositoriesExtension
    {
        public static WebApplicationBuilder AddRepositories(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IProductRepository, ProductRepository>();
            builder.Services.AddScoped<IItemsRepository, ItemsRepository>();
            builder.Services.AddScoped<IEventRepository, EventRepository>();
            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
            builder.Services.AddScoped<IAccountRepository, AccountRepository>();

            return builder;
        }
    }
}