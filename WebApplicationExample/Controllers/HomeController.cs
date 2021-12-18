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

        public ActionResult Search(string query)
        {
            ViewBag.Message = query;
            return View();
        }



        [HttpGet]
        public ActionResult Login()
        {

            LoginModel _model = new LoginModel { ErrorMessage = "test"};
            return View(_model);
        }


        [HttpPost]
        public ActionResult Login(LoginModel inModel)
        {

            // use case # 1, username and password match, store profile object and send them to dashboard

            // use case # 2, username and/or password not not match, no profile stored and give them an error message 

            if (ModelState.IsValid)
            {
                // TODO: add code there
                BusinessLogicPassThru businessLogicPassThru = new BusinessLogicPassThru(System.Configuration.ConfigurationManager.
                ConnectionStrings["dbconnection"].ConnectionString);

                List<UserDTO> _users = businessLogicPassThru.GetUsersData();

                LoginModel _model = new LoginModel();

                inModel.ErrorMessage = "Something went wrong beside client validation";
                return View(inModel);

                //return RedirectToAction("DashBoard", "System");

            }
            else 
            {
               
                return View(inModel);
            }

           
        }
        
    }
}