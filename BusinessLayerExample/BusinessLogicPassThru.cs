using LibraryCommon.DTO;
using LibraryCommon;
using LibraryDatabaseAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace LibraryBusinessLogicLayer
{

    public class BusinessLogicPassThru
    {
        private readonly string _conn;

        // properties

        // constructors
        public BusinessLogicPassThru()
        {
            _conn = ConfigurationManager.ConnectionStrings["dbconnection"].ToString();
        }
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

        public List<MediaDTO> GetTopThreeMedia()
        {
            MediaDataAccess mediaDataAccess = new MediaDataAccess();

            List<MediaDTO> listOfMedia = mediaDataAccess.GetTop3RecentMedia();

            return listOfMedia;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<DayHours> GetWeekDaysOpen()
        {
            var today = DateTime.Now;
            var beginDate = today.AddDays(-(int)today.DayOfWeek);
            var endDate = beginDate.AddDays(6);

            MetaDataAccess mData = new MetaDataAccess();
            var dtoList = mData.GetDaysClosed(beginDate, endDate);

            List<DayHours> dayList = new List<DayHours>();

            foreach (var dto in dtoList)
            {
                var day = new DayHours
                {
                    Date = dto.Date,
                    IsOpen = !dto.IsClosed,
                    HourOfOperation = dto.IsClosed ? "Closed" : "9AM - 5PM"
                };
                dayList.Add(day);
            }
            return dayList;
        }
        public int CreateUser(UserDTO u)
        {
            UserDataAccess userDataAccess = new UserDataAccess(this._conn);
            int userid = userDataAccess.CreateUser(u);
            return userid;
        }

        public int CreateContactRequest(ContactDTO c)
        {
            ContactDataAcess contactDataAcess = new ContactDataAcess(this._conn);
            int ContactID = contactDataAcess.CreateContactRequest(c);
            return ContactID;
        }

    }
}
