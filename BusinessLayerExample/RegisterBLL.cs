using LibraryCommon;
using LibraryCommon.DTO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LibraryBusinessLogicLayer
{
    public class RegisterBLL
    {


        public UserDTO Register(UserDTO userDTO, BusinessLogicPassThru businessLogicPassThru)
        {
            UserDTO _user;
            // 1. get all the users for the database, GetUsersData() uses a view, which filters for certain roles
            List<UserDTO> _allUsers = businessLogicPassThru.GetUsersData();

            // 2. filter thru and see if I have match for this username
            UserDTO _match = (_allUsers.Where(u => u.Username == userDTO.Username).Count() == 1) ?
                _allUsers.Where(u => u.Username == userDTO.Username).FirstOrDefault() : new UserDTO();

            if (_match.Username != null)
            {
                // 3. if match,  unable to register.
                _match.ErrorMessage = "Username already exists.";
                return _match;
            }
            else
            {
                Hasher hasher = new Hasher();
                string _salt = hasher.CreateSalt();
                //create new user from HTTPGet 
                _user = new UserDTO
                {
                    RoleId = userDTO.RoleId,  //RoleId is assumed member, has minimal abilities
                    RoleName = userDTO.RoleName,
                    AddressID = userDTO.AddressID,
                    FirstName = userDTO.FirstName,
                    LastName = userDTO.LastName,
                    PrimaryEmail = userDTO.PrimaryEmail,
                    PrimaryPhone = userDTO.PrimaryPhone,
                    Username = userDTO.Username,
                    Password = hasher.HashedValue(_salt + userDTO.Password), //add salt to password before creating hashed password
                    Salt = _salt,//use password to get salt 
                    Comment =userDTO.Comment,
                    DateModified = DateTime.Now,  // default will be picked by database default
                    ModifiedByUserID = (int)RoleType.System // default will be picked by database default

                };
                //assign userid after creating user in Db
                int userid = businessLogicPassThru.CreateUser(_user);
                _user.UserId = userid;
                return _user;
            }
        }
    }
}
