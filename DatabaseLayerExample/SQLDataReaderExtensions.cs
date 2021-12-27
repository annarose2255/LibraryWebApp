using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLayerExample
{
    public static class SQLDataReaderExtensions
    {
        public static string SafeGetString(this SqlDataReader reader, int colIndex)
        {
            if (!reader.IsDBNull(colIndex))
                return reader.GetString(colIndex);
            return null;
        }
        public static string SafeGetString(this SqlDataReader reader, string colName)
        {
            return reader.SafeGetString(reader.GetOrdinal(colName));
        }
    }
}
