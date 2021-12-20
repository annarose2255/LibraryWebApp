using LibraryCommon.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplicationExample.Controllers
{
    public class SystemController : Controller
    {
        // GET: System
        public ActionResult Dashboard()
        {

            UserDTO _profile  = (UserDTO)System.Web.HttpContext.Current.Session["Profile"];
            return View();
        }
    }
}