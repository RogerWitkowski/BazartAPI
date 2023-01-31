using Bazart.DataAccess.DataAccess;
using Bazart.Models.Model;
using Microsoft.AspNetCore.Identity;

namespace Bazart.API.Configurations.Extensions.Add
{
    internal static class WebApplicationBuilderAddUserIdentityExtension
    {
        internal static WebApplicationBuilder AddUserIdentity(this WebApplicationBuilder builder)
        {
            builder.Services.AddIdentity<User, IdentityRole>(options =>
                {
                    options.User.AllowedUserNameCharacters = default;

                    options.Password.RequiredLength = 10;
                    options.Password.RequireLowercase = true;
                    options.Password.RequireUppercase = true;
                    options.Password.RequireDigit = true;
                    options.Password.RequireNonAlphanumeric = true;
                    options.Password.RequiredUniqueChars = 2;

                    options.User.RequireUniqueEmail = true;

                    options.Lockout.MaxFailedAccessAttempts = 3;
                    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);

                    options.SignIn.RequireConfirmedEmail = true;
                }).AddEntityFrameworkStores<BazartDbContext>()
                .AddDefaultTokenProviders();

            return builder;
        }
    }
}