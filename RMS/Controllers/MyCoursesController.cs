﻿using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Newtonsoft.Json;
using RMS.Models;
using RMSDataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Data.Entity;

namespace RMS.Controllers
{
    public class MyCoursesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: MyCourses
        public ActionResult Index()
        {
            //var userId = User.Identity.GetUserId();
            //var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            //var currentUser = manager.FindById(userId);

            //var instructors = db.Instructors
            //    .Include(i => i.) db.Instructors.Include(i => i.C)
            //    .OrderBy(i => i.Name);

            var userID = User.Identity.GetUserId();
            var currentUser = db.Users.Find(userID);

            var instructorId = currentUser.Instructor.ID;

            var instructor = db.Instructors.Find(instructorId);
            if (instructor == null)
            {
                return HttpNotFound();
            }

            //var courses = instructor.Courses;
            
            //var courses = db.Instructors
            //    .Where(i => i.ID == instructorId).Single().Courses;

            //if(courses == null)
            //{
            //    return HttpNotFound();
            //}

            //var assignments = db.Instructors
            //    .Where(x => x.ID == instructorId)
            //    .Single().Assignments;

            var assignments = instructor.Assignments;

            if (assignments == null)
            {
                return HttpNotFound();
            }

            return View(assignments);
        }

        // GET: MyCourses/Details/5
        public ActionResult Details(int? courseID)
        {
            if (courseID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            Course course = db.Courses.Find(courseID);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }


        public ActionResult Load(int? assignmentID)
        {
            //var jsonData = new[]
            //         {
            //             new[] {" ", "Kia", "Nissan",
            //             "Toyota", "Honda", "Mazda", "Ford"},
            //             new[] {"2012", "10", "11",
            //             "12", "13", "15", "16"},
            //             new[] {"2013", "10", "11",
            //             "12", "13", "15", "16"},
            //             new[] {"2014", "10", "11",
            //             "12", "13", "15", "16"},
            //             new[] {"2015", "10", "11",
            //             "12", "13", "15", "16"},
            //             new[] {"2016", "10", "11",
            //             "12", "13", "15", "16"}
            //        };

            if (assignmentID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //Course course = db.Courses.Find(courseID);
            //if (course == null)
            //{
            //    return HttpNotFound();
            //}

            //var assignments = course.Assignments.FirstOrDefault();
            var assignment = db.Assignments.Find(assignmentID);

            if (assignment == null)
            {
                return HttpNotFound();
            }

            List<string[]> jsonData;

            string path = Server.MapPath("~/MarkSheets/");
            path = Path.Combine(path, assignment.MarksheetFileName);

            using (StreamReader file = System.IO.File.OpenText(path))
            {
                JsonSerializer serializer = new JsonSerializer();
                jsonData = (List<string[]>)serializer.Deserialize(file, typeof(List<string[]>));
            }

            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Save(List<string[]> dataListFromTable, int assignmentID)
        {

            //System.IO.File.WriteAllText(@"c:\movie.json", JsonConvert.SerializeObject(dataListFromTable.ToArray()));

            // serialize JSON directly to a file

            //if (courseID == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}

            //TO DO Refactor
            //Course course = db.Courses.Find(courseID);
            //if (course == null)
            //{
            //    return HttpNotFound();
            //}

            var assignment = db.Assignments.Find(assignmentID);

            if (assignment == null)
            {
                return HttpNotFound();
            }

            string path = Server.MapPath("~/MarkSheets/");
            path = Path.Combine(path, assignment.MarksheetFileName);

            using (StreamWriter file = System.IO.File.CreateText(path))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, dataListFromTable);
            }

            //_data.Add(new data()
            //{
            //    Id = 1,
            //    SSN = 2,
            //    Message = "A Message"
            //});

            //string json = JsonConvert.SerializeObject(dataListFromTable.ToArray());

            ////write string to file
            //System.IO.File.WriteAllText(@"F:\path.txt", json);


            var dataListTable = dataListFromTable;
            return Json("Response, Data Received Successfully");

        }
        public ActionResult MarkSheet(int? assignmentID)
        {
            if (assignmentID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //Course course = db.Courses.Find(courseID);
            //if (course == null)
            //{
            //    return HttpNotFound();
            //}

            //var enrollments = course.Enrollments;
            //var assignments = course.Assignments.FirstOrDefault();


            ////Alternative
            ////var enrollments = db.Courses
            ////    .Where(x => x.CourseID == courseID)
            ////    .Single().Enrollments;
            //if (enrollments == null || assignments == null)
            //{
            //    return HttpNotFound();
            //}

            //List<string[]> studentList = new List<string[]>();

            //foreach(var item in enrollments)
            //{
            //    studentList.Add(new string[] { item.Student.RegistrationNumber, item.Student.Name, "", "" });
            //}

            ////RedirectToAction("Save", studentList);
            //string path = Server.MapPath("~/Marksheets/" + assignments.MarksheetFileName);
            //using (StreamWriter file = System.IO.File.CreateText(path))
            //{
            //    JsonSerializer serializer = new JsonSerializer();
            //    serializer.Serialize(file, studentList);
            //}

            ////ViewBag.CourseName = course.Title;

            ////return View(enrollments);

            ViewBag.assignmentID = assignmentID;
            return View();
        }



        // GET: MyCourses/StudentList
        public ActionResult StudentList(int? courseID)
        {
            if(courseID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Course course = db.Courses.Find(courseID);
            if(course == null)
            {
                return HttpNotFound();
            }

            var enrollments = course.Enrollments;


            //Alternative
            //var enrollments = db.Courses
            //    .Where(x => x.CourseID == courseID)
            //    .Single().Enrollments;
            if (enrollments == null)
            {
                return HttpNotFound();
            }
            ViewBag.CourseName = course.Title;

            return View(enrollments);
            
        }

        public ActionResult Submit(int? assignmentID)
        {
            if(assignmentID==null)
            {
                return HttpNotFound();
            }

            //var userID = User.Identity.GetUserId();
            //var user = db.Users.Find(userID);
            //var assignments = user.Instructor.Assignments
            //    .Where(i => i.CourseID == courseID).FirstOrDefault();

            var assignment = db.Assignments.Find(assignmentID);

            if(assignment==null)
            {
                return HttpNotFound();
            }

            List<string[]> jsonData;

            string path = Server.MapPath("~/MarkSheets/");
            path = Path.Combine(path, assignment.MarksheetFileName);

            using (StreamReader file = System.IO.File.OpenText(path))
            {
                JsonSerializer serializer = new JsonSerializer();
                jsonData = (List<string[]>)serializer.Deserialize(file, typeof(List<string[]>));
            }

            int CEColumn=0, FinalColumn=0;

            for(int i=0; i<jsonData[0].Length; i++)
            {
                if(jsonData[0][i].ToLower()=="cetotal")
                {
                    CEColumn = i;
                }

                if(jsonData[0][i].ToLower() == "finaltotal")
                {
                    FinalColumn = i;
                }
            }

            Course course = db.Courses.Find(assignment.CourseID);

            if (course == null)
            {
                return HttpNotFound();
            }

            var enrollments = course.Enrollments;


            //Alternative
            //var enrollments = db.Courses
            //    .Where(x => x.CourseID == courseID)
            //    .Single().Enrollments;
            if (enrollments == null)
            {
                return HttpNotFound();
            }

            foreach(var item in enrollments)
            {
                
            }

            for(int i=1; i<jsonData.Count; i++)
            {
                //if (jsonData[i][0] == null || jsonData[i][1] == "") continue;

                var regNo = jsonData[i][0];
                var ceMark = Convert.ToDouble(jsonData[i][CEColumn]);
                var finalMark = Convert.ToDouble(jsonData[i][FinalColumn]);

                var student = db.Students
                    .Where(s => s.RegistrationNumber == regNo).FirstOrDefault();

                if (student == null) continue;
                var enrollment = db.Enrollments
                    .Where(s => (s.StudentId == student.ID && s.CourseID == assignment.CourseID))
                    .FirstOrDefault();

                if (enrollment == null) continue;

                enrollment.CEMark = ceMark;
                enrollment.FinalMark = finalMark;
                db.Entry(enrollment).State = EntityState.Modified;
                db.SaveChanges();


            }

            //foreach(var row in jsonData)
            //{
            //    var student = db.Students
            //        .Where(i => i.RegistrationNumber == row[0]).FirstOrDefault();

            //    if (student == null) continue;
            //    var enrollment = db.Enrollments
            //        .Where(i => (i.StudentId == student.ID && i.CourseID == assignment.CourseID))
            //        .FirstOrDefault();

            //    if (enrollment == null) continue;
                
            //    enrollment.CEMark = Convert.ToDouble(row[CEColumn]);
            //    enrollment.FinalMark = Convert.ToDouble(row[FinalColumn]);
            //    db.Entry(enrollment).State = EntityState.Modified;
            //    db.SaveChanges();
            //}




            return View();
        }
    }
}