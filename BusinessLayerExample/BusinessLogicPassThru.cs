using ExampleCommon;
using LibraryCommon;
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

            List<MediaDTO> listOfMedia = new List<MediaDTO>();
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

            //return dtoList.Select(dto => new DayHours
            //{
            //    Date = dto.Date,
            //    IsOpen = !dto.IsClosed,
            //    HourOfOperation = dto.IsClosed ? "Closed": "9AM - 5PM"
            //}).ToList();

        }
    }
}
