using AMW.Core.IServices;
using AMW.Core.Services.Base;
using AMW.Data.Models.Amw;
using AMW.Data.Models.Candidates;
using AMW.Shared.Extensioins.String;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AMW.Core.Services.Candidates
{
    public class CandidateAuthService : BaseAuthService<Candidate, AmwSecure>
    {
        #region Properties
        private readonly IRepositoryService<Candidate> candidateService;
        #endregion

        #region Constructer
        /// <summary>
        /// Default constructer
        /// </summary>
        public CandidateAuthService(IRepositoryService<Candidate> candidateService, IConfiguration configuration)
            : base(configuration)
        {
            this.candidateService = candidateService;
        }
        #endregion

        public override async Task<Candidate> TryAuthenticateAsync(AmwSecure entity)
        {
            var candidate = (await candidateService.GetByFilterAsync(new CandidateFilter()
            {
                Login = entity.Login,
            })).SingleOrDefault();


            if (candidate != null && candidate is CandidateRegister fullInfo && fullInfo.Password.VertifyPassword(entity.Password))
            {
                candidate.Extra.Add("token", CreateJwtToken(candidate, new Claim(ClaimTypes.NameIdentifier, candidate.FullName)));
                return (candidate as Candidate);
            }

            return null;
        }
    }
}
