using LibraryCommon;
using LibraryCommon.DTO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LibraryBusinessLogicLayer
{
    public class EditUserBLL
    {
        public void EditUser(UserDTO userDTO, BusinessLogicPassThru businessLogicPassThru)
        {
            businessLogicPassThru.EditUser(userDTO);
            //return updateduser;
        }
    }
}
