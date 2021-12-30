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
            if (ProfileChecker.IsLoggedIn())
            {
                // connection string coming out of the web.config
                BusinessLogicPassThru businessLogicPassThru = new BusinessLogicPassThru(System.Configuration.ConfigurationManager.
                ConnectionStrings["dbconnection"].ConnectionString);

                List<UserDTO> _list = businessLogicPassThru.GetUsersData();
                UsersModel _model = new UsersModel(Mapper.ListOfUserDTOToListOfUserModel(_list));

                return View(_model);
            }
            else 
            {
                return RedirectToAction("Index", "Home");
            }
          
          
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
            //if admin, pass list of roles
            if (ProfileChecker.IsLoggedIn() && ProfileChecker.IsAdmin())
            {

                // connection string coming out of the web.config
                BusinessLogicPassThru businessLogicPassThru = new BusinessLogicPassThru(System.Configuration.ConfigurationManager.
                ConnectionStrings["dbconnection"].ConnectionString);

                // get the roles and add to a bag
                List<RoleDTO> _rolesDTO = businessLogicPassThru.GetRoles();
                ViewBag.Roles = new SelectList(Mapper.ListOfRoleDTOToListOfRoles(_rolesDTO), "RoleId", "RoleName");

                UserDTO _user = businessLogicPassThru.GetSingleUserData(id);
                return View(Mapper.UserDTOToUserModel(_user));
            }
            //if logged in but does not have admin status, do not pass list of roles (for security)
            if (ProfileChecker.IsLoggedIn())
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

            if (ProfileChecker.IsLoggedIn() & ProfileChecker.IsAdmin())
            {
                //can update any user in db
                if (ModelState.IsValid)
                {
                    // connection string coming out of the web.config
                    BusinessLogicPassThru businessLogicPassThru = new BusinessLogicPassThru(System.Configuration.ConfigurationManager.
                    ConnectionStrings["dbconnection"].ConnectionString);
                    UserDTO _currentUser = (UserDTO)System.Web.HttpContext.Current.Session["Profile"]; // get the current user
                    UserDTO _user = businessLogicPassThru.UpdateUser(Mapper.UserModelToUserDTO(inModel, _currentUser.UserId));
                    System.Web.HttpContext.Current.Session["Profile"] = businessLogicPassThru.GetSingleUserData(_currentUser.UserId); //if inModel and currentUser have the same userId, profile session would need to reflect new user fields
                    return RedirectToAction("Dashboard", "System",inModel);
                }
                else
                {
                    return View(inModel);
                }
            }

            if (ProfileChecker.IsLoggedIn())
            {
                //can only update themselves in db
                if (ModelState.IsValid)
                {
                    // connection string coming out of the web.config
                    BusinessLogicPassThru businessLogicPassThru = new BusinessLogicPassThru(System.Configuration.ConfigurationManager.
                    ConnectionStrings["dbconnection"].ConnectionString);
                    UserDTO _currentUser = (UserDTO)System.Web.HttpContext.Current.Session["Profile"]; // get the current user
                    UserDTO _user = businessLogicPassThru.UpdateUser(Mapper.UserModelToUserDTO(inModel, _currentUser.UserId));
                    System.Web.HttpContext.Current.Session["Profile"] = _user; //update user fields
                    return RedirectToAction("Dashboard", "System");
                }
                else
                {
                    return View(inModel);
                }
            }
            else 
            { //cannot update any user
                return RedirectToAction("Login", "Home");
            }
        }


        [HttpPost]
        public ActionResult DeleteUser(int id)
         {
            if (ModelState.IsValid)
            {
                BusinessLogicPassThru businessLogicPassThru = new BusinessLogicPassThru(System.Configuration.ConfigurationManager.
                ConnectionStrings["dbconnection"].ConnectionString);
                businessLogicPassThru.DeleteUser(id);

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