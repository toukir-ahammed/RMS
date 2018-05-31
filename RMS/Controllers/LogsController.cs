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
    [Authorize(Roles = "Admin")]
    public class LogsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Logs
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            ViewBag.CourseSortParm = sortOrder == "Course" ? "course_desc" : "Course";
            ViewBag.DeptSortParm = sortOrder == "Dept" ? "dept_desc" : "Dept";
            ViewBag.SemSortParm = sortOrder == "Sem" ? "sem_desc" : "Sem";



            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var logs = db.Logs.Include(i => i.Assignment);

            if (!String.IsNullOrEmpty(searchString))
            {
                logs = logs.Where(s => s.Assignment.Instructor.Name.Contains(searchString)
                                       || s.DateTime.ToString().Contains(searchString)
                                       || s.LogMessage.Contains(searchString)
                                       || s.Assignment.Department.Name.Contains(searchString)
                                       || s.Assignment.Course.Title.Contains(searchString)
                                        || s.Assignment.Semester.ToString().Contains(searchString)
                                       );
            }


            switch (sortOrder)
            {
                case "name_desc":
                    logs = logs.OrderByDescending(s => s.Assignment.Instructor.Name);
                    break;
                case "Date":
                    logs = logs.OrderBy(s => s.DateTime.ToString());
                    break;
                case "date_desc":
                    logs = logs.OrderByDescending(s => s.DateTime.ToString());
                    break;
                case "Course":
                    logs = logs.OrderBy(s => s.Assignment.Course.Title.ToString());
                    break;
                case "course_desc":
                    logs = logs.OrderByDescending(s => s.Assignment.Course.Title.ToString());
                    break;
                case "Sem":
                    logs = logs.OrderBy(s => s.Assignment.Semester.ToString());
                    break;
                case "sem_desc":
                    logs = logs.OrderByDescending(s => s.Assignment.Semester.ToString());
                    break;
                case "Dept":
                    logs = logs.OrderBy(s => s.Assignment.Department.Name.ToString());
                    break;
                case "dept_desc":
                    logs = logs.OrderByDescending(s => s.Assignment.Department.Name.ToString());
                    break;
                default:
                    logs = logs.OrderBy(s => s.DateTime.ToString());
                    break;
            }

            int pageSize = 20;
            int pageNumber = (page ?? 1);
            return View(logs.ToPagedList(pageNumber, pageSize));

        }
    }
}
