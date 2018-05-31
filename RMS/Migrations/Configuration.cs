namespace RMS.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using RMS.Models;
    using RMSDataModel;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<RMS.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
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

            if (!context.Users.Any(t => t.UserName == "admin@rms.com"))
            {
                var user = new ApplicationUser
                {
                    UserName = "admin@rms.com",
                    Email = "admin@rms.com",
                    //MyUserInfo = new MyUserInfo { FirstName = "Admin", LastName = "Admin" },
                    EmailConfirmed = true
                };
                userManager.Create(user, "@Bcd1234");

                context.Roles.AddOrUpdate(m => m.Name, new IdentityRole { Name = "Admin" });
                context.Roles.AddOrUpdate(m => m.Name, new IdentityRole { Name = "Instructor" });
                context.SaveChanges();

                userManager.AddToRole(user.Id, "Admin");
            }

            var departments = new List<Department>
            {
                new Department { Name = "IIT", LeastCGPAToPass = 2.50, LeastGPToPass = 2.00},
                new Department { Name = "CSE", LeastCGPAToPass = 2.50, LeastGPToPass = 2.00},
                new Department { Name = "Statistics", LeastCGPAToPass = 2.50, LeastGPToPass = 2.00},
                new Department { Name = "Pharmacy", LeastCGPAToPass = 2.50, LeastGPToPass = 2.00},
                new Department { Name = "IBA", LeastCGPAToPass = 2.50, LeastGPToPass = 2.00},
                new Department { Name = "Genetics", LeastCGPAToPass = 2.50, LeastGPToPass = 2.00},
                new Department { Name = "Microbiology", LeastCGPAToPass = 2.50, LeastGPToPass = 2.00},
                new Department { Name = "English", LeastCGPAToPass = 2.50, LeastGPToPass = 2.00},
                new Department { Name = "Mathematics", LeastCGPAToPass = 2.50, LeastGPToPass = 2.00},
                new Department { Name = "Economics", LeastCGPAToPass = 2.50, LeastGPToPass = 2.00}
            };
            departments.ForEach(s => context.Departments.AddOrUpdate(p => p.Name, s));
            context.SaveChanges();



            var students = new List<Student>
            {
                new Student { Name = "Toukir Ahammed",  ClassRoll = 806, ExamRoll = 822,
                    RegistrationNumber = "201531800", Session = "2015-16", DepartmentId = 7 },
                new Student { Name = "Tahmid Khan",  ClassRoll = 801, ExamRoll = 801,
                    RegistrationNumber = "201531801", Session = "2015-16", DepartmentId = 7 },
                new Student { Name = "Iftekhar Jamil",  ClassRoll = 802, ExamRoll = 802,
                    RegistrationNumber = "201531802", Session = "2015-16" , DepartmentId = 7 },
                new Student { Name = "Tahlil",  ClassRoll = 803, ExamRoll = 803,
                    RegistrationNumber = "201531803", Session = "2015-16" , DepartmentId = 7 },
                new Student { Name = "Abdullah Al Jubaer",  ClassRoll = 812, ExamRoll = 823,
                    RegistrationNumber = "201531805", Session = "2015-16" , DepartmentId = 7 },
                new Student { Name = "Aba Kowser",  ClassRoll = 825, ExamRoll = 825,
                    RegistrationNumber = "201531809", Session = "2015-16" , DepartmentId = 7 }
            };

            students.ForEach(s => context.Students.AddOrUpdate(p => p.Name, s));
            context.SaveChanges();

            //var instructors = new List<Instructor>
            //{
            //    new Instructor { Name = "BM Mainul Hossain", Designation = "Associate Professor" },
            //    new Instructor { Name = "Amit Seal Ami", Designation = "Lecturer" },
            //    new Instructor { Name = "Nadia Nahar", Designation = "Lecturer" },
            //    new Instructor { Name = "Zerina Begum", Designation = "Professor" },
            //    new Instructor { Name = "Toukir Ahammed", Designation = "Lecturer"}

            //};
            //instructors.ForEach(s => context.Instructors.AddOrUpdate(p => p.Name, s));
            //context.SaveChanges();


            //var instructors = new List<Instructor>
            //{
            //    new Instructor { Name = "BM Mainul Hossain", Designation = "Associate Professor" },
            //    new Instructor { Name = "Amit Seal Ami", Designation = "Lecturer" },
            //    new Instructor { Name = "Nadia Nahar", Designation = "Lecturer" },
            //    new Instructor { Name = "Zerina Begum", Designation = "Professor" },
            //    new Instructor { Name = "Toukir Ahammed", Designation = "Lecturer"}

            //};
            //instructors.ForEach(s => context.Instructors.AddOrUpdate(p => p.Name, s));
            //context.SaveChanges();

            //var departments = new List<Department>
            //{
            //    new Department { Name = "English"},
            //    new Department { Name = "Mathematics"},
            //    new Department { Name = "Engineering"},
            //    new Department { Name = "Economics"}
            //};
            //departments.ForEach(s => context.Departments.AddOrUpdate(p => p.Name, s));
            //context.SaveChanges();

            //var courses = new List<Course>
            //{
            //    new Course {CourseID = 1050, Title = "Chemistry", Credits = 3,
            //      DepartmentID = departments.Single( s => s.Name == "Engineering").DepartmentID,
            //      Instructors = new List<Instructor>()
            //    },
            //    new Course {CourseID = 4022, Title = "Microeconomics", Credits = 3,
            //      DepartmentID = departments.Single( s => s.Name == "Economics").DepartmentID,
            //      Instructors = new List<Instructor>()
            //    },
            //    new Course {CourseID = 4041, Title = "Macroeconomics", Credits = 3,
            //      DepartmentID = departments.Single( s => s.Name == "Economics").DepartmentID,
            //      Instructors = new List<Instructor>()
            //    },
            //    new Course {CourseID = 1045, Title = "Calculus",       Credits = 3,
            //      DepartmentID = departments.Single( s => s.Name == "Mathematics").DepartmentID,
            //      Instructors = new List<Instructor>()
            //    },
            //    new Course {CourseID = 3141, Title = "Trigonometry",   Credits = 3,
            //      DepartmentID = departments.Single( s => s.Name == "Mathematics").DepartmentID,
            //      Instructors = new List<Instructor>()
            //    },
            //    new Course {CourseID = 2021, Title = "Composition",    Credits = 3,
            //      DepartmentID = departments.Single( s => s.Name == "English").DepartmentID,
            //      Instructors = new List<Instructor>()
            //    },
            //    new Course {CourseID = 2042, Title = "Literature",     Credits = 3,
            //      DepartmentID = departments.Single( s => s.Name == "English").DepartmentID,
            //      Instructors = new List<Instructor>()
            //    }
            //};
            //courses.ForEach(s => context.Courses.AddOrUpdate(p => p.CourseID, s));
            //context.SaveChanges();



            //AddOrUpdateInstructor(context, "Chemistry", "BM Mainul Hossain");
            //AddOrUpdateInstructor(context, "Chemistry", "Amit Seal Ami");
            //AddOrUpdateInstructor(context, "Microeconomics", "Zerina Begum");
            //AddOrUpdateInstructor(context, "Macroeconomics", "Amit Seal Ami");

            //AddOrUpdateInstructor(context, "Calculus", "BM Mainul Hossain");
            //AddOrUpdateInstructor(context, "Trigonometry", "Nadia Nahar");
            //AddOrUpdateInstructor(context, "Composition", "Nadia Nahar");
            //AddOrUpdateInstructor(context, "Literature", "BM Mainul Hossain");
            //AddOrUpdateInstructor(context, "Chemistry", "Toukir Ahammed");
            //AddOrUpdateInstructor(context, "Calculus", "Toukir Ahammed");

            //context.SaveChanges();

            //var enrollments = new List<Enrollment>
            //    {
            //        new Enrollment {
            //            StudentId = students.Single(s => s.Name == "Toukir Ahammed").ID,
            //            CourseID = courses.Single(c => c.Title == "Chemistry" ).CourseID
            //        },
            //         new Enrollment {
            //            StudentId = students.Single(s => s.Name == "Tahmid Khan").ID,
            //            CourseID = courses.Single(c => c.Title == "Microeconomics" ).CourseID
            //         },
            //         new Enrollment {
            //            StudentId = students.Single(s => s.Name == "Tahlil").ID,
            //            CourseID = courses.Single(c => c.Title == "Macroeconomics" ).CourseID
            //         },
            //         new Enrollment {
            //             StudentId = students.Single(s => s.Name == "Toukir Ahammed").ID,
            //            CourseID = courses.Single(c => c.Title == "Calculus" ).CourseID
            //         },
            //         new Enrollment {
            //            StudentId = students.Single(s => s.Name == "Abdullah Al Jubaer").ID,
            //            CourseID = courses.Single(c => c.Title == "Trigonometry" ).CourseID
            //         },
            //         new Enrollment {
            //            StudentId = students.Single(s => s.Name == "Tahlil").ID,
            //            CourseID = courses.Single(c => c.Title == "Composition" ).CourseID
            //         },
            //         new Enrollment {
            //            StudentId = students.Single(s => s.Name == "Iftekhar Jamil").ID,
            //            CourseID = courses.Single(c => c.Title == "Chemistry" ).CourseID
            //         },
            //         new Enrollment {
            //            StudentId = students.Single(s => s.Name == "Aba Kowser").ID,
            //            CourseID = courses.Single(c => c.Title == "Microeconomics").CourseID
            //         },
            //        new Enrollment {
            //            StudentId = students.Single(s => s.Name == "Tahmid Khan").ID,
            //            CourseID = courses.Single(c => c.Title == "Chemistry").CourseID
            //         },
            //         new Enrollment {
            //            StudentId = students.Single(s => s.Name == "Aba Kowser").ID,
            //            CourseID = courses.Single(c => c.Title == "Composition").CourseID
            //         },
            //         new Enrollment {
            //            StudentId = students.Single(s => s.Name == "Tahlil").ID,
            //            CourseID = courses.Single(c => c.Title == "Literature").CourseID
            //         }
            //    };

            //foreach (Enrollment e in enrollments)
            //{
            //    var enrollmentInDataBase = context.Enrollments.Where(
            //        s =>
            //             s.Student.ID == e.StudentId &&
            //             s.Course.CourseID == e.CourseID).SingleOrDefault();
            //    if (enrollmentInDataBase == null)
            //    {
            //        context.Enrollments.Add(e);
            //    }
            //}
            //context.SaveChanges();
        }

        //void AddOrUpdateInstructor(RMSContext context, string courseTitle, string instructorName)
        //{
        //    var crs = context.Courses.SingleOrDefault(c => c.Title == courseTitle);
        //    var inst = crs.Instructors.SingleOrDefault(i => i.Name == instructorName);
        //    if (inst == null)
        //        crs.Instructors.Add(context.Instructors.Single(i => i.Name == instructorName));
        //}
    }
}

