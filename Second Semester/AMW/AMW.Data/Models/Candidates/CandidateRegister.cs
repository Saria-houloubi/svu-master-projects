using AMW.Data.Abstraction.Parsing;
using AMW.Data.Attributes;
using AMW.Shared.Extensioins.String;
using AMW.Shared.Extensions;
using AMW.Shared.Models;
using System.ComponentModel.DataAnnotations;

namespace AMW.Data.Models.Candidates
{
    public class CandidateRegister : Candidate
    {
        #region Properties
        [Required]
        [SqlParam]
        public string Login { get; set; }

        private string password;
        [Required]
        [SqlParam]
        public string Password
        {
            get { return password; }
            set
            {
                password = noHash ? value : value.HashPassword();
            }
        }

        /// <summary>
        /// A flag to not has the password
        ///     which happens when reading password from db
        /// </summary>
        private bool noHash;
        #endregion

        #region Constructer
        /// <summary>
        /// Default constructer
        /// </summary>
        public CandidateRegister()
        {

        }
        #endregion

        public override void ParseData(AmwSqlDataReaderWrapper reader)
        {
            base.ParseData(reader);

            noHash = true;
            Login = reader.GetValueIfExisits<string>(nameof(Login));
            Password = reader.GetValueIfExisits<string>(nameof(Password));
        }

        #region Serialize Methods

        public bool ShouldSerializePassword()
        {
            return false;
        }
        #endregion
    }
}
