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
                                    MediaTypeID = reader.GetInt32(reader.GetOrdinal("MediaTypeID_FK")),
                                    GenreTypeID = reader.GetInt32(reader.GetOrdinal("GenreTypeID_FK")),
                                    PublisherID = reader.GetInt32(reader.GetOrdinal("PublisherID_FK")),
                                    IsCheckedOutUserID = reader.GetInt32(reader.GetOrdinal("IsCheckedOutUserID_FK")),
                                    Title = reader.SafeGetString("Name"),
                                    //Comment = (string)reader["Comment"],
                                    DateModified = reader.GetDateTime(reader.GetOrdinal("DateModified")),
                                    ModifiedByUserID = reader.GetInt32(reader.GetOrdinal("ModifiedByUserID")),
                                    ImageName = reader.SafeGetString("image-name"),
                                    Description = reader.SafeGetString("Description"),
                                };
                                _list.Add(Media);
                            }
                        }
                        con.Close();
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }

            return _list;
        }

        // methods

        // user login
    }
}
