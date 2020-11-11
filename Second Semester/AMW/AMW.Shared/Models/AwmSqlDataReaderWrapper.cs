using System.Data.SqlClient;

namespace AMW.Shared.Models
{
    public class AmwSqlDataReaderWrapper
    {
        #region Properties
        public SqlDataReader Reader { get; private set; }

        public string QueryText { get; private set; }

        public bool HasRows => Reader != null && Reader.HasRows;
        #endregion

        #region Constructer
        /// <summary>
        /// 
        /// </summary>
        public AmwSqlDataReaderWrapper(SqlDataReader reader, string query)
        {
            this.Reader = reader;
            this.QueryText = query;
        }
        #endregion
    }
}
