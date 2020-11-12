namespace AMW.Core.Services.Diplomas
{
    public partial class DiplomaService
    {
        public override string GetByFilterProc => "Amw.Diploma_GetByFilter";
        public override string GetByIdProc => "Amw.Diploma_GetById";
        public override string InsertOrUpdateProc => "Amw.Diploma_InsertOrUpdate";
        public override string DeleteEntityProc => "Amw.Diploma_Delete";
    }
}
