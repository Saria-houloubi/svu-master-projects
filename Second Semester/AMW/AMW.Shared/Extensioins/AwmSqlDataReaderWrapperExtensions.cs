using AMW.Shared.Models;
using log4net;
using System.Collections.Generic;

namespace AMW.Shared.Extensions
{
    public static class AwmSqlDataReaderWrapperExtensions
    {

        /// <summary>
        /// Caching query paramters for faster preformance
        /// </summary>
        private static Dictionary<string, List<string>> queryColumns = new Dictionary<string, List<string>>();
        private static readonly ILog log = LogManager.GetLogger(nameof(AwmSqlDataReaderWrapperExtensions));
        public static T GetValueIfExisits<T>(this AmwSqlDataReaderWrapper reader, string column)
        {
            //Check if the query has been run before
            if (queryColumns.ContainsKey(reader.QueryText))
            {
                if (queryColumns[reader.QueryText].Contains(column))
                {
                    try
                    {
                        return (T)reader.Reader[column];
                    }
                    catch (System.Exception ex)
                    {
                        log.Error("Unable to cast object", ex);
                    }
                }
            }
            else
            {
                queryColumns.Add(reader.QueryText, new List<string>());

                for (int i = 0; i < reader.Reader.FieldCount; i++)
                {
                    queryColumns[reader.QueryText].Add(reader.Reader.GetName(i));
                }

                return reader.GetValueIfExisits<T>(column);
            }
            return default;
        }
    }
}
