namespace AMW.Core.Services.Jobs
{
    public partial class JobService 
    {
        public override string InsertOrUpdateProc => "Amw.Job_InsertOrUpdate";
        public override string GetByFilterProc => "Amw.Job_GetByFilter";
        public override string GetByIdProc => "Amw.Job_GetById";
    }
}
