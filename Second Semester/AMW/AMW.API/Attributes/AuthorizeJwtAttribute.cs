using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace AMW.API.Attributes
{
    public class AuthorizeJwtAttribute : AuthorizeAttribute
    {
        #region Constructer
        /// <summary>
        /// Default constructer
        /// </summary>
        public AuthorizeJwtAttribute()
        {
            this.AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme;
        }
        #endregion
    }
}
