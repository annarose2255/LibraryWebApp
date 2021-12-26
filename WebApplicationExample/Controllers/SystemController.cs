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

          
            //UsersModel _model = new UsersModel(Mapper.ListOfUserDTOToListOfUserModel(_list));
            DashboardModels _model = new DashboardModels();

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
        public ActionResult EditUser()
        {
            //RoleListVM list = new RoleListVM(_logicRole.GetRolesPassThru());
            //ViewBag.Roles = new SelectList(list.ListOfRoleModel, "RoleId", "RoleName");
            return View();
        }

        [HttpPost]
        public ActionResult EditUser(DashboardModels inModel)
        {
            if (ModelState.IsValid)
            {
                // connection string coming out of the web.config
                BusinessLogicPassThru businessLogicPassThru = new BusinessLogicPassThru(System.Configuration.ConfigurationManager.
                ConnectionStrings["dbconnection"].ConnectionString);

                EditUserBLL edituserBLL = new EditUserBLL();


                UserDTO updateduser = edituserBLL.EditUser(businessLogicPassThru.GetUser(inModel.UserModel.UserId), businessLogicPassThru);

                // TODO: user mapper
                //toAdd.FirstName = model.FirstName;
                //toAdd.LastName = model.LastName;
                //toAdd.UserName = model.Username;
                //toAdd.Password = model.Password;
                //toAdd.RoleID_FK = model.RoleId;

                //_logicUser.CreateUserPassThru(toAdd);

                inModel.Message = updateduser.ErrorMessage;
                if (string.IsNullOrEmpty(inModel.Message))
                {
                    //use case # 1, the user was able to successfully edit fields
                    //reload the page to see the changes
                    return RedirectToAction("Dashboard", "System");
                }
                //the User model does not currently have error messages 
                return RedirectToAction("Dashboard", "System");
            }
            else
            {
                // use case # 3, valiation on client failed, show error message in Dashboard.cshtml
                return View();
            }
        }


        [HttpPost]
        public ActionResult DeleteUser(int inPK)
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