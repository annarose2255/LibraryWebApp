using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryDatabaseAccessLayer
{
    public class LogException
    {
        // fields
        private readonly string _conn;

        // properties

        // constructors
        public LogException(string conn)
        {
            _conn = conn;
        }

        public LogException()
        {
        }
        public int CreateLogException(Exception e)
        {
                using (SqlConnection con = new SqlConnection(_conn))
                {
                    using (SqlCommand _sqlCommand = new SqlCommand("uspCreateLogException", con))
                    {
                        _sqlCommand.CommandType = CommandType.StoredProcedure;
                        _sqlCommand.CommandTimeout = 30;


                        SqlParameter _parmStackTrace = _sqlCommand.CreateParameter();
                        _parmStackTrace.DbType = DbType.String;
                        _parmStackTrace.ParameterName = "@parmStackTrace";
                        _parmStackTrace.Value = e.StackTrace;
                        _sqlCommand.Parameters.Add(_parmStackTrace);

                        SqlParameter _parmMessage = _sqlCommand.CreateParameter();
                        _parmMessage.DbType = DbType.String;
                        _parmMessage.ParameterName = "@parmMessage";
                        _parmMessage.Value = e.Message;
                        _sqlCommand.Parameters.Add(_parmMessage);

                        SqlParameter _parmSource = _sqlCommand.CreateParameter();
                        _parmSource.DbType = DbType.String;
                        _parmSource.ParameterName = "@parmSource";
                        _parmSource.Value = e.Source;
                        _sqlCommand.Parameters.Add(_parmSource);

                        SqlParameter _parmURL = _sqlCommand.CreateParameter();
                        _parmURL.DbType = DbType.String;
                        _parmURL.ParameterName = "@parmURL";
                        _parmURL.Value = e.HelpLink;
                        _sqlCommand.Parameters.Add(_parmURL);

                        SqlParameter _parmLogdate = _sqlCommand.CreateParameter();
                        _parmLogdate.DbType = DbType.DateTime;
                        _parmLogdate.ParameterName = "@parmLogdate";
                        _parmLogdate.Value = DateTime.Now;
                        _sqlCommand.Parameters.Add(_parmLogdate);

                        SqlParameter _parmLogIDOut = _sqlCommand.CreateParameter();
                        _parmLogIDOut.DbType = DbType.Int32;
                        _parmLogIDOut.ParameterName = "@parmOutExceptionLoggingID";
                        _parmLogIDOut.Direction = ParameterDirection.Output;
                        var pk = _sqlCommand.Parameters.Add(_parmLogIDOut);

                        con.Open();
                        _sqlCommand.ExecuteNonQuery();   // calls the sp 
                        var result = _parmLogIDOut.Value;
                        con.Close();
                        return (int)result;

                    }
                }

        }
    }
}
