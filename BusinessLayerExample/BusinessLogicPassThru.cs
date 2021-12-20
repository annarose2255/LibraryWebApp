﻿using LibraryCommon;
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


        public List<UserDTO> GetUsersData()
        {

            UserDataAccess userDataAccess = new UserDataAccess();

            List<UserDTO> _listOfUsers = userDataAccess.GetUsers();

            return _listOfUsers;
        }

        public List<MediaDTO> GetTopThreeMedia()
        {
            MediaDataAccess mediaDataAccess = new MediaDataAccess();

            List<MediaDTO> listOfMedia = mediaDataAccess.GetTop3RecentMedia();

            return listOfMedia;
        }
       
    }
}
