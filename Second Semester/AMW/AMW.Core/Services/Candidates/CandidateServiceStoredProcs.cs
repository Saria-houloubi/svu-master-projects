namespace AMW.Core.Services.Candidates
{
    public partial class CandidateService 
    {
        public override string GetByFilterProc => "Amw.Candidate_GetByFilter";
        public override string GetByIdProc => "Amw.Candidate_GetById";
        public override string InsertOrUpdateProc => "Amw.Candidate_InsertOrUpdate";
    }
}
