namespace WebApplication.Migrations
{
    using Microsoft.AspNet.Identity;
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

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

            /**

            context.Configuration.LazyLoadingEnabled = true;

            var passwordHash = new PasswordHasher();
            string password = passwordHash.HashPassword("password");

            context.Users.AddOrUpdate(u => u.UserName,
                new ApplicationUser
                {
                    UserName = "123456",
                    Email = "Lecturer1@email.com",
                    PasswordHash = password,
                    LectureNo = 123456
                },
                new ApplicationUser
                {
                    UserName = "9999",
                    Email = "Student9999@email.com",
                    PasswordHash = password,
                    StudentNo = 9999
                },
                                new ApplicationUser
                                {
                                    UserName = "1",
                                    Email = "Student1@email.com",
                                    PasswordHash = password,
                                    StudentNo = 1
                                },
                                                new ApplicationUser
                                                {
                                                    UserName = "2",
                                                    Email = "Student2@email.com",
                                                    PasswordHash = password,
                                                    StudentNo = 2
                                                },
                                                                new ApplicationUser
                                                                {
                                                                    UserName = "3",
                                                                    Email = "Student3@email.com",
                                                                    PasswordHash = password,
                                                                    StudentNo = 3
                                                                },
                                                                new ApplicationUser
                                                                {
                                                                    UserName = "5",
                                                                    Email = "Student5@email.com",
                                                                    PasswordHash = password,
                                                                    StudentNo = 5
                                                                },
                                                                new ApplicationUser
                                                                {
                                                                    UserName = "4",
                                                                    Email = "Student4@email.com",
                                                                    PasswordHash = password,
                                                                    StudentNo = 4
                                                                }
                );
    **/
        }
    }
}
