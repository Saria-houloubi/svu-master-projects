using System;

namespace AMW.API.Models
{
    public class ErrorModel
    {
        #region Properties
        public string Error { get; set; }

        public Exception Exception { get; set; }
        #endregion

        #region Constructers
        /// <summary>
        /// Default constructer
        /// </summary>
        public ErrorModel()
        {

        }
        /// <summary>
        /// Error constructer
        /// </summary>
        public ErrorModel(string error)
        {
            this.Error = error;
        }
        #endregion
    }
}
