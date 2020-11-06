using AMW.Core.IServices;
using AMW.Shared.Models;
using log4net;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace AMW.Core.Services
{
    public class SqlServerDatabaseExecuterService : IDatabaseExecuterService
    {

        #region Properties
        private static IConfiguration configuration;
        private static readonly ILog log = LogManager.GetLogger(nameof(SqlServerDatabaseExecuterService));
        #endregion
        #region Constructer
        /// <summary>
        /// Default constructer
        /// </summary>
        public SqlServerDatabaseExecuterService(IConfiguration config)
        {
            configuration = configuration ?? config;
        }
        #endregion
        public async Task<int> RunStoredProcedureAsync(string name, Dictionary<string, object> paramters = null)
        {
            using (var connection = new SqlConnection(GetConnectionString()))
            {
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = name;

                    if (paramters != null)
                    {
                        foreach (var param in paramters)
                        {
                            cmd.Parameters.Add(new SqlParameter(param.Key, param.Value));
                        }
                    }

                    try
                    {
                        connection.Open();

                        return await cmd.ExecuteNonQueryAsync();
                    }
                    catch (Exception ex)
                    {
                        log.Error(ex.Message, ex);

                        throw ex;
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
        }

        public async Task<T> RunStoredProcedureAsync<T>(string name, Func<AwmSqlDataReaderWrapper, T> converter, Dictionary<string, object> paramters = null)
        {
            using (var connection = new SqlConnection(GetConnectionString()))
            {
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = name;

                    if (paramters != null)
                    {
                        foreach (var param in paramters)
                        {
                            cmd.Parameters.Add(new SqlParameter(param.Key, param.Value));
                        }
                    }

                    try
                    {
                        connection.Open();

                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            //Check if an erro happened
                            if (reader.RecordsAffected == -1)
                            {
                                reader.Read();
                            }
                            return converter(new AwmSqlDataReaderWrapper(reader, cmd.CommandText));
                        }
                    }
                    catch (Exception ex)
                    {
                        log.Error(ex.Message, ex);

                        throw ex;
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
        }

        #region Private 
        private string GetConnectionString()
        {
            var connectionName = configuration["connectionStringName"]?.ToString();

            if (string.IsNullOrEmpty(connectionName))
            {
                throw new Exception($"Could not read connection name from config key 'connectionStringName'");
            }
            var connectionString = configuration.GetConnectionString(connectionName);

            if (string.IsNullOrEmpty(connectionString))
            {
                throw new Exception($"No connection string found under name {connectionName}");
            }

            return connectionString;
        }
        #endregion
    }
}
