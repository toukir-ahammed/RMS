namespace RMS.DAL.RMSMigrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using RMSDataModel;

    internal sealed class Configuration : DbMigrationsConfiguration<RMS.DAL.RMSContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"DAL\RMSMigrations";
        }

        protected override void Seed(RMS.DAL.RMSContext context)
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

            //var students = new List<Student>
            //{
            //    new Student { Name = "Toukir Ahammed", RegistrationNumber = "2015-318-00", ClassRoll = 806},
            //    new Student { Name = "Aba Kowser", RegistrationNumber = "2015-319-000", ClassRoll = 825}
            //};


        }
    }
}
