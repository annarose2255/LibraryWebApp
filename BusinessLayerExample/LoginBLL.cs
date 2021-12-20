using LibraryCommon;
using LibraryCommon.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryBusinessLogicLayer
{
    public class LoginBLL
    {
        public UserDTO Login(string username, string password, BusinessLogicPassThru businessLogicPassThru)
        {
            // 1. get all the users for the database
            List<UserDTO> _allUsers = businessLogicPassThru.GetUsersData();

            // 2. filter thru and see if I have match for this username
            UserDTO _match = (_allUsers.Where(u => u.Username == username).Count() == 1) ? 
                _allUsers.Where(u => u.Username == username).FirstOrDefault() : new UserDTO();


            if (_match.Password != null)
            {
                // 3. if match,  hash and salt the password parm.
                // get salt from the match
                Hasher hasher = new Hasher();
                string _salt = _match.Salt;
                string _hashAndSalt = hasher.SHA256HashWithSalt(password, _salt);

                // 4. if it matches, we have have a validated user so pass it back up.
                if (_hashAndSalt == _match.Password)
                {
                    return _match;
                }
                else 
                {
                    // add error message
                    _match.ErrorMessage = "Username and/or password not recognized.";
                    return _match;
                }
            }
            else
            {

                // 5. if not return error message and UserDTO.password is null
                _match.ErrorMessage = "Please register.";
                return _match;


            }

        }
    }
    public class RegisterBLL
    {
        public UserDTO Register(string username, string firstname, string lastname,string password, int roleid, int addressid, BusinessLogicPassThru businessLogicPassThru,string email = "",string phonenumber = "")
        {
            UserDTO _user;
            // 1. get all the users for the database
            List<UserDTO> _allUsers = businessLogicPassThru.GetUsersData();

            // 2. filter thru and see if I have match for this username
            UserDTO _match = (_allUsers.Where(u => u.Username == username).Count() == 1) ?
                _allUsers.Where(u => u.Username == username).FirstOrDefault() : new UserDTO();


            if (_match.Password != null)
            {
                // 3. if match,  hash and salt the password parm.
                _match.ErrorMessage = "Username already exists.";
                return _match;
            }
            else
            {
                //create new user from HTTPGet 
                _user = new UserDTO
                {
                    //TODO: will need to change how roleid and addressid are passed through, currently no RoleDTO
                    RoleId = roleid,
                    AddressID = addressid,
                    FirstName = firstname,
                    LastName = lastname,
                    PrimaryEmail = email,
                    PrimaryPhone = phonenumber,
                    Username = username,
                    Password = password,
                    Salt = "",//use password to get salt 
                    Comment = "",
                    DateModified = DateTime.Now,
                    ModifiedByUserID = roleid

                };
                //assign userid after creating user in Db
                int userid = businessLogicPassThru.CreateUser(_user);
                _user.UserId = userid;
                return _user;
            }
            //return _newuser;
        }
    }
}
