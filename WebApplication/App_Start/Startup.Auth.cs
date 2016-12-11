using System;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Google;
using Owin;
using WebApplication.Models;
using Schaeflein.Community.MVC5AuthZPolicy;

namespace WebApplication
{
    public partial class Startup
    {
        // For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {
            var options = new AuthorizationOptions();
            options.AddPolicy("Lecturer", policy =>
            {
                policy.RequireClaim("Permission", "CRUD MCQ");
                policy.RequireClaim("Role", "Lecturer");
            });

            options.AddPolicy("Cheat", policy =>
            {
                policy.RequireClaim("May-I-Cheat-As-A-Student", "Yes");
            });

            options.AddPolicy("studentEnrolled", policy =>
            {
                policy.RequireClaim("Permission", "Attempt Tests");
            });



            app.UseMVC5AuthZPolicy(AuthorizationService.Create(options));

            // Configure the db context, user manager and signin manager to use a single instance per request
            app.CreatePerOwinContext(ApplicationDbContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            app.CreatePerOwinContext<ApplicationSignInManager>(ApplicationSignInManager.Create);

            // Enable the application to use a cookie to store information for the signed in user
            // and to use a cookie to temporarily store information about a user logging in with a third party login provider
            // Configure the sign in cookie
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
                Provider = new CookieAuthenticationProvider
                {
                    // Enables the application to validate the security stamp when the user logs in.
                    // This is a security feature which is used when you change a password or add an external login to your account.  
                    OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<ApplicationUserManager, ApplicationUser>(
                        validateInterval: TimeSpan.FromMinutes(0),
                        regenerateIdentity: (manager, user) => user.GenerateUserIdentityAsync(manager))
                }
            });
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            // Enables the application to temporarily store user information when they are verifying the second factor in the two-factor authentication process.
            app.UseTwoFactorSignInCookie(DefaultAuthenticationTypes.TwoFactorCookie, TimeSpan.FromMinutes(5));

            // Enables the application to remember the second login verification factor such as phone or email.
            // Once you check this option, your second step of verification during the login process will be remembered on the device where you logged in from.
            // This is similar to the RememberMe option when you log in.
            app.UseTwoFactorRememberBrowserCookie(DefaultAuthenticationTypes.TwoFactorRememberBrowserCookie);

            // Uncomment the following lines to enable logging in with third party login providers
            app.UseMicrosoftAccountAuthentication(
                clientId: "4cd331c0-90f3-450f-9469-864f5cf69dd4",
                clientSecret: "gcqK3ZHnj4pDtY8cQv0Y8f3");

            app.UseTwitterAuthentication(
               consumerKey: "Vl2gnnh5hUn6houoiHmpulNqi",
               consumerSecret: "nowP1tfvwLjDdVRYMC0YqLCmBhvLfG6RL5T8kbBh0veVezLPtz");

            app.UseFacebookAuthentication(
               appId: "251745348576037",
               appSecret: "da260a592c39cdeeed7e4c0de04be027");

            app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions()
            {
                ClientId = "1090508238142-638jqb40i47ncip4q50s0qev0423t0mq.apps.googleusercontent.com",
                ClientSecret = "QF0llLbrGXAqlHAsrx9GzHz5"
            });
        }
    }
}