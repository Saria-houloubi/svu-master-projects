using AMW.Data.Attributes;
using AMW.Data.Models.Base;
using AMW.Shared.Extensioins;
using System.ComponentModel.DataAnnotations;

namespace AMW.Data.Models.Amw
{
    public class AmwSecure : BaseSecureModel
    {
        #region Properties
        [Required]
        [SqlParam]
        public override string Login { get; set; }

        [Required]
        public override string Password
        {
            get => base.Password;
            set => base.Password = value;
        }
        #endregion


        #region Methods
        #endregion
    }
}
