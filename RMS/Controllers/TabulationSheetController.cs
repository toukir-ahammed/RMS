using RMS.Models;
using RMSDataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RMS.Controllers
{
    public class TabulationSheetController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: TabulationSheet
        public ActionResult Index()
        {
            ViewBag.DepartmentID = new SelectList(db.Departments, "DepartmentID", "Name");
            return View();
        }



        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult TabulationSheet(TabulationSheetViewModel model, string returnUrl)
        {
            ViewBag.DepartmentID = new SelectList(db.Departments, "DepartmentID", "Name");
            //var enrollments = db.Enrollments.
            //    Where(e => e.Semester == model.Semester && e.CalenderYear == model.CalenderYear);

            //var x = db.Students
            //    .Where(s => s.Enrollments
            //    .Select(i => i.Semester == model.Semester && i.CalenderYear == model.CalenderYear).Single());

            //var students = db.Students
            //    .Where(s => s.Semester == model.Semester);
            //return View(students.ToList());

            var enrollments = db.Enrollments
                .Where(e => e.Student.DepartmentId == model.DepartmentID
                && e.Semester == model.Semester
                && e.CalenderYear == model.CalenderYear).ToList();

            HashSet<Student> students = new HashSet<Student>();

            foreach(var enrollment in enrollments)
            {
                students.Add(enrollment.Student);
            }

            return View(students);
        }

    }
}