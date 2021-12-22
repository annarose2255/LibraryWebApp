using ExampleCommon;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryDatabaseAccessLayer
{
    public class MetaDataAccess
    {
        private readonly string _connString;

        public MetaDataAccess()
        {
            _connString = "Data Source=DESKTOP-99PARR2;Integrated Security=True; Initial Catalog = Library";
        }

        /// <summary>
        /// Returns a list of days closed within specified range of dates.  
        /// </summary>
        /// <param name="beginDate">Sets beginning of range</param>
        /// <param name="endDate">End of date range</param>
        /// <returns></returns>
        public List<DaysClosedDTO> GetDaysClosed(DateTime beginDate, DateTime endDate)
        {
            List<DaysClosedDTO> daysClosed = new List<DaysClosedDTO>();
            try
            {
                using (SqlConnection conn = new SqlConnection(_connString))
                {
                    using (SqlCommand command = new SqlCommand("spGetDaysClosed", conn))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@BeginDate", beginDate);
                        command.Parameters.AddWithValue("@EndDate", endDate);

                        conn.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            DaysClosedDTO _days;
                            while (reader.Read())
                            {
                                _days = new DaysClosedDTO
                                {
                                    IsClosed = Convert.ToBoolean(reader["IsClosed"]),
                                    Date = Convert.ToDateTime(reader["Date"])
                                };
                                daysClosed.Add(_days);
                            }
                        }
                        conn.Close();
                    }
                }
                return daysClosed;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
