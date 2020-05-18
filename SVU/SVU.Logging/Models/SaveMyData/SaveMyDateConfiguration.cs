namespace SVU.Logging.Models.SaveMyData
{
    public class SaveMyDataConfiguration
    {
        #region Properties
        public string Email { get; set; }
        public string Password { get; set; }
        public string DbName { get; set; }
        /// <summary>
        /// The names of the database for each type
        /// </summary>
        public string ExceptionTableName { get; set; }
        public string RequestsTableName { get; set; }
        #endregion

        #region constructer
        /// <summary>
        /// Default constructer
        /// </summary>
        public SaveMyDataConfiguration()
        {

        }
        #endregion

    }
}
