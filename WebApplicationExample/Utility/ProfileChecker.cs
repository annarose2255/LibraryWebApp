using LibraryCommon;
using LibraryCommon.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibraryWebApp.Utility
{
    internal static class ProfileChecker
    {

        internal static bool IsLoggedIn() 
        {

            UserDTO _check = (UserDTO)System.Web.HttpContext.Current.Session["Profile"];
            return _check == null ? false : true;

        }

        internal static bool IsAdmin()
        {
            UserDTO _check = (UserDTO)System.Web.HttpContext.Current.Session["Profile"];
            return _check.RoleName == RoleType.Administrator.ToString() ? true : false;
        }
    }
}