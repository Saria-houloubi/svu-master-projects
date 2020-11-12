namespace AMW.Core.Services.Companies
{
    public partial class CompanyService
    {
        public override string GetByIdProc => "Amw.Company_GetById";
        public override string InsertOrUpdateProc => "Amw.Company_InsertOrUpdate";
        public override string GetByFilterProc => "Amw.Company_GetByFilter";

    }
}
