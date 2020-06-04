using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SVU.Web.UI.Models.Homework
{
    /// <summary>
    /// The model for a calculating bayes algo
    /// </summary>
    public class CalculateTargetModel
    {
        #region Properties
        [Required]
        public string Target { get; set; }
        [Required]
        public string DbSet { get; set; }
        /// <summary>
        /// Any properties to ignore during the calcuations
        /// </summary>
        public IEnumerable<string> IgnoreProperties { get; set; }
        public JObject TestExample { get; set; }
        #endregion

        #region Constructer
        /// <summary>
        /// Default constructer
        /// </summary>
        public CalculateTargetModel()
        {

        }
        #endregion
    }
}
