using System;

namespace AMW.API.Models
{
    public class ErrorModel
    {
        #region Properties
        public string Error { get; set; }

        public Exception Exception { get; set; }
        #endregion
    }
}
