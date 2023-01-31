using Bazart.EmailService.EmailService.IEmailService;

namespace Bazart.API.Configurations.Extensions.Add
{
    internal static class WebApplicationBuilderAddEmailServiceExtension
    {
        internal static WebApplicationBuilder AddEmailService(this WebApplicationBuilder builder)
        {
            builder.Services.AddSingleton<IEmailService, EmailService.EmailService.EmailService>();
            return builder;
        }
    }
}