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

    public class ContactDataAcess
    {

        // fields
        private readonly string _conn;

        // properties

        // constructors
        public ContactDataAcess(string conn)
        {
            _conn = conn;
        }

        public ContactDataAcess()
        {
        }

        // methods

        //Get Contact Requests
        public List<ContactDTO> GetContactRequests()
        {
            List<ContactDTO> _list = new List<ContactDTO>();

            try
            {
                using (SqlConnection con = new SqlConnection(_conn))
                {
                    using (SqlCommand _sqlCommand = new SqlCommand("uspGetAllContactRequests", con))
                    {
                        _sqlCommand.CommandType = CommandType.Text;
                        _sqlCommand.CommandTimeout = 30;

                        con.Open();
                        ContactDTO _contact;

                        using (SqlDataReader reader = _sqlCommand.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                _contact = new ContactDTO
                                {
                                    ContactId = reader.GetInt32(reader.GetOrdinal("ContactID")),
                                    Name = (string)reader["Name"],
                                    Email = (reader["Email"] == System.DBNull.Value) ? "" : (string)reader["Email"],
                                    Message = (string)reader["Message"],
                                    DateSubmitted = reader.GetDateTime(reader.GetOrdinal("DateSubmitted")),
                                    FollowedUp = (reader.GetOrdinal("FollowedUp") == 0) ? false : true //if 0 be false, otherwise be true

                                };
                                _list.Add(_contact);
                            }
                        }
                        con.Close();
                    }
                }
            }
            catch (Exception ex)
            {

                throw;
            }

            return _list;
        }

        public int CreateContactRequest(ContactDTO c)
        {

            try
            {
                using (SqlConnection con = new SqlConnection(_conn))
                {
                    using (SqlCommand _sqlCommand = new SqlCommand("uspCreateContactRequest", con))
                    {
                        _sqlCommand.CommandType = CommandType.StoredProcedure;
                        _sqlCommand.CommandTimeout = 30;


                        SqlParameter _parmName = _sqlCommand.CreateParameter();
                        _parmName.DbType = DbType.String;
                        _parmName.ParameterName = "@Name";
                        _parmName.Value = c.Name;
                        _sqlCommand.Parameters.Add(_parmName);


                        // TODO: @parmPrimaryEmail
                        SqlParameter _parmEmail = _sqlCommand.CreateParameter();
                        _parmEmail.DbType = DbType.String;
                        _parmEmail.ParameterName = "@Email";
                        _parmEmail.Value = c.Email;
                        _sqlCommand.Parameters.Add(_parmEmail);

                        // TODO: @parmComment
                        SqlParameter _parmMessage = _sqlCommand.CreateParameter();
                        _parmMessage.DbType = DbType.String;
                        _parmMessage.ParameterName = "@Message";
                        _parmMessage.Value = c.Message;
                        _sqlCommand.Parameters.Add(_parmMessage);

                        // TODO:  @parmDateModified
                        SqlParameter _parmDateSubmitted = _sqlCommand.CreateParameter();
                        _parmDateSubmitted.DbType = DbType.DateTime;
                        _parmDateSubmitted.ParameterName = "@DateSubmitted";
                        _parmDateSubmitted.Value = c.DateSubmitted;
                        _sqlCommand.Parameters.Add(_parmDateSubmitted);


                        SqlParameter _parmContactIDOut = _sqlCommand.CreateParameter();
                        _parmContactIDOut.DbType = DbType.Int32;
                        _parmContactIDOut.ParameterName = "@ChangedContactID";
                        var pk = _sqlCommand.Parameters.Add(_parmContactIDOut);
                        _parmContactIDOut.Direction = ParameterDirection.Output;

                        con.Open();
                        _sqlCommand.ExecuteNonQuery();   // calls the sp 
                        var result = _parmContactIDOut.Value;
                        con.Close();
                        return (int)result;


                    }
                }
            }
            catch (Exception ex)
            {

                throw;
            }


        }


    }

}
