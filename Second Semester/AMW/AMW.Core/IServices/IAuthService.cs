using AMW.Data.Models.Base;
using AMW.Data.Models.General;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AMW.Core.IServices
{
    /// <summary>
    /// Authenticaion abstract 
    /// </summary>
    /// <typeparam name="TFor">The authorizsd return model</typeparam>
    /// <typeparam name="TSecure">the model to get information based on</typeparam>
    public interface IAuthService<TFor, TSecure> {
        Task<TFor> TryAuthenticateAsync(TSecure entity);

        string CreateJwtToken(TFor model, params Claim[] extra);

        JwtTokenSettings SetupJwtTokenSettings();
    }
}
