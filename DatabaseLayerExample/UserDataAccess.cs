using LibraryCommon;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace LibraryDatabaseAccessLayer
{
    public class UserDataAccess
    {
        // fields
        private readonly string _conn;

        // properties

        // constructors
        public UserDataAccess(string conn)
        {
            _conn = conn;
        }

        public UserDataAccess()
        {
        }

        // methods

        // user login
        public List<UserDTO> GetUsers()
        {
            List<UserDTO> _list = new List<UserDTO>();

            try
            {
                using (SqlConnection con = new SqlConnection(_conn))
                {
                    using (SqlCommand _sqlCommand = new SqlCommand("uspGetUser", con))
                    {
                        _sqlCommand.CommandType = CommandType.Text;
                        _sqlCommand.CommandTimeout = 30;

                        con.Open();
                        UserDTO _user;

                        using (SqlDataReader reader = _sqlCommand.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                _user = new UserDTO
                                {
                                    UserId = reader.GetInt32(reader.GetOrdinal("UserID")),
                                    RoleId = reader.GetInt32(reader.GetOrdinal("RoleID")),
                                    AddressID = reader.GetInt32(reader.GetOrdinal("AddressID")),
                                    FirstName = (string)reader["FirstName"],
                                    LastName = (string)reader["LastName"],
                                    PrimaryEmail = (reader["PrimaryEmail"] == System.DBNull.Value) ? "" : (string)reader["PrimaryEmail"],
                                    PrimaryPhone = (reader["PrimaryPhone"] == System.DBNull.Value) ? "" : (string)reader["PrimaryPhone"],
                                    Username = (string)reader["UserName"],
                                    Password = (string)reader["Password"],
                                    Salt = (reader["Salt"] == System.DBNull.Value) ? "" : (string)reader["Salt"], // teritary operation C#   
                                    RoleName = (string)reader["RoleName"],
                                    Comment = (reader["Comment"] == System.DBNull.Value) ? "" : (string)reader["Comment"],
                                    DateModified = reader.GetDateTime(reader.GetOrdinal("DateModified")),
                                    ModifiedByUserID = reader.GetInt32(reader.GetOrdinal("RoleID"))

                                };
                                _list.Add(_user);
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
        public void CreateUser(UserDTO u)
        {

            try
            {
                using (SqlConnection con = new SqlConnection(_conn))
                {
                    using (SqlCommand _sqlCommand = new SqlCommand("uspCreateUser", con))
                    {
                        _sqlCommand.CommandType = CommandType.StoredProcedure;
                        _sqlCommand.CommandTimeout = 30;


                        SqlParameter _parmRoleIdFK = _sqlCommand.CreateParameter();
                        _parmRoleIdFK.DbType = DbType.Int32;
                        _parmRoleIdFK.ParameterName = "@parmRoleID_FK";
                        _parmRoleIdFK.Value = u.RoleId;
                        _sqlCommand.Parameters.Add(_parmRoleIdFK);

                        SqlParameter _parmAddressIDFK = _sqlCommand.CreateParameter();
                        _parmAddressIDFK.DbType = DbType.Int32;
                        _parmAddressIDFK.ParameterName = "@parmAddressID_FK";
                        _parmAddressIDFK.Value = u.RoleId;
                        _sqlCommand.Parameters.Add(_parmAddressIDFK);

                        SqlParameter _parmFirstName = _sqlCommand.CreateParameter();
                        _parmFirstName.DbType = DbType.String;
                        _parmFirstName.ParameterName = "@parmFirstName";
                        _parmFirstName.Value = u.FirstName;
                        _sqlCommand.Parameters.Add(_parmFirstName);

                        SqlParameter _parmLastName = _sqlCommand.CreateParameter();
                        _parmLastName.DbType = DbType.String;
                        _parmLastName.ParameterName = "@parmLastName";
                        _parmLastName.Value = u.LastName;
                        _sqlCommand.Parameters.Add(_parmLastName);

                        // TODO: @parmPrimaryEmail


                        // TODO: @parmPrimaryPhone

                        SqlParameter _parmUserName = _sqlCommand.CreateParameter();
                        _parmUserName.DbType = DbType.String;
                        _parmUserName.ParameterName = "@parmUserName";
                        _parmUserName.Value = u.Username;
                        _sqlCommand.Parameters.Add(_parmUserName);

                        SqlParameter _paramPassword = _sqlCommand.CreateParameter();
                        _paramPassword.DbType = DbType.String;
                        _paramPassword.ParameterName = "@ParamPassword";
                        _paramPassword.Value = u.Password;
                        _sqlCommand.Parameters.Add(_paramPassword);

                       
                        SqlParameter _paramSalt = _sqlCommand.CreateParameter();
                        _paramSalt.DbType = DbType.String;
                        _paramSalt.ParameterName = "@ParamSalt";
                        _paramSalt.Value = u.Salt;
                        _sqlCommand.Parameters.Add(_paramSalt);



                        // TODO: @parmComment

                        // TODO:  @parmDateModified

                        // TODO: @parmModifiedByUserID


                        SqlParameter _parmUserIDOut = _sqlCommand.CreateParameter();
                        _parmUserIDOut.DbType = DbType.Int32;
                        _parmUserIDOut.ParameterName = "@parmUserIDOut";
                        var pk = _sqlCommand.Parameters.Add(_parmUserIDOut);
                        _parmUserIDOut.Direction = ParameterDirection.Output;

                        con.Open();
                        _sqlCommand.ExecuteNonQuery();   // calls the sp 
                                                         //var result = _paramAuthorIDReturn.Value;
                        con.Close();
                        //return (int)result;


                    }
                }
            }
            catch (Exception)
            {

                throw;
            }

          
        }

    }
}
