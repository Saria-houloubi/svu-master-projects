namespace AMW.Core.Services.Jobs
{
    public partial class JobService 
    {
        public override string InsertOrUpdateProc => "Amw.Job_InsertOrUpdate";
    }
}
