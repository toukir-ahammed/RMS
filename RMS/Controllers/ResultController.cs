using RMS.Models;
using RMSDataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace RMS.Controllers
{
    [AllowAnonymous]
    public class ResultController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Result
        public ActionResult Index()
        {
            //return View();
            return RedirectToAction("CheckResult");
        }

        // GET: /Result/CheckResult
        [AllowAnonymous]
        public ActionResult CheckResult()
        {
            //ViewBag.ReturnUrl = returnUrl;
            //ViewBag.DepartmentID = new SelectList(db.Departments, "DepartmentID", "Name");
            return View();
        }

        //
        // POST: Result/CheckResult
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult CheckResult(ResultViewModel model, string returnUrl)
        {
            //ViewBag.DepartmentID = new SelectList(db.Departments, "DepartmentID", "Name");
            if (!ModelState.IsValid)
            {
                return View(model);
            }                     
            
            var student =  db.Students
                .Where(c => (c.RegistrationNumber == model.RegistrationNumber))
                .FirstOrDefault();
            

            if(student == null)
            {
                ModelState.AddModelError("", "Student Not Found");
                return View(model);
                //return HttpNotFound();
            }

            var resultPublications = db.ResultPublications
                .Where(r => r.DepartmentID == student.DepartmentId
                && r.CalenderYear == model.CalenderYear
                && r.Semester == model.Semester).FirstOrDefault();

            if(resultPublications == null)
            {
                ModelState.AddModelError("", "Result is not published yet");
                return View(model);
            }

            var publicationDate = resultPublications.PublicationDate;
            var currentDate = DateTime.Now;


            if(DateTime.Compare(publicationDate,currentDate) > 0 )
            {
                ModelState.AddModelError("", "Result will be published at " + publicationDate.ToString("MMMM dd, yyyy"));
                return View(model);
            }

            var viewmodel = new MarkSheetViewModel();

            viewmodel.Student = student;

            var enrollments = db.Enrollments
                .Where(e => e.StudentId == student.ID && e.Semester == model.Semester).ToList();
            

            if(!enrollments.Any())
            {
                ModelState.AddModelError("", "Result Not Found");
                return View(model);
                //return HttpNotFound();
            }

            viewmodel.Enrollments = enrollments;

            double currentSemesterTotalCredits = 0.0;
            double currentSemesterObtainedCredits = 0.0;

            bool promoted = true;
            foreach (var enrollment in enrollments )
            {
                var credit = enrollment.Course.Credits;
                currentSemesterTotalCredits += credit;
                var gp = enrollment.GradePoint;

                if (gp < student.Department.LeastGPToPass || gp == 0.0)
                {
                    promoted = false;
                }

                currentSemesterObtainedCredits += gp*credit;
            }

            

            double TotalCredits = 0.0;
            double ObtainedCredits = 0.0;

            var allEnrollments = student.Enrollments;

            foreach (var enrollment in allEnrollments)
            {
                var credit = enrollment.Course.Credits;
                TotalCredits += credit;
                ObtainedCredits += enrollment.GradePoint*credit;
            }

            if (ObtainedCredits / TotalCredits < student.Department.LeastCGPAToPass)
                viewmodel.Promoted = false;

            viewmodel.Semester = model.Semester;
            viewmodel.Promoted = promoted;
            viewmodel.CurrentSemesterTotalCredits = currentSemesterTotalCredits;
            viewmodel.CurrentSemesterObtainedCredits = currentSemesterObtainedCredits;
            viewmodel.TotalCredits = TotalCredits;
            viewmodel.TotalObtainedCredits = ObtainedCredits;

            GeneratePdf(viewmodel);

            return View("Transcript", viewmodel);
        }

        public ActionResult Download(string file)
        {
            if (!System.IO.File.Exists(file))
            {
                return HttpNotFound();
            }

            var fileBytes = System.IO.File.ReadAllBytes(file);
            var response = new FileContentResult(fileBytes, "application/octet-stream")
            {
                FileDownloadName = "loremIpsum.pdf"
            };
            return response;
        }

        private void GeneratePdf(MarkSheetViewModel viewmodel)
        {
            List<string[]> pdfData = new List<string[]>();

            foreach(var enrollment in viewmodel.Enrollments)
            {
                pdfData.Add(new string[]
                { enrollment.CourseID.ToString(), enrollment.Course.Title,
                enrollment.Course.Credits.ToString(), enrollment.Grade, enrollment.GradePoint.ToString() 
                });
            }
            String registrationNumber = viewmodel.Student.RegistrationNumber;
            String studentname = viewmodel.Student.Name;
            String examName = viewmodel.Student.ExamRoll.ToString();
            String semester = viewmodel.Semester.ToString();
            //new PDFGenerator(pdfData.ToArray(),registrationNumber,examName,studentname,semester);
            new PDFGenerator(pdfData.ToArray(), viewmodel.Student);




        }

        //public ActionResult GenerateMarksheetIndex()
        //{

        //    return View();
        //}

        //public ActionResult GenerateMarksheet()
        //{
        //    var model = new MarkSheetViewModel();
        //    model.Students = db.Students
        //        .Where(s => s.Semester == 0);
        //    model.Courses = db.Courses;

        //    model.Enrollments = db.Enrollments;



        //    return View(model);
        //}
    }
}