using Bazart.EmailService.SettingModel;

namespace Bazart.API.Configurations.Extensions.Add
{
    internal static class WebApplicationBuilderAddEmailSendingSettingsModelExtension
    {
        internal static WebApplicationBuilder AddEmailSendingSettingsModel(this WebApplicationBuilder builder)
        {
            builder.Services.Configure<EmailSendingSettingsModel>(builder.Configuration.GetSection("MailGun"));
            return builder;
        }
    }
}