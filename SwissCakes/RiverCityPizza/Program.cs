using SwissConfectionery.Models;
using SwissConfectionery.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.Configure<CaptchaOptions>(builder.Configuration.GetSection(CaptchaOptions.Section));
builder.Services.Configure<EmailOptions>(builder.Configuration.GetSection(EmailOptions.Section));
builder.Services.Configure<AuthOptions>(builder.Configuration.GetSection(AuthOptions.Section));
builder.Services.Configure<SMTPOptions>(builder.Configuration.GetSection(SMTPOptions.Section));
builder.Services.AddScoped<ICaptchaVerificationService, CaptchaVerificationService>();
builder.Services.AddScoped<IEmailService, EmailService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "HomePage",
    pattern: "/",
    defaults: new { controller = "Home", action = "Index" });

app.MapControllerRoute(
    name: "Menu",
    pattern: "/menu",
    defaults: new { controller = "Home", action = "Menu" });

app.MapControllerRoute(
    name: "Directions",
    pattern: "/directions",
    defaults: new { controller = "Home", action = "Directions" });

app.MapControllerRoute(
    name: "Contact",
    pattern: "/contact",
    defaults: new { controller = "Home", action = "Contact" });

app.MapControllerRoute(
    name: "Videos",
    pattern: "/videos",
    defaults: new { controller = "Home", action = "Videos" });

app.MapControllerRoute(
    name: "Terms",
    pattern: "/tos",
    defaults: new { controller = "Home", action = "TermsAndConditions" });

app.MapControllerRoute(
    name: "Privacy",
    pattern: "/privacy",
    defaults: new { controller = "Home", action = "Privacy" });

app.MapControllerRoute(
    name: "ThankYou",
    pattern: "/thank-you",
    defaults: new { controller = "Home", action = "ThankYou" });

app.MapControllerRoute(
    name: "Subscriber",
    pattern: "/subscriber",
    defaults: new { controller = "Home", action = "Subscriber" });

app.Run();
