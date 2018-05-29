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
            var assignments = db.Assignments.Include(a => a.Course).Include(e => e.Department).Include(a => a.Instructor);
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
            ViewBag.DepartmentID = new SelectList(db.Departments, "DepartmentID", "Name");
            ViewBag.InstructorID = new SelectList(db.Instructors, "ID", "Name");
            return View();
        }

        // POST: Assignments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AssignmentID,InstructorID,CourseID,Semester,CalenderYear,CEDeadline,CETotal,FinalExamTotal,FinalDeadLine,DepartmentID")] Assignment assignment)
        {
            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "Title", assignment.CourseID);
            ViewBag.DepartmentID = new SelectList(db.Departments, "DepartmentID", "Name", assignment.DepartmentID);
            ViewBag.InstructorID = new SelectList(db.Instructors, "ID", "Name", assignment.InstructorID);

            if (ModelState.IsValid)
            {
                //string t = DateTime.Now.ToLongTimeString();
                string fileName = DateTime.Now.ToString("yyyyMMddHHmmss") + "_"
                    + assignment.CourseID + "_"
                    + assignment.InstructorID +
                    ".json";



                string path = Server.MapPath("~/MarkSheets/");
                path = Path.Combine(path, fileName);

                var enrollments = db.Enrollments
                .Where(e => e.Student.DepartmentId == assignment.DepartmentID
                && e.Semester == assignment.Semester
                && e.CalenderYear == assignment.CalenderYear
                && e.CourseID == assignment.CourseID).ToList();


                if (!enrollments.Any())
                {
                    ModelState.AddModelError("", "No enrollments found");
                    return View(assignment);
                }

                List<string[]> studentList = new List<string[]>();

                studentList.Add(new string[] 
                {
                    "Registration No.", "Exam Roll", "Name", "Attendance", "Class Test", "Midterm", "Lab",
                    "CE Total",
                    "Final Exam Total",
                    "Total(100)"


                });
                foreach (var item in enrollments)
                {
                    studentList.Add(new string[]
                    {
                        item.Student.RegistrationNumber,
                        item.Student.ExamRoll.ToString(),
                        item.Student.Name,
                        "","","","","","",""
                    });
                }

                
                using (StreamWriter file = System.IO.File.CreateText(path))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    serializer.Serialize(file, studentList);
                }

                //if (!generateInitialSpreadSheet(assignment, path))
                //{
                //    return HttpNotFound();
                //}

                assignment.MarksheetFileName = fileName;

                db.Assignments.Add(assignment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            //ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "Title", assignment.CourseID);
            //ViewBag.DepartmentID = new SelectList(db.Departments, "DepartmentID", "Name", assignment.DepartmentID);
            //ViewBag.InstructorID = new SelectList(db.Instructors, "ID", "Name", assignment.InstructorID);
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
            ViewBag.DepartmentID = new SelectList(db.Departments, "DepartmentID", "Name", assignment.DepartmentID);
            ViewBag.InstructorID = new SelectList(db.Instructors, "ID", "Name", assignment.InstructorID);
            return View(assignment);
        }

        // POST: Assignments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AssignmentID,InstructorID,CourseID,Semester,CalenderYear,CETotal,FinalExamTotal,CEDeadline,FinalDeadLine,DepartmentID")] Assignment assignment)
        {
            if (ModelState.IsValid)
            {
                var old = db.Assignments.Find(assignment.AssignmentID);
                old.InstructorID = assignment.InstructorID;
                old.CourseID = assignment.CourseID;
                old.Semester = assignment.Semester;
                old.CalenderYear = assignment.CalenderYear;
                old.CETotal = assignment.CETotal;
                old.FinalExamTotal = assignment.FinalExamTotal;
                old.CEDeadline = assignment.CEDeadline;
                old.FinalDeadLine = assignment.FinalDeadLine;
                old.DepartmentID = assignment.DepartmentID;

                //TODO Marksheet file name will be null after edit
                db.Entry(old).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "Title", assignment.CourseID);
            ViewBag.DepartmentID = new SelectList(db.Departments, "DepartmentID", "Name", assignment.DepartmentID);
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
    }
}
