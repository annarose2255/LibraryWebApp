using LibraryWebApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LibraryWebApp.Models
{
    public class UserModel : BaseModel
    {

        public int UserId { get; set; }

        public string Username { get; set; }

        
        public string Password { get; set; }

      
        public string FirstName { get; set; }

      
        public string LastName { get; set; }

        [EmailAddress]
        public string PrimaryEmail { get; set; }


        [Display(Name = "Primary Phone")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number")]
        public string PrimaryPhone { get; set; } 

        public int RoleId { get; set; }

        public string RoleName { get; set; }
    }
}