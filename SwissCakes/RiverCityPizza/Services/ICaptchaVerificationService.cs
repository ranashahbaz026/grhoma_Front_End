namespace SwissConfectionery.Services
{
    public interface ICaptchaVerificationService
    {
        Task<bool> IsCaptchaValid(string token);
    }
}
