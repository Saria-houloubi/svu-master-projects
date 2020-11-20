using AMW.Data.Abstraction.Parsing;
using AMW.Data.Attributes;
using AMW.Shared.Extensioins.String;
using AMW.Shared.Extensions;
using AMW.Shared.Models;
using System.ComponentModel.DataAnnotations;

namespace AMW.Data.Models.Companies
{
    public class CompanyRegister : Company
    {
        #region Properties
        [SqlParam]
        [Required]

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
