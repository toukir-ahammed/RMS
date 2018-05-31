using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using RMS.Models;
using RMSDataModel;

namespace RMS.Controllers
{
    [Authorize(Roles = "Admin")]
    public class InstructorsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Instructors
        public ActionResult Index()
        {
            ////var instructors = db.Instructors.Include(i => i.Department);
            ////return View(instructors.ToList());
            //var instructors = db.Users.Include(i => i.Instructor.Courses);

            var userId = User.Identity.GetUserId();
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            var currentUser = manager.FindById(userId);
            //var crs = currentUser.Instructor.Courses;
            ////var courses = db.Courses.Include(i => i.Department)
            ////    .Where( i=> i.);
            ////return View("~/Views/Courses/Index.cshtml", courses.ToList());
            //return View(instructors..ToList());
            ////var viewModel = new InstructorIndexData();
            ////viewModel.Instructors = db.Instructors
            ////    .Include(i => i.OfficeAssignment)
            ////    .Include(i => i.Courses.Select(c => c.Department))
            ////    .OrderBy(i => i.LastName);


            //var courses =  from c in db.Instructors
            //               where(c.)

            //var viewmodel = new InstructorIndexData();
            //var instructors = db.Instructors.Include(i => i.Courses).ToList();
            //foreach( var i in instructors )
            //{
            //    Console.WriteLine(i.Designation);
            //}

            //var instr = db.Instructors
            //    .Include(i => i.Courses.Select(c => c.Department))
            //    .Where(i => i.ID == currentUser.Instructor.ID).Single().Courses;
            //    //.Inc Courses;
            //return View(instr.ToList());

            var instructors = db.Instructors
                .Include(i => i.Courses)
                .OrderBy(i => i.Name);
            var instructorId = currentUser.Instructor.ID;
            var courses = instructors
                .Where(i => i.ID == instructorId).Single().Courses;

            var Enrollments = db.Instructors.Where(
            x => x.ID == instructorId).Single().Assignments;

            return View(Enrollments);

        }

        public ActionResult StudentList(int? id)
        {
            var enrollments = db.Courses.Where(
            x => x.CourseID == id).Single().Enrollments;

            return View(enrollments);
        }

        // GET: Instructors/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Instructor instructor = db.Instructors.Find(id);
            //if (instructor == null)
            //{
            //    return HttpNotFound();
            //}
            //return View(instructor);

            Course course = db.Courses.Find(id);
            if(course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        // GET: Instructors/Create
        public ActionResult Create()
        {
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentID", "Name");
            return View();
        }

        // POST: Instructors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Designation,DepartmentId")] Instructor instructor)
        {
            if (ModelState.IsValid)
            {
                db.Instructors.Add(instructor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentID", "Name", instructor.DepartmentId);
            return View(instructor);
        }

        // GET: Instructors/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Instructor instructor = db.Instructors.Find(id);
            if (instructor == null)
            {
                return HttpNotFound();
            }
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentID", "Name", instructor.DepartmentId);
            return View(instructor);
        }

        // POST: Instructors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Designation,DepartmentId")] Instructor instructor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(instructor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentID", "Name", instructor.DepartmentId);
            return View(instructor);
        }

        // GET: Instructors/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Instructor instructor = db.Instructors.Find(id);
            if (instructor == null)
            {
                return HttpNotFound();
            }
            return View(instructor);
        }

        // POST: Instructors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Instructor instructor = db.Instructors.Find(id);
            db.Instructors.Remove(instructor);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
