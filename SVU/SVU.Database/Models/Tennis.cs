using SVU.Database.Models.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace SVU.Database.Models
{
    /// <summary>
    /// This table was added db first and then mapped witht the properties here
    /// </summary>
    [Table(name: "Tennis", Schema = "SVUDataSet")]
    public class Tennis : BaseIdentityModel
    {
        #region Properties
        public string Outlook { get; set; }
        public string Temp { get; set; }
        public string Wind { get; set; }
        public string Humidity { get; set; }
        public string Play { get; set; }

        #endregion

        #region Constructer
        /// <summary>
        /// Default constructer
        /// </summary>
        public Tennis()
        {

        }
        #endregion
    }
}
