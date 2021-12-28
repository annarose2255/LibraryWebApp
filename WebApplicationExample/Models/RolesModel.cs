using LibraryWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibraryWebApp.Models
{
    public class RolesModel : BaseModel
    {
        public IEnumerable<RoleModel> ListOfRoles { get; set; }

        public RolesModel(List<RoleModel> inList)
        {
            ListOfRoles = inList;
        }
    }
}