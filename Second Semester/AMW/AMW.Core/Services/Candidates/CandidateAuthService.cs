using AMW.Core.IServices;
using AMW.Data.Models.Candidates;

namespace AMW.Core.Services.Candidates
{
    public class CandidateAuthService : AmwDefaultAuthService
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

        public override bool TryLogin(string username, string password, out int id)
        {
            return base.TryLogin(username, password, out id);
        }
    }
}
