using LibraryCommon;
using LibraryCommon.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibraryWebApp.Utility
{
    public static class ProfileChecker
    {

        public static bool IsLoggedIn() 
        {

            UserDTO _check = (UserDTO)System.Web.HttpContext.Current.Session["Profile"];
            return _check == null ? false : true;

        }

        public static bool IsAdmin()
        {
            UserDTO _check = (UserDTO)System.Web.HttpContext.Current.Session["Profile"];
            return _check.RoleName == RoleType.Administrator.ToString() ? true : false;
        }
    }
}