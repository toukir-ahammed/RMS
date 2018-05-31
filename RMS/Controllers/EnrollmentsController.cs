using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;
using RMS.Models;
using RMSDataModel;

namespace RMS.Controllers
{
    [Authorize(Roles = "Admin")]
    public class EnrollmentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Enrollments
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            
            ViewBag.CurrentSort = sortOrder;
            ViewBag.CourseSortParm = String.IsNullOrEmpty(sortOrder) ? "course_desc" : "";
            ViewBag.NameSortParm = sortOrder == "Name" ? "name_desc" : "Name";
            ViewBag.SemesterSortParm = sortOrder == "Semester" ? "sem_desc" : "Semester";
            ViewBag.YearSortParm = sortOrder == "Year" ? "year_desc" : "Year";
            ViewBag.CESortParm = sortOrder == "CE" ? "ce_desc" : "CE";
            ViewBag.FinalSortParm = sortOrder == "Final" ? "final_desc" : "Final";



            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var enrollments = db.Enrollments.Include(e => e.Course).Include(e => e.Student);

            if (!String.IsNullOrEmpty(searchString))
            {
                enrollments = enrollments.Where(s => s.Course.Title.Contains(searchString)
                                       || s.Student.Name.Contains(searchString)
                                       || s.Semester.ToString().Contains(searchString)
                                       || s.CalenderYear.ToString().Contains(searchString)
                                       || s.CEMark.ToString().Contains(searchString)
                                       || s.FinalMark.ToString().Contains(searchString)
                                       
                                       );
            }


            switch (sortOrder)
            {
                case "name_desc":
                    enrollments = enrollments.OrderByDescending(s => s.Student.Name);
                    break;
                case "Course":
                    enrollments = enrollments.OrderBy(s => s.Course.Title);
                    break;
                case "course_desc":
                    enrollments = enrollments.OrderByDescending(s => s.Course.Title);
                    break;
                case "Semester":
                    enrollments = enrollments.OrderBy(s => s.Semester);
                    break;
                case "sem_desc":
                    enrollments = enrollments.OrderByDescending(s => s.Semester);
                    break;
                case "Year":
                    enrollments = enrollments.OrderBy(s => s.CalenderYear.ToString());
                    break;
                case "year_desc":
                    enrollments = enrollments.OrderByDescending(s => s.CalenderYear.ToString());
                    break;
                case "CE":
                    enrollments = enrollments.OrderBy(s => s.CEMark);
                    break;
                case "ce_desc":
                    enrollments = enrollments.OrderByDescending(s => s.CEMark);
                    break;
                case "Final":
                    enrollments = enrollments.OrderBy(s => s.FinalMark);
                    break;
                case "final_roll_desc":
                    enrollments = enrollments.OrderByDescending(s => s.FinalMark);
                    break;
                
                default:
                    enrollments = enrollments.OrderBy(s => s.Course.Title);
                    break;
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(enrollments.ToPagedList(pageNumber, pageSize));
        }

        // GET: Enrollments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Enrollment enrollment = db.Enrollments.Find(id);
            if (enrollment == null)
            {
                return HttpNotFound();
            }
            return View(enrollment);
        }

        // GET: Enrollments/Create
        public ActionResult Create()
        {
            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "Title");
            //ViewBag.DepartmentID = new SelectList(db.Departments, "DepartmentID", "Name");
            ViewBag.StudentId = new SelectList(db.Students, "ID", "Name");
            return View();
        }

        // POST: Enrollments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EnrollmentId,CourseID,StudentId,Semester,CalenderYear")] Enrollment enrollment)
        {
            if (ModelState.IsValid)
            {
                db.Enrollments.Add(enrollment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "Title", enrollment.CourseID);
            
            ViewBag.StudentId = new SelectList(db.Students, "ID", "Name", enrollment.StudentId);
            return View(enrollment);
        }

        // GET: Enrollments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Enrollment enrollment = db.Enrollments.Find(id);
            if (enrollment == null)
            {
                return HttpNotFound();
            }
            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "Title", enrollment.CourseID);
            
            ViewBag.StudentId = new SelectList(db.Students, "ID", "Name", enrollment.StudentId);
            return View(enrollment);
        }

        // POST: Enrollments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EnrollmentId,CourseID,StudentId,Semester,CalenderYear")] Enrollment enrollment)
        {
            if (ModelState.IsValid)
            {
                var old = db.Enrollments.Find(enrollment.EnrollmentId);
                old.CourseID = enrollment.CourseID;
                old.StudentId = enrollment.StudentId;
                old.Semester = enrollment.Semester;
                old.Semester = enrollment.Semester;
                db.Entry(old).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "Title", enrollment.CourseID);
            
            ViewBag.StudentId = new SelectList(db.Students, "ID", "Name", enrollment.StudentId);
            return View(enrollment);
        }

        // GET: Enrollments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Enrollment enrollment = db.Enrollments.Find(id);
            if (enrollment == null)
            {
                return HttpNotFound();
            }
            return View(enrollment);
        }

        // POST: Enrollments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Enrollment enrollment = db.Enrollments.Find(id);
            db.Enrollments.Remove(enrollment);
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
