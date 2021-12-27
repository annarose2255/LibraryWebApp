using LibraryCommon.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataTables;
using LibraryWebApp.Models;
using LibraryBusinessLogicLayer;
using WebApplicationExample.Utility;
using LibraryWebApp.Utility;

namespace WebApplicationExample.Controllers
{
   
    public class SystemController : Controller
    {
        [HttpGet]
        public ActionResult Dashboard()
        {
          
            // connection string coming out of the web.config
            BusinessLogicPassThru businessLogicPassThru = new BusinessLogicPassThru(System.Configuration.ConfigurationManager.
            ConnectionStrings["dbconnection"].ConnectionString);

            List<UserDTO> _list = businessLogicPassThru.GetUsersData();

          
            UsersModel _model = new UsersModel(Mapper.ListOfUserDTOToListOfUserModel(_list));

            return View(_model);
        }

        //[AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        //public ActionResult Table()
        //{
        //    // connection string coming out of the web.config
        //    BusinessLogicPassThru businessLogicPassThru = 
        //        new BusinessLogicPassThru(System.Configuration.ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString);
        //    string _dbConnection = System.Configuration.ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
        //    string _dbType = "Sqlserver";

        //    //var settings = Properties.Settings.Default;
        //    var formData = HttpContext.Request.Form;

        //    using (var db = new Database(_dbType, _dbConnection))
        //    {
        //        var response = new Editor(db, "staff")
        //            .Model<LoginModel>()
        //            .Field(new Field("start_date")
        //                .Validator(Validation.DateFormat(
        //                    Format.DATE_ISO_8601,
        //                    new ValidationOpts { Message = "Please enter a date in the format yyyy-mm-dd" }
        //                ))
        //                .GetFormatter(Format.DateSqlToFormat(Format.DATE_ISO_8601))
        //                .SetFormatter(Format.DateFormatToSql(Format.DATE_ISO_8601))
        //            )
        //            .Process(formData)
        //            .Data();


        //        // TODO: add to db here

        //        return Json(response, JsonRequestBehavior.AllowGet);
        //    }
        //}


        #region Users

        [HttpGet]
        public ActionResult EditUser(int id)
        {

            //RoleListVM list = new RoleListVM(_logicRole.GetRolesPassThru());
            //ViewBag.Roles = new SelectList(list.ListOfRoleModel, "RoleId", "RoleName");

            if (ProfileChecker.IsLoggedIn() && ProfileChecker.IsAdmin())
            {

                // connection string coming out of the web.config
                BusinessLogicPassThru businessLogicPassThru = new BusinessLogicPassThru(System.Configuration.ConfigurationManager.
                ConnectionStrings["dbconnection"].ConnectionString);

                UserDTO _user = businessLogicPassThru.GetSingleUserData(id);
                return View(Mapper.UserDTOToUserModel(_user));
            }
            else 
            {
               return RedirectToAction("Login", "Home");
            }
        }

        [HttpPost]
        public ActionResult EditUser(UserModel inModel)
        {

            if (ProfileChecker.IsLoggedIn() && ProfileChecker.IsAdmin())
            {

                if (ModelState.IsValid)
                {

                    UserDTO _editThisUser = new UserDTO();

                    // connection string coming out of the web.config
                    BusinessLogicPassThru businessLogicPassThru = new BusinessLogicPassThru(System.Configuration.ConfigurationManager.
                    ConnectionStrings["dbconnection"].ConnectionString);
                    UserDTO _currentUser = (UserDTO)System.Web.HttpContext.Current.Session["Profile"]; // get the current user
                    UserDTO _user = businessLogicPassThru.UpdateUser(Mapper.UserModelToUserDTO(inModel, _currentUser.UserId));

                    return RedirectToAction("Dashboard", "System");
                }
                else
                {
                    return View(inModel);
                }
            }
            else 
            {
                return RedirectToAction("Login", "Home");
            }
        }


        [HttpPost]
        public ActionResult DeleteUser(int id)
         {
            if (ModelState.IsValid)
            {

                //_logicUser.CreateUserPassThru(toAdd);

                return RedirectToAction("Dashboard", "System");
            }
            else
            {
                return View();
            }
        }

        #endregion





    }

}