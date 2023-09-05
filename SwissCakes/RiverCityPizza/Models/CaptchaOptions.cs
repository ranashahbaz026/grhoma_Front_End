namespace SwissConfectionery.Models
{
    public class CaptchaOptions
    {
        public const string Section = "CaptchaOptions";
        public string SecretKey { get; set; }
        public string SiteKey { get; set; }
    }
}
