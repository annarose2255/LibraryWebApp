using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibraryWebApp.Models
{
    public class BaseModel
    {
        public string Message { get; set; }

        public BaseModel()
        {
            this.Message = "";
        }

    }
}