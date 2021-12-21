using LibraryCommon;
using LibraryCommon.DTO;
using LibraryWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplicationExample.Utility
{
   

    public static class Mapper
    {

        public static UserDTO GlobalLoginModelToUserDTO(GlobalLoginModel inModel)
        {
            UserDTO _userDTO = new UserDTO
            {
                Username = inModel.RegisterModel.Username,
                FirstName = inModel.RegisterModel.FirstName,
                LastName = inModel.RegisterModel.LastName,
                Password = inModel.RegisterModel.Password,
                PrimaryEmail = inModel.RegisterModel.PrimaryEmail is null ? "" : inModel.RegisterModel.PrimaryEmail,
                PrimaryPhone = inModel.RegisterModel.PrimaryPhone is null ? "": inModel.RegisterModel.PrimaryPhone,
                RoleId = (int)RoleType.Member, // this is the default
                RoleName = RoleType.Member.ToString()
            };
           
            return _userDTO;
        }
    }   
}