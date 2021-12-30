using System;
using LibraryCommon;

using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibraryWebApp.Models
{
    public class AboutUsModel
    {
        public List<DayHours> DayHours { get; set; }
        public UsersModel UsersModel { get; set; }
        public AboutUsModel(List<UserModel> inList)
        {
            DayHours = new List<DayHours>();
            UsersModel = new UsersModel(inList);
            
        }
    }
}