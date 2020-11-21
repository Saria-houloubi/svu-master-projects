using System;
using System.Collections.Generic;
using System.Text;

namespace AMW.API.Framework.Models.Cors
{
    public class CorsWrapper
    {
        #region Properties
        public string[] ExposedHeaders { get; set; }
        public string[] AllowedOrigins { get; set; }
        public string[] AllowedMethods { get; set; }
        public string[] AllowedHeaders { get; set; }
        public bool SupportsCredentials { get; set; }
        #endregion

        #region Constructer
        /// <summary>
        /// Default constructer
        /// </summary>
        public CorsWrapper()
        {

        }
        #endregion
    }
}
