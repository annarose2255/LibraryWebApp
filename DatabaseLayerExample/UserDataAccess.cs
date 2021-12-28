using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using LibraryCommon.DTO;

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

        
        #region Users

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
                                    UserId = (int) reader["UserID"],
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
        public int CreateUser(UserDTO u)
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
                        SqlParameter _parmPrimaryEmail = _sqlCommand.CreateParameter();
                        _parmPrimaryEmail.DbType = DbType.String;
                        _parmPrimaryEmail.ParameterName = "@parmPrimaryEmail";
                        _parmPrimaryEmail.Value = u.PrimaryEmail;
                        _sqlCommand.Parameters.Add(_parmPrimaryEmail);


                        // TODO: @parmPrimaryPhone
                        SqlParameter _parmPrimaryPhone = _sqlCommand.CreateParameter();
                        _parmPrimaryPhone.DbType = DbType.String;
                        _parmPrimaryPhone.ParameterName = "@parmPrimaryPhone";
                        _parmPrimaryPhone.Value = u.PrimaryPhone;
                        _sqlCommand.Parameters.Add(_parmPrimaryPhone);

                        SqlParameter _parmUserName = _sqlCommand.CreateParameter();
                        _parmUserName.DbType = DbType.String;
                        _parmUserName.ParameterName = "@parmUserName";
                        _parmUserName.Value = u.Username;
                        _sqlCommand.Parameters.Add(_parmUserName);

                        SqlParameter _paramPassword = _sqlCommand.CreateParameter();
                        _paramPassword.DbType = DbType.String;
                        _paramPassword.ParameterName = "@parmPassword";
                        _paramPassword.Value = u.Password;
                        _sqlCommand.Parameters.Add(_paramPassword);

                       
                        SqlParameter _paramSalt = _sqlCommand.CreateParameter();
                        _paramSalt.DbType = DbType.String;
                        _paramSalt.ParameterName = "@parmSalt";
                        _paramSalt.Value = u.Salt;
                        _sqlCommand.Parameters.Add(_paramSalt);


                        // TODO: @parmComment
                        SqlParameter _parmComment = _sqlCommand.CreateParameter();
                        _parmComment.DbType = DbType.String;
                        _parmComment.ParameterName = "@parmComment";
                        _parmComment.Value = u.Comment;
                        _sqlCommand.Parameters.Add(_parmComment);

                        // TODO:  @parmDateModified
                        SqlParameter _parmDateModified = _sqlCommand.CreateParameter();
                        _parmDateModified.DbType = DbType.DateTime;
                        _parmDateModified.ParameterName = "@parmDateModified";
                        _parmDateModified.Value = u.DateModified;
                        _sqlCommand.Parameters.Add(_parmDateModified);

                        // TODO: @parmModifiedByUserID
                        SqlParameter _parmModifiedByUserID = _sqlCommand.CreateParameter();
                        _parmModifiedByUserID.DbType = DbType.String;
                        _parmModifiedByUserID.ParameterName = "@parmModifiedByUserID";
                        _parmModifiedByUserID.Value = u.ModifiedByUserID;
                        _sqlCommand.Parameters.Add(_parmModifiedByUserID);

                        SqlParameter _parmUserIDOut = _sqlCommand.CreateParameter();
                        _parmUserIDOut.DbType = DbType.Int32;
                        _parmUserIDOut.ParameterName = "@parmUserIDOut";
                        var pk = _sqlCommand.Parameters.Add(_parmUserIDOut);
                        _parmUserIDOut.Direction = ParameterDirection.Output;

                        con.Open();
                        _sqlCommand.ExecuteNonQuery();   // calls the sp 
                        var result = _parmUserIDOut.Value;
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


        // TODO: return Operation DTO
        // register at login
        public UserDTO UpdateUser(UserDTO u)
        {

            UserDTO _return = new UserDTO();
            _return.ErrorMessage = "";

            try
            {
                using (SqlConnection con = new SqlConnection(_conn))
                {
                    using (SqlCommand _sqlCommand = new SqlCommand("uspUpdateUser", con))
                    {
                        _sqlCommand.CommandType = CommandType.StoredProcedure;
                        _sqlCommand.CommandTimeout = 30;

                        SqlParameter _parmUserID = _sqlCommand.CreateParameter();
                        _parmUserID.DbType = DbType.Int32;
                        _parmUserID.ParameterName = "@parmUserID";
                        _parmUserID.Value = u.UserId;
                        _sqlCommand.Parameters.Add(_parmUserID);

                        SqlParameter _parmRoleIdFK = _sqlCommand.CreateParameter();
                        _parmRoleIdFK.DbType = DbType.Int32;
                        _parmRoleIdFK.ParameterName = "@parmRoleID_FK";
                        _parmRoleIdFK.Value = u.RoleId;
                        _sqlCommand.Parameters.Add(_parmRoleIdFK);

                        //SqlParameter _parmAddressIDFK = _sqlCommand.CreateParameter();
                        //_parmAddressIDFK.DbType = DbType.Int32;
                        //_parmAddressIDFK.ParameterName = "@parmAddressID_FK";
                        //_parmAddressIDFK.Value = u.RoleId;
                        //_sqlCommand.Parameters.Add(_parmAddressIDFK);

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
                      
                        SqlParameter _parmPrimaryEmail = _sqlCommand.CreateParameter();
                        _parmPrimaryEmail.DbType = DbType.String;
                        _parmPrimaryEmail.ParameterName = "@parmPrimaryEmail";
                        _parmPrimaryEmail.Value = (u.PrimaryEmail == null) ? (object)System.DBNull.Value : u.PrimaryEmail;
                        _sqlCommand.Parameters.Add(_parmPrimaryEmail);


                        SqlParameter _parmPrimaryPhone = _sqlCommand.CreateParameter();
                        _parmPrimaryPhone.DbType = DbType.String;
                        _parmPrimaryPhone.ParameterName = "@parmPrimaryPhone";
                        _parmPrimaryPhone.Value = (u.PrimaryPhone == null) ? (object)System.DBNull.Value : u.PrimaryPhone;
                        _sqlCommand.Parameters.Add(_parmPrimaryPhone);

                        //SqlParameter _parmUserName = _sqlCommand.CreateParameter();
                        //_parmUserName.DbType = DbType.String;
                        //_parmUserName.ParameterName = "@parmUserName";
                        //_parmUserName.Value = u.Username;
                        //_sqlCommand.Parameters.Add(_parmUserName);

                        //SqlParameter _paramPassword = _sqlCommand.CreateParameter();
                        //_paramPassword.DbType = DbType.String;
                        //_paramPassword.ParameterName = "@parmPassword";
                        //_paramPassword.Value = u.Password;
                        //_sqlCommand.Parameters.Add(_paramPassword);


                        //SqlParameter _paramSalt = _sqlCommand.CreateParameter();
                        //_paramSalt.DbType = DbType.String;
                        //_paramSalt.ParameterName = "@parmSalt";
                        //_paramSalt.Value = u.Salt;
                        //_sqlCommand.Parameters.Add(_paramSalt);


                        SqlParameter _parmComment = _sqlCommand.CreateParameter();
                        _parmComment.DbType = DbType.String;
                        _parmComment.ParameterName = "@parmComment";
                        _parmComment.Value = (u.Comment == null) ? (object)System.DBNull.Value : u.Comment; ;
                        _sqlCommand.Parameters.Add(_parmComment);

                        SqlParameter _parmDateModified = _sqlCommand.CreateParameter();
                        _parmDateModified.DbType = DbType.DateTime;
                        _parmDateModified.ParameterName = "@parmDateModified";
                        _parmDateModified.Value = u.DateModified;
                        _sqlCommand.Parameters.Add(_parmDateModified);

                        SqlParameter _parmModifiedByUserID = _sqlCommand.CreateParameter();
                        _parmModifiedByUserID.DbType = DbType.String;
                        _parmModifiedByUserID.ParameterName = "@parmModifiedByUserID";
                        _parmModifiedByUserID.Value = u.ModifiedByUserID;
                        _sqlCommand.Parameters.Add(_parmModifiedByUserID);

                     
                        con.Open();
                        _sqlCommand.ExecuteNonQuery();   // calls the sp                        
                        con.Close();

                        return _return;


                    }
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        #endregion Users



        #region Roles

        public List<RoleDTO> GetRoles()
        {

            List<RoleDTO> _list = new List<RoleDTO>();

            try
            {
                using (SqlConnection con = new SqlConnection(_conn))
                {
                    using (SqlCommand _sqlCommand = new SqlCommand("uspGetRole", con))
                    {
                        _sqlCommand.CommandType = CommandType.Text;
                        _sqlCommand.CommandTimeout = 30;

                        con.Open();
                        RoleDTO _role;

                        using (SqlDataReader reader = _sqlCommand.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                _role = new RoleDTO
                                {                                  
                                    RoleId = reader.GetInt32(reader.GetOrdinal("RoleID")),
                                    RoleName = (reader["RoleName"] == System.DBNull.Value) ? "" : (string)reader["RoleName"],
                                    Comment = (reader["Comment"] == System.DBNull.Value) ? "" : (string)reader["Comment"],
                                    DateModified = reader.GetDateTime(reader.GetOrdinal("DateModified")),
                                    ModifiedByUserID = reader.GetInt32(reader.GetOrdinal("RoleID"))
                                };
                                _list.Add(_role);
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

        #endregion

    }
}
