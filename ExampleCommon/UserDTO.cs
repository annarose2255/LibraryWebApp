using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryCommon
{
    public class UserDTO
    {
        public int UserId { get; set; }      
        public int AddressIDFk {get; set;}
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PrimaryEmail { get; set; }
        public string PrimaryPhone { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public string Comment { get; set; }
        public DateTime DateModified { get; set; }
        public int ModifiedByUserID { get; set; }
    }
}
