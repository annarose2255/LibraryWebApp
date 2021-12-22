using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LibraryWebApp.Models
{
    public class SupportModel : BaseModel
    {
        [Required(ErrorMessage = "A First Name is required.")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "A Last Name is required.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessage = "Invalid email")]
        [StringLength(30, MinimumLength = 6, ErrorMessage = "Too short or long of an email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone Number is required.")]
        [RegularExpression(@"^^(1-)?\d{3}-\d{3}-\d{4}$$", ErrorMessage = "Invalid Phone Number. Please put in the form of ###-###-#### ")]
        [StringLength(13, MinimumLength = 11, ErrorMessage = "Not the correct length of a phone number")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Selection is required.")]
        public bool IsMember { get; set; } 

        [Required(ErrorMessage = "Brevity is the soul of wit, but we need your message to have text.")]
        public string MessageText { get; set; }

    }
}