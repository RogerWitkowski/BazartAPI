namespace Bazart.EmailService.SettingModel
{
    public class EmailSendingSettingsModel
    {
        public string Host { get; set; }
        public string User { get; set; }
        public string BaseUrl { get; set; }
        public string From { get; set; }
        public string Password { get; set; }
        public string Domain { get; set; }
        public string Resource { get; set; }
        public string Subject { get; set; }
    }
}