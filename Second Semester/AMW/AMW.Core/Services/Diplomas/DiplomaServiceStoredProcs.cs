namespace AMW.Core.Services.Diplomas
{
    public partial class DiplomaService
    {
        public override string GetByFilterProc => "Amw.Diploma_GetByFilter";
        public override string GetByIdProc => "Amw.Diploma_GetById";
        public override string InsertOrUpdateCandiateProc => "Amw.Diploma_InsertOrUpdate";
    }
}
