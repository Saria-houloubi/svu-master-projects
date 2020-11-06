using AMW.Core.IServices;
using AMW.Core.Services.Base;
using AMW.Data.Models.Candidates;

namespace AMW.Core.Services.Candidates
{
    public partial class CandidateService : BaseAmwRepositoryService, IRepositoryService<Candidate>
    {
        private readonly string InsertOrUpdateCandiateProc = "Amw.Candidate_InsertOrUpdate";

    }
}
