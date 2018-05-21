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
            ViewBag.DepartmentID = new SelectList(db.Departments, "DepartmentID", "Name");
            return View();
        }

        //
        // POST: Result/CheckResult
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult CheckResult(ResultViewModel model, string returnUrl)
        {
            ViewBag.DepartmentID = new SelectList(db.Departments, "DepartmentID", "Name");
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            
            
            var student =  db.Students
                .Where(c => (c.RegistrationNumber == model.RegistrationNumber))
                .FirstOrDefault();

            ViewBag.CGPA = 3.92;


            if(student == null)
            {
                return HttpNotFound();
            }

            return View("Result", student);
        }
    }
}