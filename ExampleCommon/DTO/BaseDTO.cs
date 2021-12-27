using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryCommon.DTO
{
    public class BaseDTO
    {

        public string ErrorMessage { get; set; }

        public BaseDTO()
        {
            ErrorMessage = "";
        
        }

    }
}
