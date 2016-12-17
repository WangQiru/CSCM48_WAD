namespace WebApplication.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using System.Data.Entity.Migrations;
    using System.Security.Claims;

    internal sealed class Configuration : DbMigrationsConfiguration<WebApplication.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(WebApplication.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);

            for (int i = 0; i < 6; i++)
            {
                var userToInsert = new ApplicationUser { UserName = i.ToString(), Email = "Student" + i + "@email.com", StudentNo = i, Name = "Student " + i };
                                
                var result = userManager.Create(userToInsert, "password");

                if (result.Succeeded)
                {
                    userManager.AddToRole(userToInsert.Id, "Student");
                    userManager.AddClaim(userToInsert.Id, (new Claim("Role", "Student")));
                    userManager.AddClaim(userToInsert.Id, (new Claim("Permission", "Attempt Tests")));
                }

            }

            int lectureNO = 100;

            var lecturer = new ApplicationUser { UserName = lectureNO.ToString(), Email = "Lecturer1@email.com", LectureNo = lectureNO, Name = "Lecturer 1 "};

            var result2 = userManager.Create(lecturer, "password");
            if (result2.Succeeded)
            {
                userManager.AddToRole(lecturer.Id, "Lecturer");
                userManager.AddClaim(lecturer.Id, (new Claim("Role", "Lecturer")));
                userManager.AddClaim(lecturer.Id, (new Claim("Permission", "CRUD MCQ")));
                userManager.AddClaim(lecturer.Id, (new Claim("Permission", "Attempt Tests")));
            }
        }
    }
}
