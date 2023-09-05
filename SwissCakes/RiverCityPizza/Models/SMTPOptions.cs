namespace SwissConfectionery.Models
{
    public class SMTPOptions
    {
        public const string Section = "SMTP";

        public SMTPOptions()
        {

        }

        public string Host { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int Port { get; set; }
        public bool EnableSSL { get; set; }
    }
}
