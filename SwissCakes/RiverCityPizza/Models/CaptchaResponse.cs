namespace SwissConfectionery.Models
{
    public class CaptchaResponse
    {
        public bool Success { get; set; }
        public DateTime Challenge_ts { get; set; }
        public string Hostname { get; set; }
        public double Score { get; set; }
        public string Action { get; set; }
    }
}
