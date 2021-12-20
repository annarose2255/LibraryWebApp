using LibraryCommon.DTO;
using LibraryDatabaseAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryBusinessLogicLayer
{

    public class BusinessLogicPassThru
    {
        private readonly string _conn;

        // properties

        // constructors
        public BusinessLogicPassThru(string conn)
        {
            _conn = conn;
        }

        public List<UserDTO> GetUsersData()
        {

            UserDataAccess userDataAccess = new UserDataAccess(this._conn);

            List<UserDTO> _listOfUsers = userDataAccess.GetUsers();

            return _listOfUsers;
        }
        public int CreateUser(UserDTO u)
        {
            UserDataAccess userDataAccess = new UserDataAccess(this._conn);
            int userid = userDataAccess.CreateUser(u);
            return userid;
        }

       
    }
}
