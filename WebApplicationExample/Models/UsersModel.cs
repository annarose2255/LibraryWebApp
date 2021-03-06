using LibraryWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibraryWebApp.Models
{
    public class UsersModel : BaseModel
    {
        public IEnumerable<UserModel> ListOfUsers { get; set; }

        public UsersModel(List<UserModel> inList)
        {
            ListOfUsers = inList;
        }
    }
}