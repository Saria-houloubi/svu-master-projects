using AMW.Core.IServices;
using AMW.Data.Models.Base;
using AMW.Data.Models.General;
using log4net;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AMW.Core.Services.Base
{
    public abstract class BaseAuthService<TFor, TSecure> : IAuthService<TFor, TSecure>
        where TFor : BaseEntity
    {
        #region Properties
        private static readonly ILog log = LogManager.GetLogger(nameof(BaseAuthService<TFor, TSecure>));

        public static JwtTokenSettings JwtTokenSettings { get; protected set; }

        protected readonly IConfiguration configuration;
        #endregion

        #region Constructer
        public BaseAuthService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        #endregion

        public virtual string CreateJwtToken(TFor model, params Claim[] extra)
        {
            JwtTokenSettings = JwtTokenSettings ?? SetupJwtTokenSettings();

            //Set the claims for the token
            var claims = new List<Claim>()
            {
                //Set a unique key for the clam
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Name, model.Id.ToString()),
                new Claim(ClaimTypes.Role, typeof(TFor).Name)
            };
            foreach (var item in extra)
            {
                if (!claims.Contains(item))
                {
                    claims.Add(item);
                };
            }
            //Create the credentials that are used for the token
            var credentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtTokenSettings.SecretKey)),
                SecurityAlgorithms.HmacSha256
                );

            //Create the jwt token
            var token = new JwtSecurityToken(
                issuer: JwtTokenSettings.Issuer,
                audience: JwtTokenSettings.Audience,
                claims: claims,
                expires: DateTime.Now.AddTicks(JwtTokenSettings.ExpireTicks),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public virtual JwtTokenSettings SetupJwtTokenSettings()
        {
            var jwtSettings = new JwtTokenSettings();

            configuration.GetSection("Jwt").Bind(jwtSettings);

            return jwtSettings;
        }

        public abstract Task<TFor> TryAuthenticateAsync(TSecure entity);
    }
}
