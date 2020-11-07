using AMW.Data.Attributes;
using System.ComponentModel.DataAnnotations;

namespace AMW.Data.Models.Base
{
    public abstract class BaseSecureModel
    {
        #region Properties
        public virtual string Login { get; set; }
        [Required]
        [SqlParam]
        private string password;

        public virtual string Password
        {
            get { return password; }
            set { password =EncryptPassword(value); }
        }
        #endregion


        #region Methods

        protected virtual string EncryptPassword(string password)
        {
            return password;
        }
        #endregion
    }
}
