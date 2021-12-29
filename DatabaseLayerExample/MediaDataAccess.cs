using DatabaseLayerExample;
using LibraryCommon;
using LibraryCommon.DTO;
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
    public class MediaDataAccess
    {
        // fields
        private readonly string _conn;

        // properties

        // constructors
        public MediaDataAccess(string conn)
        {
            _conn = conn;
        }

        public MediaDataAccess()
        {
            _conn = ConfigurationManager.ConnectionStrings["dbconnection"].ToString();
        }


        public List<MediaDTO> GetTop3RecentMedia()
        {
            List<MediaDTO> _list = new List<MediaDTO>();

            try
            {
                using (SqlConnection con = new SqlConnection(_conn))
                {
                    using (SqlCommand _sqlCommand = new SqlCommand("sspGetMost3RecentMedia", con))
                    {
                        _sqlCommand.CommandType = CommandType.Text;
                        _sqlCommand.CommandTimeout = 30;

                        con.Open();
                        MediaDTO Media;

                        using (SqlDataReader reader = _sqlCommand.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                
                                Media = new MediaDTO
                                {
                                    MediaId = reader.GetInt32(reader.GetOrdinal("MediaID")),
                                    MediaTypeID = reader.GetInt32(reader.GetOrdinal("MediaTypeID")),
                                    GenreTypeID = reader.GetInt32(reader.GetOrdinal("GenreTypeID")),
                                    PublisherID = reader.GetInt32(reader.GetOrdinal("PublisherID")),
                                    IsCheckedOutUserID = reader.GetInt32(reader.GetOrdinal("IsCheckedOutUserID")),
                                    Title = reader.SafeGetString("Title"),
                                    //Comment = (string)reader["Comment"],
                                    DateModified = reader.GetDateTime(reader.GetOrdinal("DateModified")),
                                    ModifiedByUserID = reader.GetInt32(reader.GetOrdinal("ModifiedByUserID")),
                                    ImageName = reader.SafeGetString("ImageName"),
                                    Description = reader.SafeGetString("Description"),
                                    GenreName = reader.SafeGetString("GenreName"),
                                    PublisherName = reader.SafeGetString("PublisherName"),
                                };
                                _list.Add(Media);
                            }
                        }
                        con.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                LogException logexception = new LogException(_conn);
                int LogId_PK = logexception.CreateLogException(ex);
                throw;
            }

            return _list;
        }

        // methods

        // user login
    }
}
