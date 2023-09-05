using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using SwissConfectionery.Models;

namespace SwissConfectionery.Services
{
    public class CaptchaVerificationService : ICaptchaVerificationService
    {
        private readonly CaptchaOptions _captchaSettings;
        private ILogger<CaptchaVerificationService> logger;

        public CaptchaVerificationService(IOptions<CaptchaOptions> captchaSettings, ILogger<CaptchaVerificationService> logger)
        {
            _captchaSettings = captchaSettings.Value;
            this.logger = logger;
        }

        public async Task<bool> IsCaptchaValid(string token)
        {
            var result = false;

            var googleVerificationUrl = "https://www.google.com/recaptcha/api/siteverify";

            try
            {
                using var client = new HttpClient();

                var response = await client.PostAsync($"{googleVerificationUrl}?secret={_captchaSettings.SecretKey}&response={token}", null);
                var jsonString = await response.Content.ReadAsStringAsync();
                var captchaVerfication = JsonConvert.DeserializeObject<CaptchaResponse>(jsonString);

                result = captchaVerfication.Success;
            }
            catch (Exception e)
            {
                // fail gracefully, but log
                logger.LogError("Failed to process captcha validation", e);
            }

            return result;
        }
    }
}
