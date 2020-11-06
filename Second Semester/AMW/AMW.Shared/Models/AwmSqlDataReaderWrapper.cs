using System.Data.SqlClient;

namespace AMW.Shared.Models
{
    public class AwmSqlDataReaderWrapper
    {
        #region Properties
        public SqlDataReader Reader { get; private set; }

        public string QueryText { get; private set; }
        #endregion

        #region Constructer
        /// <summary>
        /// 
        /// </summary>
        public AwmSqlDataReaderWrapper(SqlDataReader reader, string query)
        {
            this.Reader = reader;
            this.QueryText = query;
        }
        #endregion
    }
}
