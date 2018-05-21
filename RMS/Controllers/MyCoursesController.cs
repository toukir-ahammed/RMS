using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using RMS.Models;
using RMSDataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace RMS.Controllers
{
    public class MyCoursesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: MyCourses
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            var currentUser = manager.FindById(userId);

            //var instructors = db.Instructors
            //    .Include(i => i.) db.Instructors.Include(i => i.C)
            //    .OrderBy(i => i.Name);

            var instructorId = currentUser.Instructor.ID;

            var instructor = db.Instructors.Find(instructorId);
            if (instructor == null)
            {
                return HttpNotFound();
            }

            //var courses = instructor.Courses;
            
            //var courses = db.Instructors
            //    .Where(i => i.ID == instructorId).Single().Courses;

            //if(courses == null)
            //{
            //    return HttpNotFound();
            //}

            //var assignments = db.Instructors
            //    .Where(x => x.ID == instructorId)
            //    .Single().Assignments;

            var assignments = instructor.Assignments;

            if (assignments == null)
            {
                return HttpNotFound();
            }

            return View(assignments);
        }

        // GET: MyCourses/Details/5
        public ActionResult Details(int? courseID)
        {
            if (courseID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            Course course = db.Courses.Find(courseID);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        // GET: MyCourses/StudentList
        public ActionResult StudentList(int? courseID)
        {
            if(courseID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Course course = db.Courses.Find(courseID);
            if(course == null)
            {
                return HttpNotFound();
            }

            var enrollments = course.Enrollments;


            //Alternative
            //var enrollments = db.Courses
            //    .Where(x => x.CourseID == courseID)
            //    .Single().Enrollments;
            if (enrollments == null)
            {
                return HttpNotFound();
            }
            ViewBag.CourseName = course.Title;

            return View(enrollments);
            
        }
    }
}