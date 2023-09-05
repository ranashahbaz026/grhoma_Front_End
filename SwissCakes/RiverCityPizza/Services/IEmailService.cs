namespace SwissConfectionery.Services
{
    public interface IEmailService
    {
     
        bool SendEmail(string fromName, string subject,
            string body, string sender, string recipient,  bool isBodyHtml = true, bool isCustomerEmail = false, IEnumerable<string> emailAttachments = null);
   
    }
}
