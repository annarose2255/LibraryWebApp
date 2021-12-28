using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibraryCommon.DTO
{
    public class RoleDTO
    {

        public int RoleId { get; set; }

        public string RoleName { get; set; }

        public string Comment { get; set; }

        public DateTime DateModified { get; set; }
        public int ModifiedByUserID { get; set; }

    }
}