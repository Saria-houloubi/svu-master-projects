namespace AMW.Core.IServices
{
    public interface IAuthService
    {
        bool TryLogin(string username, string password, out int id);
    }
}
