using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using RMS.Models;
using RMSDataModel;

namespace RMS.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AssignmentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Assignments
        public ActionResult Index()
        {
            var assignments = db.Assignments.Include(a => a.Course).Include(a => a.Instructor);
            return View(assignments.ToList());
        }

        // GET: Assignments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Assignment assignment = db.Assignments.Find(id);
            if (assignment == null)
            {
                return HttpNotFound();
            }
            return View(assignment);
        }

        // GET: Assignments/Create
        public ActionResult Create()
        {
            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "Title");
            ViewBag.InstructorID = new SelectList(db.Instructors, "ID", "Name");
            return View();
        }

        // POST: Assignments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AssignmentID,InstructorID,CourseID")] Assignment assignment)
        {
            if (ModelState.IsValid)
            {
                //string t = DateTime.Now.ToLongTimeString();
                string fileName  = DateTime.Now.ToString("yyyyMMddHHmmss") + "_"
                    + assignment.CourseID + "_"
                    + assignment.InstructorID +
                    ".json";



                string path = Server.MapPath("~/MarkSheets/");
                path = Path.Combine(path, fileName);

                if(!generateInitialSpreadSheet(assignment.CourseID,path))
                {
                    return HttpNotFound();
                }

                assignment.MarksheetFileName = fileName;

                db.Assignments.Add(assignment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "Title", assignment.CourseID);
            ViewBag.InstructorID = new SelectList(db.Instructors, "ID", "Name", assignment.InstructorID);
            return View(assignment);
        }

        // GET: Assignments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Assignment assignment = db.Assignments.Find(id);
            if (assignment == null)
            {
                return HttpNotFound();
            }
            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "Title", assignment.CourseID);
            ViewBag.InstructorID = new SelectList(db.Instructors, "ID", "Name", assignment.InstructorID);
            return View(assignment);
        }

        // POST: Assignments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AssignmentID,InstructorID,CourseID")] Assignment assignment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(assignment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "Title", assignment.CourseID);
            ViewBag.InstructorID = new SelectList(db.Instructors, "ID", "Name", assignment.InstructorID);
            return View(assignment);
        }

        // GET: Assignments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Assignment assignment = db.Assignments.Find(id);
            if (assignment == null)
            {
                return HttpNotFound();
            }
            return View(assignment);
        }

        // POST: Assignments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Assignment assignment = db.Assignments.Find(id);
            db.Assignments.Remove(assignment);
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

        private bool generateInitialSpreadSheet(int courseID, string path)
        {
            //if (courseID == null)
            //{
            //    return null;
            //}

            Course course = db.Courses.Find(courseID);
            if (course == null)
            {
                return false;
            }

            var enrollments = course.Enrollments;
            

            if (enrollments == null)
            {
                return false;
            }

            List<string[]> studentList = new List<string[]>();

            studentList.Add(new string[] { "Registration No.", "Exam Roll", "Name" });
            foreach (var item in enrollments)
            {
                studentList.Add(new string[] 
                {
                    item.Student.RegistrationNumber,
                    item.Student.ExamRoll.ToString(),
                    item.Student.Name
                });
            }

            //RedirectToAction("Save", studentList);
            //string path = Server.MapPath("~/Marksheets/" + assignments.MarksheetFileName);
            using (StreamWriter file = System.IO.File.CreateText(path))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, studentList);
            }

            return true;
        }
    }
}
