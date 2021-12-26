using LibraryCommon;
using LibraryCommon.DTO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LibraryBusinessLogicLayer
{
    public class EditUserBLL
    {
        public UserDTO EditUser(UserDTO userDTO, BusinessLogicPassThru businessLogicPassThru)
        {
            UserDTO updateduser = businessLogicPassThru.EditUser(userDTO);
            return updateduser;
        }
    }
}
