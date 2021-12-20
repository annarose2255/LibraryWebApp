using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LibraryWebApp.Models
{
    public class RegisterModel : BaseModel
    {
        public new string Message { get; set; } // hides the base property

        [Required(ErrorMessage = "Username is required.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }


        [Display(Name = "First Name")]
        [Required(ErrorMessage = "First name is required.")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Last name is required.")]
        public string LastName { get; set; }

        public string PrimaryEmail { get; set; } = "";
        public string PrimaryPhone { get; set; } = "";
        public int RoleId { get; set; }
        public int AddressID { get; set; }


    }
}