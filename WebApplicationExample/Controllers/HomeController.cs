﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LibraryBusinessLogicLayer;
using LibraryCommon;
using LibraryWebApp.Models;
using WebApplicationExample.Models;

namespace LibraryWebApp
{
    public class HomeController : Controller
    {
        private BusinessLogicPassThru _logic;
        public HomeController()
        {
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