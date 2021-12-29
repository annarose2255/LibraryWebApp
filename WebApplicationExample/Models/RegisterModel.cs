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
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[#@$%!?^&*\-+]).*$", ErrorMessage = "Invalid Password. Passwords must contain: " +
            "<br> &ensp; \u2022 One or more captial letters " +
            "<br> &ensp; \u2022 One or more lower case letters" +
            "<br> &ensp; \u2022 One or more numbers (0-9)" +
            "<br> &ensp; \u2022 One or more special characters (#, ?, !, @, $, %, ^, &, *, -, +)")]
        [StringLength(20, MinimumLength = 8, ErrorMessage = "Password must be at least 8 and no more than 20 characters long.")]
        public string Password { get; set; }


        [Display(Name = "First Name")]
        [Required(ErrorMessage = "First name is required.")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Last name is required.")]
        public string LastName { get; set; }
        [EmailAddress]
        public string PrimaryEmail { get; set; } = "";
        [Display(Name = "Primary Phone")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number")]
        public string PrimaryPhone { get; set; } = "";
        //public int RoleId { get; set; }
        public int AddressID { get; set; }


    }
}