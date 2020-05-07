using System.ComponentModel.DataAnnotations;

namespace SVU.Web.UI.Models.Homework
{
    /// <summary>
    /// The model for a heart disease patient
    /// </summary>
    public class HeartDiseasePatientModel
    {
        #region Properties
        [Required]
        public int Age { get; set; }
        [Required]
        public string ChestPainType { get; set; }
        [Required]
        public int RestBloodPressure { get; set; }
        public bool BloodSuger { get; set; }
        [Required]
        public string RestElectro { get; set; }
        [Required]
        public int MaxHeartRate { get; set; }
        public bool ExerciceAngina { get; set; }
        #endregion

        #region Constructer
        /// <summary>
        /// Default constructer
        /// </summary>
        public HeartDiseasePatientModel()
        {

        }
        #endregion
    }
}
