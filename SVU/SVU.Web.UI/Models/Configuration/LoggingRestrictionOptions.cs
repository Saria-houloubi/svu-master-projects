using System.Collections.Generic;

namespace SVU.Web.UI.Models.Configuration
{
    /// <summary>
    /// Some restrection on what can we log
    /// </summary>
    public class LoggingRestrictionOptions
    {
        #region Properties
        public const string SectionName = "LoggingRestriction";
        /// <summary>
        /// The hosts that logging is blocked for them
        /// </summary>
        public IEnumerable<string> BlockedHosts { get; set; }
        #endregion

        #region Constructer
        /// <summary>
        /// Default constructer
        /// </summary>
        public LoggingRestrictionOptions()
        {

        }
        #endregion
    }
}
