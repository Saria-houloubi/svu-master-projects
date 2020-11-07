using AMW.Core.IServices;
using AMW.Data.Models.Amw;
using AMW.Data.Models.Candidates;
using System.Linq;
using System.Threading.Tasks;

namespace AMW.Core.Services.Candidates
{
    public class CandidateAuthService : IAuthService<Candidate, AmwSecure>
    {
        #region Properties
        private readonly IRepositoryService<Candidate> candidateService;
        #endregion

        #region Constructer
        /// <summary>
        /// Default constructer
        /// </summary>
        public CandidateAuthService(IRepositoryService<Candidate> candidateService)
        {
            this.candidateService = candidateService;
        }
        #endregion

        public async Task<Candidate> TryAuthenticateAsync(AmwSecure entity)
        {
            return (await candidateService.GetByFilterAsync(new CandidateFilter()
            {
                Login = entity.Login,
                Password = entity.Password
            })).SingleOrDefault();
        }
    }
}
