using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services;

namespace RMS.Controllers
{
    [Authorize(Roles = "Admin")]
    public class HandsNotableController : Controller
    {

        //[WebMethod(EnableSession = true)]
        [HttpPost]
        public ActionResult Done(string data)
        {
            String a = data;
            return Json("success!!", JsonRequestBehavior.AllowGet);

        }

        // GET: HandsNotable
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddCar(List<string[]> dataListFromTable)
        {
            var dataListTable = dataListFromTable;
            return Json("Response, Data Received Successfully");
        }
        public JsonResult GetCar()
        {
            var jsonData = new[]
                         {
                         new[] {" ", "Kia", "Nissan",
                         "Toyota", "Honda", "Mazda", "Ford"},
                         new[] {"2012", "10", "11",
                         "12", "13", "15", "16"},
                         new[] {"2013", "10", "11",
                         "12", "13", "15", "16"},
                         new[] {"2014", "10", "11",
                         "12", "13", "15", "16"},
                         new[] {"2015", "10", "11",
                         "12", "13", "15", "16"},
                         new[] {"2016", "10", "11",
                         "12", "13", "15", "16"}
                    };

            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
    }
}