using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibraryWebApp.Models
{
    public class DashboardModels : BaseModel
    {
        public UserModel UserModel { get; set; }
        public UsersModel UsersModel { get; set; }
        public DashboardModels()
        {
            UserModel = new UserModel();
            //UsersModel = new UsersModel();
        }
    }
}