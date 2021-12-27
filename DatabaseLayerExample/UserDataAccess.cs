using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using LibraryCommon.DTO;
using LibraryCommon;

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

        public UserDTO GetUser(UserDTO u)
        {
            List<UserDTO> all_users = this.GetUsers();
            //is u UserId null here?
            UserDTO match  = all_users.Find(dbu => dbu.Username == u.Username);
            return match;
        } 
        public UserDTO GetUser(int userId)
        {
            List<UserDTO> all_users = this.GetUsers();
            UserDTO match = all_users.Find(dbu => dbu.UserId == userId);
            return match;

        } 

        public void UpdateUser(UserDTO u) //update user and (hopefully)return the user with its updated fields
            {
                UserDTO _requesteduser = new UserDTO();
                UserDTO test = u; //does this hold the new fields requested by user?

            try
            {
                Hasher hasher = new Hasher();
                using (SqlConnection con = new SqlConnection(_conn))
                {
                    using (SqlCommand _sqlCommand = new SqlCommand("uspUpdateUser", con))
                    {
                        _sqlCommand.CommandType = CommandType.Text;
                        _sqlCommand.CommandTimeout = 30;

                        SqlParameter _parmUserID = _sqlCommand.CreateParameter();
                        _parmUserID.DbType = DbType.Int32;
                        _parmUserID.ParameterName = "@parmUserID";
                        _parmUserID.Value = u.UserId;
                        _sqlCommand.Parameters.Add(_parmUserID);

                        SqlParameter _parmRoleID_FK = _sqlCommand.CreateParameter();
                        _parmRoleID_FK.DbType = DbType.Int32;
                        _parmRoleID_FK.ParameterName = "@parmRoleID_FK";
                        _parmRoleID_FK.Value = u.RoleId;
                        _sqlCommand.Parameters.Add(_parmRoleID_FK);

                        SqlParameter _parmAddressID_FK = _sqlCommand.CreateParameter();
                        _parmAddressID_FK.DbType = DbType.Int32;
                        _parmAddressID_FK.ParameterName = "@parmAddressID_FK";
                        _parmAddressID_FK.Value = u.AddressID;
                        _sqlCommand.Parameters.Add(_parmAddressID_FK);

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
                        _parmPrimaryEmail.Value = u.PrimaryEmail;
                        _sqlCommand.Parameters.Add(_parmPrimaryEmail);

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
                        //hash and salt required on u.Password if password is being altered
                        SqlParameter _parmPassword = _sqlCommand.CreateParameter();
                        _parmPassword.DbType = DbType.String;
                        _parmPassword.ParameterName = "@parmPassword";
                        _parmPassword.Value = hasher.HashedValue(u.Salt + u.Password);
                        _sqlCommand.Parameters.Add(_parmPassword);
                        //should the user's salt ever need to change? (even admin?)
                        //SqlParameter _parmSalt = _sqlCommand.CreateParameter();
                        //_parmSalt.DbType = DbType.String;
                        //_parmSalt.ParameterName = "@parmSalt";
                        //_parmSalt.Value = u.Salt;
                        //_sqlCommand.Parameters.Add(_parmSalt);

                        SqlParameter _parmComment = _sqlCommand.CreateParameter();
                        _parmComment.DbType = DbType.String;
                        _parmComment.ParameterName = "@parmComment";
                        _parmComment.Value = u.Comment;
                        _sqlCommand.Parameters.Add(_parmComment);

                        SqlParameter _parmDateModified = _sqlCommand.CreateParameter();
                        _parmDateModified.DbType = DbType.DateTime;
                        _parmDateModified.ParameterName = "@parmDateModified";
                        _parmDateModified.Value = DateTime.Now;
                        _sqlCommand.Parameters.Add(_parmDateModified);

                        SqlParameter _parmModifiedByUserID = _sqlCommand.CreateParameter();
                        _parmModifiedByUserID.DbType = DbType.Int32;
                        _parmModifiedByUserID.ParameterName = "@parmModifiedByUserID";
                        _parmModifiedByUserID.Value = u.ModifiedByUserID;
                        _sqlCommand.Parameters.Add(_parmModifiedByUserID);
                        con.Open();
                        //no return value from uspUpdateUser
                        con.Close();
                    }
                }
                //_requesteduser = GetUser(u); //obtain user with altered fields
            }
            catch (Exception ex)
            {
                throw;
            }
            //return _requesteduser;
            }

    }
}
