using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LibraryBusinessLogicLayer;
using LibraryCommon.DTO;
using LibraryWebApp.Models;
using LibraryCommon;
using WebApplicationExample.Utility;
using WebApplicationExample.Models;

namespace LibraryWebApp
{
    public class HomeController : Controller
    {
        private BusinessLogicPassThru _logic;
        public HomeController()
        {
            //NEED FIX  
            BusinessLogicPassThru _logic = new BusinessLogicPassThru();
        }
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
            IndexViewModel _model = new IndexViewModel();
            MediaCarouselModel _carousel = new MediaCarouselModel();
            var closed = _logic.GetWeekDaysOpen();
            _model.DayHours = closed;
            _carousel.Media1.Img = "img1.jpg";
            _model.CarouselMedia = _carousel;
            return View(_model);
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

            GlobalLoginModel _model = new GlobalLoginModel();
            _model.Message = "";
            return View(_model);
        }


        [HttpPost]
        public ActionResult Login(GlobalLoginModel inModel)
        {
         
            if (ModelState.IsValid)
            {
                // connection string coming out of the web.config
                BusinessLogicPassThru businessLogicPassThru = new BusinessLogicPassThru(System.Configuration.ConfigurationManager.
                ConnectionStrings["dbconnection"].ConnectionString);
               
                LoginBLL loginBLL = new LoginBLL();
                // pass a LoginBLL object because it contains the connection string and that will be need in business layer
                // to connect to database
                UserDTO _profile = loginBLL.Login(inModel.LoginModel.Username, inModel.LoginModel.Password, businessLogicPassThru);
                // error message coming all the way up the stack from database or business layer
                inModel.Message = _profile.ErrorMessage;

                if (string.IsNullOrEmpty(inModel.Message))
                {
                    // use case # 1, username and password match, store profile object and send them to dashboard
                    return RedirectToAction("Dashboard","System");
                    
                }
                else
                {
                    // use case # 2, username and/or password not not match, no profile stored and give them an error message 
                    return View(inModel);
                }
            }
            else 
            {
               // use case # 3, valiation on client failed, show error message in login.cshtml
                return View(inModel);
            }
           
        }
        [HttpPost]
        public ActionResult Register(GlobalLoginModel inModel)
        {
            if (ModelState.IsValid)
            {
                // connection string coming out of the web.config
                BusinessLogicPassThru businessLogicPassThru = new BusinessLogicPassThru(System.Configuration.ConfigurationManager.
                ConnectionStrings["dbconnection"].ConnectionString);

                RegisterBLL registerBLL = new RegisterBLL();
                // pass a LoginBLL object because it contains the connection string and that will be need in business layer
                // to connect to database
                // 

                // UserDTO _profile = registerBLL.Register(inModel.RegisterModel.Username, inModel.RegisterModel.FirstName, inModel.RegisterModel.LastName, inModel.RegisterModel.Password,5, businessLogicPassThru,inModel.RegisterModel.PrimaryEmail,inModel.RegisterModel.PrimaryPhone);
                
                UserDTO _profile = registerBLL.Register(Mapper.GlobalLoginModelToUserDTO(inModel), businessLogicPassThru);

                // error message coming all the way up the stack from database or business layer
                inModel.RegisterModel.Message = _profile.ErrorMessage;

                if (string.IsNullOrEmpty(inModel.RegisterModel.Message))
                {
                    // use case # 1, registration was successful, store profile object and send them to dashboard

                    // TODO: go to dashboard


                    // save profile into in session variable
                    System.Web.HttpContext.Current.Session["Profile"] = _profile;

                    return RedirectToAction("Dashboard", "System");
                    
                }
                else
                {
                    // use case # 2, registration was not successful, no profile stored and give them an error message 
                    return View("Login", inModel);

                }
            }
            else
            {
                // use case # 3, validation on client failed
                return View("Login", inModel);
               
            }
        }
        //TODO: add methods for the index of reaching the database, (and possible need for media DTO to hold that info) 
        //TODO: and change database script for media table to reflect info now needed 9img and description)- need join/view for getting author for media
        
    }
}