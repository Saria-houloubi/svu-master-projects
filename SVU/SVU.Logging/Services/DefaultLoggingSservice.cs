using SVU.Logging.IServices;
using System;

namespace SVU.Logging.Services
{
    /// <summary>
    /// The default loggin service class
    /// </summary>
    public class DefaultLoggingSservice : ILogginService
    {
        #region Constructer
        /// <summary>
        /// Default constructer
        /// </summary>
        public DefaultLoggingSservice()
        {

        }
        #endregion
        public void LogException(Exception ex)
        {
            Console.WriteLine(ex.GetBaseException().Message);
        }
    }
}
