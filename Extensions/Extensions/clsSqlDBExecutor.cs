using System;
using System.Data.SqlClient;
using System.Data;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelperClasses.Extensions
{

    public static class clsSqlDBExecutor
    {
        // ********************** Private Retry Method **********************
        private static T _tryExecute<T>(Func<T> operation, string operationName, Action<string> logError = null)
        {
            try
            {
                return operation();
            }
            catch (SqlException ex)
            {
                logError?.Invoke($"Transient SQL error in operation \'{operationName}\': {ex.Message}");
            }
            catch (TimeoutException ex)
            {
                logError?.Invoke($"Timeout in operation \'{operationName}\': {ex.Message}");
            }
            catch (InvalidOperationException ex)
            {
                logError?.Invoke($"Invalid operation in \'{operationName}\': {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                logError?.Invoke($"Unexpected error in \'{operationName}\': {ex.Message}");
                throw;
            }
            return default;
        }

        private static async Task<T> _tryExecuteAsync<T>(Func<Task<T>> operation, string operationName, Action<string> logError = null)
        {
            try
            {
                return await operation();
            }
            catch (SqlException ex)
            {
                logError?.Invoke($"Transient SQL error in operation '{operationName}': {ex.Message}");
            }
            catch (TimeoutException ex)
            {
                logError?.Invoke($"Timeout in operation '{operationName}': {ex.Message}");
            }
            catch (InvalidOperationException ex)
            {
                logError?.Invoke($"Invalid operation in '{operationName}': {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                logError?.Invoke($"Unexpected error in '{operationName}': {ex.Message}");
                throw;
            }
            return default;
        }

        // ********************** Private Helper Method **********************

        private static SqlCommand _prepareCommand(SqlConnection connection, string query, CommandType commandType, SqlParameter[] parameters)
        {
            SqlCommand command = connection.CreateCommand();
            command.CommandText = query;
            command.CommandType = commandType;
            
            if (parameters != null) command.Parameters.AddRange(parameters);

            return command;
        }

        private static T _executeCommand<T>(string connectionString, string query, CommandType commandType, SqlParameter[] parameters, Func<SqlCommand, T> commandExecutor)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = _prepareCommand(connection, query, commandType, parameters))
                {
                    return commandExecutor(command);
                }
            }
        }

        private static async Task<T> _executeCommandAsync<T>(string connectionString, string query, CommandType commandType, SqlParameter[] parameters, Func<SqlCommand, Task<T>> commandExecutor)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                using (SqlCommand command = _prepareCommand(connection, query, commandType, parameters))
                {
                    return await commandExecutor(command);
                }
            }
        }


        // ********************** Public Methods **********************

        public static SqlDataReader ExecuteReader(string connectionString, string query, SqlParameter[] parameters = null, CommandType commandType = CommandType.Text, CommandBehavior behavior = CommandBehavior.SingleResult, Action<string> logError = null)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                SqlCommand command = _prepareCommand(connection, query, commandType, parameters);
                connection.Open();
                
                return command.ExecuteReader(behavior);
            }
            catch
            {
                connection.Dispose();
                throw;
            }
        }

        public static async Task<SqlDataReader> ExecuteReaderAsync(string connectionString, string query, SqlParameter[] parameters = null, CommandType commandType = CommandType.Text, CommandBehavior behavior = CommandBehavior.SingleResult, Action<string> logError = null)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                SqlCommand command = _prepareCommand(connection, query, commandType, parameters);
                await connection.OpenAsync();

                return await command.ExecuteReaderAsync(behavior);
            }
            catch
            {
                connection.Dispose();
                throw;
            }
        }


        public static int ExecuteNonQuery(string connectionString, string query, SqlParameter[] parameters = null, CommandType commandType = CommandType.Text, Action<string> logError = null)
        {
            return _tryExecute(() =>
            {
                return _executeCommand(connectionString, query, commandType, parameters, cmd => cmd.ExecuteNonQuery());
            }, nameof(ExecuteNonQuery), logError);
        }

        public static async Task<int> ExecuteNonQueryAsync(string connectionString, string query, SqlParameter[] parameters = null, CommandType commandType = CommandType.Text, Action<string> logError = null)
        {
            return await _tryExecuteAsync(async () =>
            {
                return await _executeCommandAsync(connectionString, query, commandType, parameters, cmd => cmd.ExecuteNonQueryAsync());
            }, nameof(ExecuteNonQueryAsync), logError);
        }
        

        public static object ExecuteScalar(string connectionString, string query, SqlParameter[] parameters = null, CommandType commandType = CommandType.Text, Action<string> logError = null)
        {
            return _tryExecute(() =>
            {
                return _executeCommand(connectionString, query, commandType, parameters, cmd => cmd.ExecuteScalar());
            }, nameof(ExecuteScalar), logError);
        }

        public static async Task<object> ExecuteScalarAsync(string connectionString, string query, SqlParameter[] parameters = null, CommandType commandType = CommandType.Text, Action<string> logError = null)
        {
            return await _tryExecuteAsync(async () =>
            {
                return await _executeCommandAsync(connectionString, query, commandType, parameters, cmd => cmd.ExecuteScalarAsync());
            }, nameof(ExecuteScalarAsync), logError);
        }

        
        public static bool ExecuteTransaction(string connectionString, Func<SqlConnection, SqlTransaction, bool> transactionBody, Action<string> logError = null)
        {
            return _tryExecute(() =>
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlTransaction transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            bool result = transactionBody(connection, transaction);
                            transaction.Commit();
                            return result;
                        }
                        catch
                        {
                            transaction.Rollback();
                            throw;
                        }
                    }
                }
            }, nameof(ExecuteTransaction), logError);
        }
        
        public static async Task<bool> ExecuteTransactionAsync(string connectionString, Func<SqlConnection, SqlTransaction, Task<bool>> transactionBody, Action<string> logError = null)
        {
            return await _tryExecuteAsync(async () =>
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();
                    using (SqlTransaction transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            bool result = await transactionBody(connection, transaction);
                            transaction.Commit();
                            return result;
                        }
                        catch
                        {
                            transaction.Rollback();
                            throw;
                        }
                    }
                }
            }, nameof(ExecuteTransactionAsync), logError);
        }


        public static DataTable ExecuteDataAdapter(string connectionString, string query, SqlParameter[] parameters = null, CommandType commandType = CommandType.Text, Action<string> logError = null)
        {
            return _tryExecute(() =>
            {
                return _executeCommand(connectionString, query, commandType, parameters, cmd =>
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        return dataTable;
                    }
                });
            }, nameof(ExecuteDataAdapter), logError);
        }

        public static async Task<DataTable> ExecuteDataAdapterAsync(string connectionString, string query, SqlParameter[] parameters = null, CommandType commandType = CommandType.Text, Action<string> logError = null)
        {
            return await _tryExecuteAsync(async () =>
            {
                return await _executeCommandAsync(connectionString, query, commandType, parameters, async cmd =>
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        DataTable dataTable = new DataTable();
                        await Task.Run(() => adapter.Fill(dataTable));
                        return dataTable;
                    }
                });
            }, nameof(ExecuteDataAdapterAsync), logError);
        }

    };
}
