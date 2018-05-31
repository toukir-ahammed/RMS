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

namespace RMS.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ResultPublicationsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ResultPublications
        public ActionResult Index()
        {
            var resultPublications = db.ResultPublications.Include(r => r.Department);
            return View(resultPublications.ToList());
        }

        // GET: ResultPublications/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ResultPublication resultPublication = db.ResultPublications.Find(id);
            if (resultPublication == null)
            {
                return HttpNotFound();
            }
            return View(resultPublication);
        }

        // GET: ResultPublications/Create
        public ActionResult Create()
        {
            ViewBag.DepartmentID = new SelectList(db.Departments, "DepartmentID", "Name");
            return View();
        }

        // POST: ResultPublications/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,DepartmentID,Semester,CalenderYear,PublicationDate")] ResultPublication resultPublication)
        {
            ViewBag.DepartmentID = new SelectList(db.Departments, "DepartmentID", "Name", resultPublication.DepartmentID);

            if (ModelState.IsValid)
            {
                var assignments = db.Assignments
                    .Where(e => e.DepartmentID == resultPublication.DepartmentID
                    && e.Semester == resultPublication.Semester
                    && e.CalenderYear == resultPublication.CalenderYear).ToList();
                if (!assignments.Any())
                {
                    ModelState.AddModelError("", "Can not calculate the result. No enrollments found");
                    return View(resultPublication);
                }

                
                HashSet<string> CENotSubmittedCourses = new HashSet<string>();
                HashSet<string> FinalSubmittedCourses = new HashSet<string>();
                foreach (var assignment in assignments)
                {
                    if (!assignment.CESubmitted)
                    {
                        CENotSubmittedCourses.Add(assignment.Course.Title);
                        //ModelState.AddModelError("", "Marks of " + enrollment.Course.Title + " is not submitted yet");

                    }
                    if (!assignment.FinalSubmitted)
                    {
                        FinalSubmittedCourses.Add(assignment.Course.Title);
                        //ModelState.AddModelError("", "Marks of " + enrollment.Course.Title + " is not submitted yet");

                    }
                }

                if (CENotSubmittedCourses.Count != 0)
                {
                    ModelState.AddModelError("", "The following courses' continuos evalution marks are not submitted yet:\n");
                    foreach (var course in CENotSubmittedCourses)
                    {
                        ModelState.AddModelError("", course + "\n");
                    }
                    
                }
                if (FinalSubmittedCourses.Count != 0)
                {
                    ModelState.AddModelError("", "The following courses' final marks are not submitted yet:\n");
                    foreach (var course in FinalSubmittedCourses)
                    {
                        ModelState.AddModelError("", course + "\n");
                    }
                }
                if(CENotSubmittedCourses.Count != 0 || FinalSubmittedCourses.Count != 0)
                {
                    return View(resultPublication);
                }
                

                db.ResultPublications.Add(resultPublication);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(resultPublication);
        }

        // GET: ResultPublications/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ResultPublication resultPublication = db.ResultPublications.Find(id);
            if (resultPublication == null)
            {
                return HttpNotFound();
            }
            ViewBag.DepartmentID = new SelectList(db.Departments, "DepartmentID", "Name", resultPublication.DepartmentID);
            return View(resultPublication);
        }

        // POST: ResultPublications/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,DepartmentID,Semester,CalenderYear,PublicationDate")] ResultPublication resultPublication)
        {
            if (ModelState.IsValid)
            {
                db.Entry(resultPublication).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DepartmentID = new SelectList(db.Departments, "DepartmentID", "Name", resultPublication.DepartmentID);
            return View(resultPublication);
        }

        // GET: ResultPublications/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ResultPublication resultPublication = db.ResultPublications.Find(id);
            if (resultPublication == null)
            {
                return HttpNotFound();
            }
            return View(resultPublication);
        }

        // POST: ResultPublications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ResultPublication resultPublication = db.ResultPublications.Find(id);
            db.ResultPublications.Remove(resultPublication);
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
