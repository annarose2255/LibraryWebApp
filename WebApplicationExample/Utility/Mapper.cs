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
                Comment = inModel.RegisterModel.Password,
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
                    UserId = item.UserId,
                    RoleId = item.RoleId,
                    RoleName = item.RoleName,
                    FirstName = item.FirstName,
                    LastName = item.LastName,
                    Password = item.Password,
                    Username = item.Username,
                    PrimaryEmail = item.PrimaryEmail,
                    PrimaryPhone = item.PrimaryPhone,
                    Comment = item.Comment
                    
                };
                _list.Add(_m);
            }
            return _list;
        }

        internal static List<RoleModel> ListOfRoleDTOToListOfRoles(List<RoleDTO> inList)
        {
            List<RoleModel> _list = new List<RoleModel>();

            foreach (var item in inList)
            {
                RoleModel _m = new RoleModel
                {                   
                    RoleId = item.RoleId,
                    RoleName = item.RoleName
                };
                _list.Add(_m);
            }
            return _list;
        }

        internal static UserModel UserDTOToUserModel(UserDTO user)
        {
          

            UserModel _m = new UserModel
            {
                UserId = user.UserId,
                RoleId = user.RoleId,
                RoleName = user.RoleName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Password = user.Password,
                Username = user.Username,
                PrimaryEmail = user.PrimaryEmail,
                PrimaryPhone = user.PrimaryPhone,
                Comment = user.Comment //???
            };
            return _m;
        }

        internal static UserDTO UserModelToUserDTO(UserModel inModel, int inCurrentUserId)
        {
            UserDTO _dto = new UserDTO
            {
                UserId = inModel.UserId,
                RoleId = inModel.RoleId,
                //RoleName = inModel.RoleName,
                FirstName = inModel.FirstName,
                LastName = inModel.LastName,
                Password = inModel.Password,
                Username = inModel.Username,
                PrimaryEmail = inModel.PrimaryEmail,
                PrimaryPhone = inModel.PrimaryPhone,
                Comment = inModel.Comment,
                DateModified = DateTime.Now,
                ModifiedByUserID = inCurrentUserId // this will store who make this change this user
            };
            return _dto;
        }   

        public static ContactDTO ContactModelToContactDTO(ContactModel inModel)
        {
            ContactDTO _ContactDTO = new ContactDTO
            {
                Name = inModel.Name,
                Email = inModel.Email is null ? "" : inModel.Email, //??
                Message = inModel.MessageText,
                DateSubmitted = DateTime.Now

            };

            return _ContactDTO;
        }

        public static SupportDTO SupportModelToSupportDTO(SupportModel inModel)
        {
            SupportDTO _SupportDTO = new SupportDTO
            {
                FirstName = inModel.FirstName,
                LastName = inModel.LastName,
                Email = inModel.Email,
                Phone = inModel.PhoneNumber,
                IsMember = inModel.IsMember,
                Message = inModel.MessageText,
                DateSubmitted= DateTime.Now
            };

            return _SupportDTO;
        }
    }
}