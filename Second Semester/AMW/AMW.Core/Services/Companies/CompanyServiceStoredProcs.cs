namespace AMW.Core.Services.Companies
{
    public partial class CompanyService
    {
        private readonly string InsertOrUpdateCandiateProc = "Amw.Company_InsertOrUpdate";
        private readonly string GetByIdProc = "Amw.Company_GetById";
        private readonly string GetByFilterProc = "Amw.Company_GetByFilter";
    }
}
