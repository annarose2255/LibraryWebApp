using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LibraryWebApp.Models
{
    public class ContactModel : BaseModel
    {
        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessage ="Invalid email")]
        [StringLength (30, MinimumLength = 6,ErrorMessage ="Too short or long of an email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Brevity is the soul of wit, but we need your message to have text.")]
        public string MessageText { get; set; }
        //public new string Message { get; set; } // hides the base property   ?????????

    }
}