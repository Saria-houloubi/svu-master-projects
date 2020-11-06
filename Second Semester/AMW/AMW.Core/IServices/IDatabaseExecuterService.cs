using AMW.Shared.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace AMW.Core.IServices
{
    /// <summary>
    /// The service to execute and run sql or SP on the database
    /// </summary>
    public interface IDatabaseExecuterService
    {
        Task<int> RunStoredProcedureAsync(string name, Dictionary<string, object> paramters = null);
        Task<T> RunStoredProcedureAsync<T>(string name, Func<AwmSqlDataReaderWrapper, T> converter, Dictionary<string, object> paramters = null);

    }
}
