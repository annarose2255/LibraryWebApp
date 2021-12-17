using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LibraryBusinessLogicLayer;
using LibraryCommon;
using LibraryWebApp.Models;

namespace LibraryWebApp
{
    public class HomeController : Controller
    {
        /// <summary>
        /// method to redirect the default to be index, so we can have url for index appear
        /// </summary>
        /// <returns></returns>
        public ActionResult Root()
        {
            // RedirectToRoute(new { name = "Home", url = "Home/Home" });  // might work with work on it
            return RedirectToAction("Index"); // go to the index page
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Example() // need view that corresponds 
        {
            // example for getting to 
            BusinessLogicPassThru businessLogicPassThru = new BusinessLogicPassThru();
            List<UserDTO> _users = businessLogicPassThru.GetUsersData();

            UserModel _model = new UserModel();

            _model.FirstName = "Joe";
            _model.LastName = "Smith";
            _model.RoleName = "Admin";
            return View(_model); //way to give view the model to outprint data
        }

        public ActionResult FAQ()
        {
            ViewBag.Message = "FAQ Page";
            return View();
        }
        public ActionResult Support()
        {
            ViewBag.Message = "Support Page";
            return View();
        }
    }
}