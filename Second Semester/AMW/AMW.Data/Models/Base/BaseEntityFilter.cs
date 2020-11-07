using AMW.Data.Attributes;

namespace AMW.Data.Models.Base
{
    /// <summary>
    /// Base entity filter to be passed around to repositories
    /// </summary>
    public abstract class BaseEntityFilter
    {
        #region Properties
        [SqlParam]
        public int Id { get; set; }
        #endregion
    }
}
