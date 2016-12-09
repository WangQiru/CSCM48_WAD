using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Web;

namespace WebApplication.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            if (this.LectureNo > 0)
            {
                userIdentity.AddClaim(new Claim(ClaimTypes.Role, "Lecturer"));
                manager.AddToRole(this.Id, "Lecturer");
            }
            else
            {
                manager.AddToRole(this.Id, "Student");
            }
            // Add custom user claims here
            return userIdentity;
        }

        public string Name { get; set; }
        public int StudentNo { get; set; }
        public int LectureNo { get; set; }
    }
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }


        public DbSet<MCQModels> MCQs { get; set; }
        public DbSet<QuestionModels> Questions { get; set; }
        public DbSet<OptionModels> Options { get; set; }
        public DbSet<ResponseModels> Responses { get; set; }
    }
}