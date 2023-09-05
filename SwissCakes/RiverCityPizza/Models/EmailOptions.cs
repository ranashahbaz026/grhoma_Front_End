namespace SwissConfectionery.Models
{
    public class EmailOptions
    {
        public const string Section = "EmailOptions";
        public EmailOptions() { }
        public string From { get; set; }
        public string To { get; set; }
        public string Cc { get; set; }
        public string Bcc { get; set; }
    }
}
