using SVU.Database.Models.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace SVU.Database.Models
{
    /// <summary>
    /// This table was added db first and then mapped witht the properties here
    /// </summary>
    [Table(name: "HeartDisease", Schema = "SVUDataSet")]
    public class HeartDisease : BaseIdentityModel
    {
        #region Properties
        public double Age { get; set; }
        public double MaxHeartRate { get; set; }
        public double RestBloodPressure { get; set; }
        public bool BloodSugar { get; set; }
        public bool ExerciceAngina { get; set; }
        public bool Disease { get; set; }

        public string RestElectro { get; set; }
        public string ChestPainType { get; set; }
        #endregion

        #region Constructer
        /// <summary>
        /// Default constructer
        /// </summary>
        public HeartDisease()
        {

        }
        #endregion
    }
}
