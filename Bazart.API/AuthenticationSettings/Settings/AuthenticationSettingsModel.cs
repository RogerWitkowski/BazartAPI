namespace Bazart.AuthenticationSettings.Settings
{
    public class AuthenticationSettingsModel
    {
        public string JwtKey { get; set; }
        public int JwtExpireMinutes { get; set; }
        public string JwtIssuer { get; set; }
        public string JwtAudience { get; set; }
    }
}