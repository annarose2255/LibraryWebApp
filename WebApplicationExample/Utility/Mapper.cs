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

        internal static List<UserModel> ListOfUserDTOToListOfUserModel(List<UserDTO> inList)
        {
            List<UserModel> _list = new List<UserModel>();

            foreach (var item in inList)
            {
                UserModel _m = new UserModel 
                { 
                    RoleId = item.RoleId,
                    RoleName = item.RoleName,
                    FirstName = item.FirstName,
                    LastName = item.LastName,
                    Password = item.Password,
                    Username = item.Username,
                    PrimaryEmail = item.PrimaryEmail,
                    PrimaryPhone = item.PrimaryPhone
                };
                _list.Add(_m);
            }

            return _list;
        }
        internal static UserModel UserDTOToUserModel(UserDTO item)
        {
            UserModel _m = new UserModel
            {
                RoleId = item.RoleId,
                RoleName = item.RoleName,
                FirstName = item.FirstName,
                LastName = item.LastName,
                Password = item.Password,
                Username = item.Username,
                PrimaryEmail = item.PrimaryEmail,
                PrimaryPhone = item.PrimaryPhone
            };
            return _m;
        }
        //internal static UserDTO UserModelToUserDTO(UserModel inModel)
        //{
        //    //need to obtain salt from database.. create userDTO from database
        //    int userid = inModel.UserId;
        //}
    }   
}