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


        [Display(Name = "First Name")]
        [Required(ErrorMessage = "First name is required.")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Last name is required.")]
        public string LastName { get; set; }

        [EmailAddress(ErrorMessage = "Not a valid email.")]
        [Display(Name = "Email")]
        public string PrimaryEmail { get; set; } = "";

        [Display(Name = "Phone")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number")]
        public string PrimaryPhone { get; set; } = "";

        public string Username { get; set; }
      
        public string Password { get; set; }
        [Display(Name = "Role")]
        public int RoleId { get; set; }

        public string RoleName { get; set; }

        public string Comment { get; set; }
    }
}