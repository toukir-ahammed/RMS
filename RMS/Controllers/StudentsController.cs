using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RMS.Models;
using RMSDataModel;
using PagedList;

namespace RMS.Controllers
{
    public class StudentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Students
        //public ActionResult Index()
        //{
        //    var students = db.Students.Include(s => s.Department);
        //    return View(students.ToList());
        //}

        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DepartmentSortParm = sortOrder == "Department" ? "dept_desc" : "Department";
            ViewBag.SemesterSortParm = sortOrder == "Semester" ? "sem_desc" : "Semester";
            ViewBag.SessionSortParm = sortOrder == "Session" ? "session_desc" : "Session";
            ViewBag.ClassRollSortParm = sortOrder == "ClassRoll" ? "class_roll_desc" : "ClassRoll";
            ViewBag.ExamRollSortParm = sortOrder == "ExamRoll" ? "exam_roll_desc" : "ExamRoll";
            ViewBag.RegNoSortParm = sortOrder == "RegNo" ? "reg_no_desc" : "RegNo";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var students = db.Students.Include(i => i.Department);

            if (!String.IsNullOrEmpty(searchString))
            {
                students = students.Where(s => s.Name.Contains(searchString)
                                       || s.Department.Name.Contains(searchString)
                                       || s.Semester.ToString().Contains(searchString)
                                       || s.Session.Contains(searchString)
                                       || s.ClassRoll.ToString().Contains(searchString)
                                       || s.ExamRoll.ToString().Contains(searchString)
                                       || s.RegistrationNumber.Contains(searchString)
                                       );
            }

            
            switch (sortOrder)
            {
                case "name_desc":
                    students = students.OrderByDescending(s => s.Name);
                    break;
                case "Department":
                    students = students.OrderBy(s => s.Department.Name);
                    break;
                case "dept_desc":
                    students = students.OrderByDescending(s => s.Department.Name);
                    break;
                case "Semester":
                    students = students.OrderBy(s => s.Semester);
                    break;
                case "sem_desc":
                    students = students.OrderByDescending(s => s.Semester);
                    break;
                case "Session":
                    students = students.OrderBy(s => s.Session);
                    break;
                case "session_desc":
                    students = students.OrderByDescending(s => s.Session);
                    break;
                case "ClassRoll":
                    students = students.OrderBy(s => s.ClassRoll);
                    break;
                case "class_roll_desc":
                    students = students.OrderByDescending(s => s.ClassRoll);
                    break;
                case "ExamRoll":
                    students = students.OrderBy(s => s.ExamRoll);
                    break;
                case "exam_roll_desc":
                    students = students.OrderByDescending(s => s.ExamRoll);
                    break;
                case "RegNo":
                    students = students.OrderBy(s => s.RegistrationNumber);
                    break;
                case "reg_no_desc":
                    students = students.OrderByDescending(s => s.RegistrationNumber);
                    break;
                default:
                    students = students.OrderBy(s => s.Name);
                    break;
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(students.ToPagedList(pageNumber, pageSize));

            //return View(students.ToList());
        }

        // GET: Students/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // GET: Students/Create
        public ActionResult Create()
        {
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentID", "Name");
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,RegistrationNumber,Session,ClassRoll,ExamRoll,Semester,DepartmentId")] Student student)
        {
            if (ModelState.IsValid)
            {
                db.Students.Add(student);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentID", "Name", student.DepartmentId);
            return View(student);
        }

        // GET: Students/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentID", "Name", student.DepartmentId);
            return View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,RegistrationNumber,Session,ClassRoll,ExamRoll,Semester,DepartmentId")] Student student)
        {
            if (ModelState.IsValid)
            {
                db.Entry(student).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentID", "Name", student.DepartmentId);
            return View(student);
        }

        // GET: Students/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Student student = db.Students.Find(id);
            db.Students.Remove(student);
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
