using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LibraryWebApp.Models
{
    public class LoginModel : BaseModel
    {
      

        [Required(ErrorMessage = "Username is required.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[#@$%!?^&*\-+]).*$", ErrorMessage = "Invalid Password. Passwords must contain: " +
            "<br> &ensp; \u2022 One or more captial letters " +
            "<br> &ensp; \u2022 One or more lower case letters" +
            "<br> &ensp; \u2022 One or more numbers (0-9)" +
            "<br> &ensp; \u2022 One or more special characters (#, ?, !, @, $, %, ^, &, *, -, +)")]
        [StringLength(20, MinimumLength = 8, ErrorMessage = "Password must be at least 8 and no more than 20 characters long.")]
        /* Notes about above regrex: 
          ^ - The string being checked must start with this regrex
          (?= ) - this is a lookabout which means after checking for the expression (the stuff after the = ) reset checking to the begining of the string - this allows us to not care for the order of the characters
          (?=.*?[a-z]) - This checks for at least 1 lowercase letter  
          (?=.*?[A-Z]) - This checks for at least 1 uppercase letter
          (?=.*?[0-9]) - This checks for at least 1 number (0-9)
          (?=.*?[#?!@$%^&*-+]) - This checks for at least 1 of our special characters (#, ?, !, @, $, %, ^, &, *, -, +)
          .* means that at the end of the string once we have checked for our characters, we dont care about any following characters 
          $ - The string being checked must end with the preceeding rules (regrex)
        */
        /* Notes about the error message:
            <br> is html code that (creates a break line) when it goes out (b/c of the method in login view)
            &ensp; is html code that (creates 2 spaces) when it goes out (b/c of the method in login view)
            \u2022 is the unicode value for a bullet
         */
        public string Password { get; set; }


    }
}