using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RMS.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            //TODO
            //if(User.Identity.IsAuthenticated)
            //{
            //    if(User.IsInRole("Admin"))
            //    {
            //        return RedirectToAction("Index", "Departments");
            //        //RedirectToActionPermanent("Index", "Departments");
            //    }
            //    else if(User.IsInRole("Instructor"))
            //    {
            //        return RedirectToAction("Index", "Instructors");
            //        //RedirectToActionPermanent("Index", "Instructors");
            //    }
            //}

            return View();
        }

        //Uncomment to add About and Contact

        //public ActionResult About()
        //{
        //    ViewBag.Message = "Your application description page.";

        //    return View();
        //}

        //[Authorize]
        //public ActionResult Contact()
        //{
        //    ViewBag.Message = "Your contact page.";

        //    return View();
        //}
    }
}