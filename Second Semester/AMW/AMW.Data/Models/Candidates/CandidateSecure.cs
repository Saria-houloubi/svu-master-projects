using AMW.Data.Attributes;
using System.ComponentModel.DataAnnotations;

namespace AMW.Data.Models.Candidates
{
    public class CandidateSecure : Candidate
    {
        #region Properties
        [Required]
        [SqlParam]
        public string Login { get; set; }
        [Required]
        [SqlParam]
        public string Password { get; set; }
        #endregion
    }
}
