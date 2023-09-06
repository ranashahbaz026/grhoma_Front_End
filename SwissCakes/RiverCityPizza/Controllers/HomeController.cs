using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SwissConfectionery.Models;
using SwissConfectionery.Services;
using System.Diagnostics;

namespace SwissConfectionery.Controllers
{
    public class HomeController : Controller
    {
        private readonly IOptions<CaptchaOptions> _captchaOptions;
        private readonly ICaptchaVerificationService _captchaVerificationService;
        private readonly IEmailService _emailService;
        private readonly IOptions<EmailOptions> _emailOptions;
        private readonly IOptions<AuthOptions> _authOptions;


        [BindProperty(Name = "g-recaptcha-response")]
        public string CaptchaResponse { get; set; }

        public HomeController
            (
                IOptions<CaptchaOptions> captchaOptions,
                ICaptchaVerificationService captchaVerificationService,
                IEmailService emailService,
                IOptions<EmailOptions> emailOptions,
                IOptions<AuthOptions> authOptions
            )
        {
            _captchaOptions = captchaOptions;
            _captchaVerificationService = captchaVerificationService;
            _emailOptions = emailOptions;
            _emailService = emailService;
            _authOptions = authOptions;
        }
        public IActionResult Index()
        {
            return View();
        }
        
        public IActionResult WeddingCakes()
        {
            return View();
        }

        public IActionResult Menu()
        {
            return View();
        }

        public IActionResult Directions()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View(new ContactModel());
        }

        [HttpPost]
        public async Task<IActionResult> Contact(ContactModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var requestIsValid = await _captchaVerificationService.IsCaptchaValid(CaptchaResponse);
            if (requestIsValid)
            {
                string body = "<p>&nbsp;</p><table style='border-collapse: collapse; width: 78.8352%;' border='1'><tbody><tr><td style='width: 24.5739%;'>Name:</td><td style='width: 54.2613%;'>XXXNAME</td></tr><tr><td style='width: 24.5739%;'>Telephone:</td><td style='width: 54.2613%;'>XXXTELEPHONE</td></tr><tr><td style='width: 24.5739%;'>Email Address:</td><td style='width: 54.2613%;'>XXXEMAIL</td></tr><tr><td style='width: 24.5739%;'>The Message:</td><td style='width: 54.2613%;'>XXXMESSAGE</td></tr></tbody></table><p>&nbsp;</p>";
                body = body.Replace("XXXNAME", model.Name).Replace("XXXTELEPHONE", model.Phone).Replace("XXXEMAIL", model.Email).Replace("XXXADDRESS", "").Replace("XXXCITYSTATEZIP", "").Replace("XXXMESSAGE", model.Message);


                _emailService.SendEmail("Swiss Confectionery",
                                          "Swiss Confectionery Information Request",
                                          body,
                                          _emailOptions.Value.From,
                                          _emailOptions.Value.To,
                                          true,
                                          false
                                          );

                body = "Thank you for reaching out. We will be back with you shortly";
                _emailService.SendEmail("Swiss Confectionery",
                                          "Swiss Confectionery Information Request",
                                          body,
                                          _emailOptions.Value.From,
                                          model.Email,
                                          true,
                                          true
                                          );

                return RedirectToRoute("ThankYou");
            }
            else
            {
                return BadRequest();
            }

        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult TermsAndConditions()
        {
            return View();
        }

        public IActionResult Videos()
        {
            bool.TryParse(Convert.ToString(TempData["Authenticate"]), out bool isAuthenticate);
            var model = new AuthenticateModel()
            {
                Authenticate = isAuthenticate
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult Videos(AuthenticateModel model)
        {
            if(_authOptions.Value.Password.Equals(model.Password))
            {
                TempData["Authenticate"] = true;
                ModelState.Clear();
                return RedirectToRoute("Videos");
            }
            else
            {
                ModelState.Clear();
                ModelState.AddModelError("", "Password is invalid");
            }
            model.Authenticate = false;
            return View(model);
        }

        public IActionResult ThankYou()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Subscriber(NewsLetterSubscriptionModel model, string returnUrl)
        {
            string body = "<p>&nbsp;</p><table style='border-collapse: collapse; width: 78.8352%;' border='1'><tbody><tr><td style='width: 24.5739%;'>Name:</td><td style='width: 54.2613%;'>XXXNAME</td></tr><tr><td style='width: 24.5739%;'>Email Address:</td><td style='width: 54.2613%;'>XXXEMAIL</td></tr><tr><td style='width: 24.5739%;'>Preffered Location:</td><td style='width: 54.2613%;'>XXXLOCATION</td></tr></tbody></table><p>&nbsp;</p>";
            body = body.Replace("XXXNAME", model.SubscriberName).Replace("XXXEMAIL", model.SubscriberEmail).Replace("XXXLOCATION", "");


            _emailService.SendEmail("Swiss Confectionery",
                                      "Swiss Confectionery Subscribe News Letter",
                                      body,
                                      _emailOptions.Value.From,
                                      _emailOptions.Value.To
                                      ) ;
            TempData["SuccessSubscribe"] = "true";
            return RedirectToRoute(returnUrl);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}