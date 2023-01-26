using Bazart.EmailService.SettingModel;
using Microsoft.Extensions.Options;
using RestSharp.Authenticators;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bazart.EmailService.EmailService
{
    public class EmailService : IEmailService.IEmailService
    {
        private readonly IOptions<EmailSendingSettingsModel> _emailSettings;

        public EmailService(IOptions<EmailSendingSettingsModel> emailSettings)
        {
            _emailSettings = emailSettings;
        }

        public async Task<bool> SendEmailAsync(string body, string email)
        {
            var client = new RestClient(_emailSettings.Value.BaseUrl);
            var request = new RestRequest(_emailSettings.Value.Resource, Method.Post);

            client.Authenticator = new HttpBasicAuthenticator(_emailSettings.Value.User, _emailSettings.Value.Password);

            request.AddParameter(_emailSettings.Value.Domain, _emailSettings.Value.Host, ParameterType.UrlSegment);
            request.Resource = "{domain}/messages";
            request.AddParameter("from", _emailSettings.Value.From);
            request.AddParameter("to", email);
            request.AddParameter("subject", _emailSettings.Value.Subject);
            request.AddParameter("text", body);

            request.Method = Method.Post;
            var response = await client.ExecuteAsync(request);

            return response.IsSuccessful;
        }
    }
}