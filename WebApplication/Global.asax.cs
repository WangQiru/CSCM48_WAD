using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using WebApplication.Controllers;
using WebApplication.Models;

namespace WebApplication
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            //GlobalFilters.Filters.Add(new RequireHttpsAttribute());
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            // Seed te users required
            Seed(new ApplicationDbContext());
        }


        protected void Seed(ApplicationDbContext context)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var passwordHash = new PasswordHasher();
            string password = passwordHash.HashPassword("password");
            var newUserList = new List<ApplicationUser>();

            var newUser = new ApplicationUser
            {
                UserName = "123456",
                Email = "Lecturer1@email.com",
                PasswordHash = password,
                LectureNo = 123456
            };

            newUserList.Add(newUser);

            newUser = new ApplicationUser
            {
                UserName = "1",
                Email = "Student1@email.com",
                PasswordHash = password,
                StudentNo = 1,
            };

            newUserList.Add(newUser);

            newUser = new ApplicationUser
            {
                UserName = "2",
                Email = "Student2@email.com",
                PasswordHash = password,
                StudentNo = 2,
            };

            newUserList.Add(newUser);

            newUser = new ApplicationUser
            {
                UserName = "3",
                Email = "Student3@email.com",
                PasswordHash = password,
                StudentNo = 3,
            };

            newUserList.Add(newUser);

            newUser = new ApplicationUser
            {
                UserName = "4",
                Email = "Student4@email.com",
                PasswordHash = password,
                StudentNo = 4,
            };

            newUserList.Add(newUser);

            newUser = new ApplicationUser
            {
                UserName = "5",
                Email = "Student5@email.com",
                PasswordHash = password,
                StudentNo = 5,
            };

            newUserList.Add(newUser);

            newUser = new ApplicationUser
            {
                UserName = "9999",
                Email = "Student9999@email.com",
                PasswordHash = password,
                StudentNo = 9999,
            };

            newUserList.Add(newUser);

            foreach (ApplicationUser u in newUserList)
            {
                ApplicationUser user = userManager.FindByName(u.UserName);
                if (user == null)
                {
                    userManager.Create(u);
                }
            }


        }


        public void Application_Error(Object sender, EventArgs e)
        {
            Exception exception = Server.GetLastError();
            Server.ClearError();

            var routeData = new RouteData();
            routeData.Values.Add("controller", "ErrorPage");
            routeData.Values.Add("action", "Error");
            routeData.Values.Add("exception", exception);

            if (exception.GetType() == typeof(HttpException))
            {
                routeData.Values.Add("statusCode", ((HttpException)exception).GetHttpCode());
                routeData.Values.Add("statusMsg", ((HttpException)exception).GetHtmlErrorMessage());
            }
            else
            {
                routeData.Values.Add("statusCode", 500);
                routeData.Values.Add("statusMsg", "Something went haywire, maybe try again later?");
            }

            Response.TrySkipIisCustomErrors = true;
            IController controller = new ErrorPageController();
            controller.Execute(new RequestContext(new HttpContextWrapper(Context), routeData));
            Response.End();
        }

        protected void Application_EndRequest(object sender, EventArgs e)
        {
            if (Context.Response.StatusCode == 401 || Context.Response.StatusCode == 403)
            {
                // this is important, because the 401 is not an error by default!!!
                throw new HttpException(401, "You are not authorised");
            }
        }
    }


}
