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
            MediaCarouselModel _model = new MediaCarouselModel();
            BusinessLogicPassThru businessLogicPassThru = new BusinessLogicPassThru();


            List<MediaDTO> listOfMedia = businessLogicPassThru.GetTopThreeMedia();
            _model.Media1.Img = listOfMedia[2].ImageName;
            _model.Media1.Title = listOfMedia[2].Title;
            _model.Media1.Description = listOfMedia[2].Description;
            _model.Media2.Img = listOfMedia[1].ImageName;
            _model.Media2.Title = listOfMedia[1].Title;
            _model.Media2.Description = listOfMedia[1].Description;
            _model.Media3.Img = listOfMedia[0].ImageName;
            _model.Media3.Title = listOfMedia[0].Title;
            _model.Media3.Description = listOfMedia[0].Description;
            return View(_model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ContactModel _model = new ContactModel();
            _model.Message = "";
            return View(_model);
        }

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

            UserModel _model = new UserModel();
            return View(_model);
        }


        [HttpPost]
        public ActionResult Login(UserModel inModel)
        {

            if (ModelState.IsValid)
            {
                // TODO: add code there
                BusinessLogicPassThru businessLogicPassThru = new BusinessLogicPassThru();
                List<UserDTO> _users = businessLogicPassThru.GetUsersData();

                UserModel _model = new UserModel();

                return View(inModel);

            }
            else 
            {
                return View(inModel);
            }

           
        }
        //TODO: add methods for the index of reaching the database, (and possible need for media DTO to hold that info) 
        //TODO: and change database script for media table to reflect info now needed 9img and description)- need join/view for getting author for media
        
    }
}