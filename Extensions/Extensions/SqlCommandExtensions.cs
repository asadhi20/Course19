using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelperClasses.Extensions
{
    public static class SqlCommandExtensions
    {
        public static SqlCommand CreateCommand(this SqlConnection connection, string query, SqlParameter[] parameters = null, CommandType commandType = CommandType.Text)
        {
            SqlCommand command = connection.CreateCommand();
            command.CommandText = query;
            command.CommandType = commandType;

            if (parameters != null) command.Parameters.AddRange(parameters);

            return command;
        }
    }
}
