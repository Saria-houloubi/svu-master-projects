using System;

namespace AMW.Data.Models.General
{
    /// <summary>
    /// The setting model for JWT token 
    /// </summary>
    public class JwtTokenSettings
    {
        #region Properties
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string SecretKey { get; set; }
        /// <summary>
        /// The amount of ticks to which jwt token expires
        /// </summary>
        public long ExpireTicks { get; set; }
        #endregion
    }
}
