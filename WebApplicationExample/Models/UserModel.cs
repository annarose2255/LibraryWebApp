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

     
        public string PrimaryEmail { get; set; } 
   
          
        public string PrimaryPhone { get; set; } 

        public int RoleId { get; set; }

        public string RoleName { get; set; }
    }
}