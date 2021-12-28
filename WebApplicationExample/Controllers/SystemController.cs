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
using LibraryCommon;

namespace LibraryWebApp
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
            _model.UsersModel = new UsersModel(Mapper.ListOfUserDTOToListOfUserModel(_list));

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
            return View("Login");
            
        }
        [HttpGet]
        public ActionResult RequestEdit(DashboardModels inModel)
        {
            inModel.UserModel.Message = "incomplete";
            return View("Dashboard", inModel);
        }

        [HttpPost]
        public ActionResult EditUser(DashboardModels inModel)
        {
            UserDTO _profile = (UserDTO)System.Web.HttpContext.Current.Session["Profile"];
            if (ModelState.IsValid)
            {
                // connection string coming out of the web.config
                BusinessLogicPassThru businessLogicPassThru = new BusinessLogicPassThru(System.Configuration.ConfigurationManager.
                ConnectionStrings["dbconnection"].ConnectionString);
                //requested changes to profile/user fields
                UserDTO _mappedUser = new UserDTO
                {
                    Username = inModel.UserModel.Username,
                    FirstName = inModel.UserModel.FirstName,
                    LastName = inModel.UserModel.LastName,
                    Password = inModel.UserModel.Password,
                    PrimaryEmail = inModel.UserModel.PrimaryEmail is null ? "" : inModel.UserModel.PrimaryEmail,
                    PrimaryPhone = inModel.UserModel.PrimaryPhone is null ? "" : inModel.UserModel.PrimaryPhone,
                    RoleId = inModel.UserModel.RoleId, 
                    RoleName = inModel.UserModel.RoleName
                };
                Hasher hasher = new Hasher();
                //userDTO with updated fields (unchanged fields stayed the same)
                UserDTO new_db_fields = new UserDTO
                {
                    //i need to get the UserID from the database via session profile
                    UserId = _profile.UserId,
                    //if the model does not have a value, keep the original, otherwise, pass in new value
                    Username = string.IsNullOrEmpty(_mappedUser.Username) ? _profile.Username : _mappedUser.Username,
                    //new password must be passed as a hash
                    Password = string.IsNullOrEmpty(_mappedUser.Password) ? _profile.Password : hasher.HashedValue(_profile.Salt+_mappedUser.Password),
                    //no reason to change salt 
                    Salt = _profile.Salt,
                    FirstName = string.IsNullOrEmpty(_mappedUser.FirstName) ? _profile.FirstName : _mappedUser.FirstName,
                    LastName = string.IsNullOrEmpty(_mappedUser.LastName) ? _profile.LastName : _mappedUser.LastName,
                    PrimaryEmail = string.IsNullOrEmpty(_mappedUser.PrimaryEmail) ? _profile.PrimaryEmail : _mappedUser.PrimaryEmail,
                    PrimaryPhone = string.IsNullOrEmpty(_mappedUser.PrimaryPhone) ? _profile.PrimaryPhone : _mappedUser.PrimaryPhone,
                    //cannot change AddressID currently
                    AddressID = _profile.AddressID,
                    //possibility for these not to match in db in future, for now, not editable from dashboard
                    RoleId = _mappedUser.RoleId == 0 ? _profile.RoleId : _mappedUser.RoleId,
                    RoleName = string.IsNullOrEmpty(_mappedUser.RoleName) ? _profile.RoleName : _mappedUser.RoleName,
                    Comment = string.IsNullOrEmpty(_mappedUser.Comment) ? _profile.Comment : _mappedUser.Comment,
                    ModifiedByUserID = _profile.UserId,
                    DateModified = DateTime.Now

                };
                businessLogicPassThru.EditUser(new_db_fields);
                if (string.IsNullOrEmpty(inModel.Message) & string.IsNullOrEmpty(inModel.UserModel.Message))
                {
                    //use case # 1, the user was able to successfully edit fields
                    //reload the page, show 
                    System.Web.HttpContext.Current.Session["Profile"] = businessLogicPassThru.GetUser(new_db_fields);
                    inModel.UserModel.Message = "complete";
                    inModel.Message = "Changes saved successfully";
                    return View("Dashboard",inModel);
                }
                //the dashboard has error messages, display edit view 
                inModel.UserModel.Message = "incomplete";
                return View("Dashboard", inModel);
            }
            else
            {
                // use case # 3, valiation on client failed, show error message in Dashboard.cshtml, don't leave editing part of view
                inModel.UserModel.Message = "Unable to save changes to user";
                return View("Dashboard",inModel);
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
        [HttpGet]
        public void Logout()
        {

            Session.Abandon();

            //return View();
        }
    }





    

}