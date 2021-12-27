using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using LibraryCommon.DTO;

namespace LibraryDatabaseAccessLayer
{
    public class SupportDataAccess
    {

        // fields
        private readonly string _conn;

        // properties

        // constructors
        public SupportDataAccess(string conn)
        {
            _conn = conn;
        }

        public SupportDataAccess()
        {
        }

        // methods

        public List<SupportDTO> GetAllSupportRequest()
        {
            List<SupportDTO> _list = new List<SupportDTO>();

            try
            {
                using (SqlConnection con = new SqlConnection(_conn))
                {
                    using (SqlCommand _sqlCommand = new SqlCommand("uspGetAllContactRequests", con))
                    {
                        _sqlCommand.CommandType = CommandType.Text;
                        _sqlCommand.CommandTimeout = 30;

                        con.Open();
                        SupportDTO _support;

                        using (SqlDataReader reader = _sqlCommand.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                _support = new SupportDTO
                                {
                                    SupportId = reader.GetInt32(reader.GetOrdinal("SupportID")),
                                    FirstName = (string)reader["FirstName"],
                                    LastName = (string)reader["LastName"],
                                    Email = (reader["Email"] == System.DBNull.Value) ? "" : (string)reader["Email"],
                                    Phone = (reader["Phone"] == System.DBNull.Value) ? "" : (string)reader["Phone"],
                                    IsMember = (reader.GetOrdinal("IsMember") == 0) ? false : true, //if 0 be false, otherwise be true
                                    Message = (string)reader["Message"],
                                    DateSubmitted = reader.GetDateTime(reader.GetOrdinal("DateSubmitted")),
                                    FollowedUp = (reader.GetOrdinal("FollowedUp") == 0) ? false : true, //if 0 be false, otherwise be true
                                    UserIDFollowedUp_FK = (reader["UserIDFollowedUp_FK"] == System.DBNull.Value)? -1 : reader.GetInt32(reader.GetOrdinal("UserIDFollowedUp_FK")) //put -1 if there is no uder followup

                                };
                                _list.Add(_support);
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


        // TODO: return Operation DTO
        // register at login
        public int CreateSupportRequest(SupportDTO s)
        {

            try
            {
                using (SqlConnection con = new SqlConnection(_conn))
                {
                    using (SqlCommand _sqlCommand = new SqlCommand("uspCreateSupportRequest", con))
                    {
                        _sqlCommand.CommandType = CommandType.StoredProcedure;
                        _sqlCommand.CommandTimeout = 30;


                        SqlParameter _parmFirstName = _sqlCommand.CreateParameter();
                        _parmFirstName.DbType = DbType.String;
                        _parmFirstName.ParameterName = "@FirstName";
                        _parmFirstName.Value = s.FirstName;
                        _sqlCommand.Parameters.Add(_parmFirstName);

                        SqlParameter _parmLastName = _sqlCommand.CreateParameter();
                        _parmLastName.DbType = DbType.String;
                        _parmLastName.ParameterName = "@LastName";
                        _parmLastName.Value = s.LastName;
                        _sqlCommand.Parameters.Add(_parmLastName);

                        // TODO: @parmPrimaryEmail
                        SqlParameter _parmEmail = _sqlCommand.CreateParameter();
                        _parmEmail.DbType = DbType.String;
                        _parmEmail.ParameterName = "@Email";
                        _parmEmail.Value = s.Email;
                        _sqlCommand.Parameters.Add(_parmEmail);


                        // TODO: @parmPrimaryPhone
                        SqlParameter _parmPhone = _sqlCommand.CreateParameter();
                        _parmPhone.DbType = DbType.String;
                        _parmPhone.ParameterName = "@Phone";
                        _parmPhone.Value = s.Phone;
                        _sqlCommand.Parameters.Add(_parmPhone);


                        SqlParameter _parmIsMember = _sqlCommand.CreateParameter();
                        _parmIsMember.DbType = DbType.Int32;
                        _parmIsMember.ParameterName = "@IsMember";
                        _parmIsMember.Value = (s.IsMember == true)? 1: 0;
                        _sqlCommand.Parameters.Add(_parmIsMember);

                        SqlParameter _paramMessage = _sqlCommand.CreateParameter();
                        _paramMessage.DbType = DbType.String;
                        _paramMessage.ParameterName = "@Message";
                        _paramMessage.Value = s.Message;
                        _sqlCommand.Parameters.Add(_paramMessage);


                        SqlParameter _paramDateSubmitted = _sqlCommand.CreateParameter();
                        _paramDateSubmitted.DbType = DbType.DateTime;
                        _paramDateSubmitted.ParameterName = "@DateSubmitted";
                        _paramDateSubmitted.Value = s.DateSubmitted;
                        _sqlCommand.Parameters.Add(_paramDateSubmitted);


                        SqlParameter _parmSupportIDOut = _sqlCommand.CreateParameter();
                        _parmSupportIDOut.DbType = DbType.Int32;
                        _parmSupportIDOut.ParameterName = "@ChangedSupportID";
                        var pk = _sqlCommand.Parameters.Add(_parmSupportIDOut);
                        _parmSupportIDOut.Direction = ParameterDirection.Output;

                        con.Open();
                        _sqlCommand.ExecuteNonQuery();   // calls the sp 
                        var result = _parmSupportIDOut.Value;
                        s.SupportId = (int)result;
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
