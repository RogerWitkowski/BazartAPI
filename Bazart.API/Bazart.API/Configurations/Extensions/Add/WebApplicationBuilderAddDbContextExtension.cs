using Bazart.DataAccess.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Bazart.API.Configurations.Extensions.Add
{
    public static class WebApplicationBuilderAddDbContextExtension
    {
        public static WebApplicationBuilder AddBazartDbContext(this WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<BazartDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DatabaseConnection"));
            });
            return builder;
        }
    }
}