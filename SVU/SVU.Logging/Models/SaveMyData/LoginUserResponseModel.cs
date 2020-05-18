using System;
using System.Collections.Generic;
using System.Text;

namespace SVU.Logging.Models.SaveMyData
{
    /// <summary>
    /// The response mapping for trying to log the user in
    /// </summary>
    public class LoginUserResponseModel
    {
        #region Properties
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public DateTime Dob { get; set; }
        public bool IsMailConfirmed { get; set; }

        public string Token { get; set; }
        #endregion

        #region Constructer
        /// <summary>
        /// Default constructer
        /// </summary>
        public LoginUserResponseModel()
        {

        }
        #endregion
    }
}
