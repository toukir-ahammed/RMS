namespace RMS.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using RMS.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<RMS.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "RMS.Models.ApplicationDbContext";
        }

        protected override void Seed(RMS.Models.ApplicationDbContext context)
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

            if (!context.Users.Any( t=> t.UserName == "admin@rms.com"))
            {
                var user = new ApplicationUser
                {
                    UserName = "admin@rms.com",
                    Email = "admin@rms.com",
                    MyUserInfo = new MyUserInfo { FirstName = "Admin", LastName = "Admin" },
                    EmailConfirmed = true
                };
                userManager.Create(user, "@Bcd1234");

                context.Roles.AddOrUpdate(m => m.Name, new IdentityRole { Name = "Admin" });
                context.Roles.AddOrUpdate(m => m.Name, new IdentityRole { Name = "Instructor" });
                context.SaveChanges();

                userManager.AddToRole(user.Id, "Admin");
            }
        }
    }
}
