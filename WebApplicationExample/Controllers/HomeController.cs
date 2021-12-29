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
        private BusinessLogicPassThru _logic = new BusinessLogicPassThru();
        public HomeController()
        {
            //NEED FIX  
             _logic = new BusinessLogicPassThru();
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
            //MediaCarouselModel _model = new MediaCarouselModel();
            //BusinessLogicPassThru businessLogicPassThru = new BusinessLogicPassThru();


            //List<MediaDTO> listOfMedia = businessLogicPassThru.GetTopThreeMedia();
            
            IndexViewModel _model = new IndexViewModel();
            var closed = _logic.GetWeekDaysOpen();
            _model.DayHours = closed;

            List<MediaDTO> listOfMedia = _logic.GetTopThreeMedia();
            foreach (var mediaDTO in listOfMedia)
            {
                var m = new MediaModel()
                {
                    Title = mediaDTO.Title,
                    ImageName = mediaDTO.ImageName,
                    Description = mediaDTO.Description,
                    Publisher = mediaDTO.PublisherName,
                    Genre = mediaDTO.GenreName
                };
                _model.CarouselMedia.Add(m);
            }
            return View(_model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }


        [HttpGet]
        public ActionResult Contact()
        {
            ContactModel _model = new ContactModel();
            _model.Message = "";
            return View(_model);
        }

        [HttpPost]
        public ActionResult Contact(ContactModel inModel)
        {

            if (ModelState.IsValid)
            {

                ContactModel _model = new ContactModel();
                //TODO: take info to database
                // connection string coming out of the web.config
                BusinessLogicPassThru businessLogicPassThru = new BusinessLogicPassThru(System.Configuration.ConfigurationManager.
                ConnectionStrings["dbconnection"].ConnectionString);

                //RegisterBLL registerBLL = new RegisterBLL();

                // pass a LoginBLL object because it contains the connection string and that will be need in business layer
                // to connect to database
                try
                {
                    int go = businessLogicPassThru.CreateContactRequest(Mapper.ContactModelToContactDTO(inModel));
                    if (go >= 0) //if the id of the contactrequest is 0 or greater we know that the data was added 
                    {
                        inModel.Message = "You have submitted the form. Thank you!";

                    }
                    else
                    {
                        throw new Exception("Something went wrong");
                    }
                    
                }
                catch (Exception ex)
                {
                    inModel.Message = "";
                    throw;

                }
                return View(inModel);
                // 
                //UserDTO _profile = registerBLL.Register(Mapper.GlobalLoginModelToUserDTO(inModel), businessLogicPassThru);

                // error message coming all the way up the stack from database or business layer
                //inModel.RegisterModel.Message = _profile.ErrorMessage;


            }
            else
            {
                inModel.Message = "";
                return View(inModel);

            }
        }

        public ActionResult FAQ()
        {
            ViewBag.Message = "FAQ Page";
            return View();
        }

        [HttpGet]
        public ActionResult Support()
        {
            SupportModel _model = new SupportModel();
            _model.Message = "";
            return View(_model);
        }

        [HttpPost]
        public ActionResult Support(SupportModel inModel)
        {
            if (ModelState.IsValid)
            {

                SupportModel _model = new SupportModel();
                //TODO: take info to database

                BusinessLogicPassThru businessLogicPassThru = new BusinessLogicPassThru(System.Configuration.ConfigurationManager.
                ConnectionStrings["dbconnection"].ConnectionString);

                //RegisterBLL registerBLL = new RegisterBLL();

                // pass a LoginBLL object because it contains the connection string and that will be need in business layer
                // to connect to database
                try
                {
                    int go = businessLogicPassThru.CreateSupportRequest(Mapper.SupportModelToSupportDTO(inModel));
                    if (go >= 0) //if the id of the contactrequest is 0 or greater we know that the data was added 
                    {
                        inModel.Message = "You have submitted the form. Thank you!";

                    }
                    else
                    {
                        throw new Exception("Something went wrong");
                    }

                }
                catch (Exception ex)
                {
                    inModel.Message = "";
                    throw;

                }
                return View(inModel);
            }
            else
            {
                inModel.Message = "";
                return View(inModel);

            }
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
                    // save profile into in session variable
                    System.Web.HttpContext.Current.Session["Profile"] = _profile;
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