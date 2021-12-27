using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibraryWebApp.Models
{
    //create global login model to contain multiple models in one view
    public class GlobalLoginModel : BaseModel
    {
        public LoginModel LoginModel { get; set; }
        public RegisterModel RegisterModel { get; set; }


        public GlobalLoginModel() 
        {
            LoginModel = new LoginModel();
            RegisterModel = new RegisterModel();
        }
    }
}